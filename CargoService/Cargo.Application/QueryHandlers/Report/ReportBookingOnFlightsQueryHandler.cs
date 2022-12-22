using Cargo.Application.Services;
using Cargo.Contract.Queries.Report;
using Cargo.Infrastructure.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Report
{
    public class ReportBookingOnFlightsQueryHandler : IQueryHandler<ReportBookingOnFlightsQuery, byte[]>
    {
        SettingsService commPayloaderService;
        CargoContext CargoContext;
        public ReportBookingOnFlightsQueryHandler(CargoContext CargoContext, SettingsService commPayloaderService)
        {
            this.CargoContext = CargoContext;
            this.commPayloaderService = commPayloaderService;
        }

        public async Task<byte[]> Handle(ReportBookingOnFlightsQuery request, CancellationToken cancellationToken)
        {
            //не до красоты
            var Airlines = this.CargoContext.Airlines.Where(al => al.ContragentId == request.agentId).FirstOrDefault();
            IQueryable<ReportBookingOnFlights> query;
            if (Airlines != null)
            {
                query = from flight in CargoContext.FlightShedules
                        join booking in CargoContext.Bookings on flight.Id equals booking.FlightScheduleId
                        join awb in CargoContext.Awbs on booking.AwbId equals awb.Id
                        where flight.FlightDate >= request.beginDate
                        & flight.FlightDate <= request.endDate
                        & flight.SaleState != IDeal.Common.Components.FlightSaleState.Closed
                        where booking.SpaceAllocationCode == "KK"
                        orderby flight.FlightDate
                        select new ReportBookingOnFlights
                        {
                            AwbAgentId = awb.AgentId,
                            FlightId = flight.Id,
                            FlightOrigin = flight.Origin,
                            FlightDestination = flight.Destination,
                            FlightDate = flight.FlightDate,
                            FlightStDestination = flight.StDestination,
                            FlightNumber = flight.Number,
                            FlightAircraftType = flight.AircraftType,
                        };
            }
            else
            {
                query = from flight in CargoContext.FlightShedules
                        join booking in CargoContext.Bookings on flight.Id equals booking.FlightScheduleId
                        join awb in CargoContext.Awbs on booking.AwbId equals awb.Id
                        where flight.FlightDate >= request.beginDate
                        & flight.FlightDate <= request.endDate
                        & flight.SaleState != IDeal.Common.Components.FlightSaleState.Closed
                        where booking.SpaceAllocationCode == "KK"
                        where awb.AgentId == request.agentId
                        select new ReportBookingOnFlights
                        {
                            AwbAgentId = awb.AgentId,
                            FlightId = flight.Id,
                            FlightOrigin = flight.Origin,
                            FlightDestination = flight.Destination,
                            FlightDate = flight.FlightDate,
                            FlightStDestination = flight.StDestination,
                            FlightNumber = flight.Number,
                            FlightAircraftType = flight.AircraftType,
                        };
            }
            var flights = query.AsNoTracking().ToList();

            int i = 0;
            foreach (var flight in flights)
            {
                var planCommPayload = await commPayloaderService.FindPayload(flight.FlightAircraftType, flight.FlightDate);

                var bookings = CargoContext.Bookings
                    .Include(i => i.Awb)
                    .Where(x => x.FlightScheduleId == flight.FlightId & x.SpaceAllocationCode == "KK")
                    .AsNoTracking().ToList();

                var awbs = bookings.Select(x => x.Awb).ToList();
                flights[i].WeightFact = (double)awbs.Sum(w => w.Weight);
                flights[i].VolumeFact = (double)awbs.Sum(v => v.VolumeAmount);
                flights[i].WeightPlan = (double)planCommPayload.Value.Weight;
                flights[i].VolumePlan = (double)planCommPayload.Value.Volume;
                i++;
            }

            var result = GenerateExcelNPOI(flights);
            //var result = GenerateExcel(flights);
            //File.WriteAllBytes("Report.xlsx", result);
            return await Task.FromResult(result);
        }

        private static byte[] GenerateExcelNPOI(List<ReportBookingOnFlights> request)
        {
            byte[] result = null;
            using (MemoryStream ms = new())
            {
                int colsCount = 11;
                XSSFWorkbook workbook = new();
                XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("Бронирование на рейсах");
                ICreationHelper createHelper = workbook.GetCreationHelper();
                var cellsName = new[]
                {
                    "Дата",
                    "STD",
                    "№рейса",
                    "Рейс из",
                    "Рейс в",
                    "Бронь общая КК (кг)",
                    "Бронь общая КК (м3)",
                    "ПКЗ рейса общее (кг)",
                    "ПКЗ рейса общее (м3)",
                    "% использ ПКЗ по весу",
                    "% использ ПКЗ по объему"
                };

                XSSFCellStyle cellHeaderStyle = (XSSFCellStyle)workbook.CreateCellStyle();
                cellHeaderStyle.WrapText = true;
                cellHeaderStyle.VerticalAlignment = VerticalAlignment.Center;
                cellHeaderStyle.Alignment = HorizontalAlignment.Center;
                cellHeaderStyle.BorderTop = BorderStyle.Thin;
                cellHeaderStyle.BorderLeft = BorderStyle.Thin;
                cellHeaderStyle.BorderBottom = BorderStyle.Thin;
                cellHeaderStyle.BorderRight = BorderStyle.Thin;

                sheet.SetColumnWidth(0, 4500);
                sheet.SetColumnWidth(1, 4500);


                XSSFRow rowHeader = (XSSFRow)sheet.CreateRow(0);
                for (int i = 0; i < colsCount; i++)
                {
                    rowHeader.CreateCell(i).SetCellValue(cellsName[i]);
                    rowHeader.Cells[i].CellStyle = cellHeaderStyle;
                }

                rowHeader.HeightInPoints = 60;


                XSSFCellStyle cellDateTimeStyle = (XSSFCellStyle)workbook.CreateCellStyle();
                cellDateTimeStyle.SetDataFormat(createHelper.CreateDataFormat().GetFormat("dd.mm.yyyy hh:mm"));

                XSSFCellStyle cellNumberStyle = (XSSFCellStyle)workbook.CreateCellStyle();
                cellNumberStyle.SetDataFormat(createHelper.CreateDataFormat().GetFormat("# ##0.00"));

                XSSFCellStyle cellStringStyle = (XSSFCellStyle)workbook.CreateCellStyle();
                cellStringStyle.Alignment = HorizontalAlignment.Center;

                int rowIdx = 1;
                foreach (var data in request)
                {
                    XSSFRow row = (XSSFRow)sheet.CreateRow(rowIdx);
                    row.CreateCell(0).SetCellValue((DateTime)data.FlightDate);
                    row.CreateCell(1).SetCellValue(data.FlightStDestination);
                    row.CreateCell(2).SetCellValue(data.FlightNumber);
                    row.CreateCell(3).SetCellValue(data.FlightOrigin);
                    row.CreateCell(4).SetCellValue(data.FlightDestination);
                    row.CreateCell(5).SetCellValue(data.WeightFact);
                    row.CreateCell(6).SetCellValue(data.VolumeFact);
                    row.CreateCell(7).SetCellValue(data.WeightPlan);
                    row.CreateCell(8).SetCellValue(data.VolumePlan);
                    row.CreateCell(9).SetCellValue(data.WeightFact / data.WeightPlan * 100);
                    row.CreateCell(10).SetCellValue(data.VolumeFact / data.VolumePlan * 100);

                    row.Cells[0].CellStyle = cellDateTimeStyle;
                    row.Cells[1].CellStyle = cellDateTimeStyle;
                    row.Cells[2].CellStyle = cellStringStyle;
                    row.Cells[6].CellStyle = cellNumberStyle;
                    row.Cells[7].CellStyle = cellNumberStyle;
                    row.Cells[8].CellStyle = cellNumberStyle;
                    row.Cells[9].CellStyle = cellNumberStyle;
                    row.Cells[10].CellStyle = cellNumberStyle;
                    rowIdx++;
                }
                workbook.Write(ms);
                result = ms.ToArray();
            }
            return result;
        }

        internal class ReportBookingOnFlights
        {
            public int? AwbAgentId { get; set; }
            public ulong FlightId { get; set; }
            public string FlightOrigin { get; set; }
            public string FlightDestination { get; set; }
            public string FlightNumber { get; set; }
            public string FlightAircraftType { get; set; }
            public DateTime FlightStDestination { get; set; }
            public DateTime FlightDate { get; set; }
            public double WeightFact { get; set; }
            public double VolumeFact { get; set; }
            public double WeightPlan { get; set; }
            public double VolumePlan { get; set; }
        }
    }
}

