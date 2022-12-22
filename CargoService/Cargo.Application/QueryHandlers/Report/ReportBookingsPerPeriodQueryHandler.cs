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
    public class ReportBookingsPerPeriodQueryHandler : IQueryHandler<ReportBookingsPerPeriodQuery, byte[]>
    {
        CargoContext CargoContext;

        public ReportBookingsPerPeriodQueryHandler(CargoContext CargoContext) => this.CargoContext = CargoContext;

        public async Task<byte[]> Handle(ReportBookingsPerPeriodQuery request, CancellationToken cancellationToken)
        {
            //не до красоты
            var Airlines = this.CargoContext.Airlines
                .Where(al => al.ContragentId == request.agentId).FirstOrDefault();
            IQueryable<ReportBookingsPerPeriod> query;
            if (Airlines != null)
            {
                query = from awb in this.CargoContext.Awbs
                        join agent in this.CargoContext.Contragents on awb.AgentId equals agent.Id
                        join booking in this.CargoContext.Bookings on awb.Id equals booking.AwbId
                        join flight in this.CargoContext.FlightShedules on booking.FlightScheduleId equals flight.Id
                        where flight.FlightDate >= request.beginDate & flight.FlightDate <= request.endDate
                        & flight.SaleState != IDeal.Common.Components.FlightSaleState.Closed
                        select new ReportBookingsPerPeriod
                        {
                            AwbNumder = awb.AcPrefix + "-" + awb.SerialNumber,
                            AwbOrigin = awb.Origin,
                            AwbDestination = awb.Destination,
                            AwbNumberOfPieces = awb.NumberOfPieces,
                            AwbWeight = awb.Weight,
                            AwbVolume = awb.VolumeAmount,
                            AwbAgentId = awb.AgentId,
                            AwbAgent = agent.InternationalName,
                            BookingFlightSceduleId = booking.FlightScheduleId,
                            BookingNumberOfPieces = booking.NumberOfPieces,
                            BookingWeight = booking.Weight,
                            BookingVolume = booking.VolumeAmount,
                            BookingSpaceAllocationCode = booking.SpaceAllocationCode,
                            FlightNumber = flight.Number,
                            FlightDate = flight.FlightDate,
                            FlightOrigin = flight.Origin,
                            FlightDestination = flight.Destination
                        };
            }
            else
            {
                query = from awb in this.CargoContext.Awbs
                        join agent in this.CargoContext.Contragents on awb.Id equals agent.Id
                        join booking in this.CargoContext.Bookings on awb.Id equals booking.AwbId
                        join flight in this.CargoContext.FlightShedules on booking.FlightScheduleId equals flight.Id
                        where awb.AgentId == request.agentId
                        where flight.FlightDate >= request.beginDate & flight.FlightDate <= request.endDate
                        & flight.SaleState != IDeal.Common.Components.FlightSaleState.Closed
                        select new ReportBookingsPerPeriod
                        {
                            AwbNumder = awb.AcPrefix + "-" + awb.SerialNumber,
                            AwbOrigin = awb.Origin,
                            AwbDestination = awb.Destination,
                            AwbNumberOfPieces = awb.NumberOfPieces,
                            AwbWeight = awb.Weight,
                            AwbVolume = awb.VolumeAmount,
                            AwbAgentId = awb.AgentId,
                            AwbAgent = agent.InternationalName,
                            BookingFlightSceduleId = booking.FlightScheduleId,
                            BookingNumberOfPieces = booking.NumberOfPieces,
                            BookingWeight = booking.Weight,
                            BookingVolume = booking.VolumeAmount,
                            BookingSpaceAllocationCode = booking.SpaceAllocationCode,
                            FlightNumber = flight.Number,
                            FlightDate = flight.FlightDate,
                            FlightOrigin = flight.Origin,
                            FlightDestination = flight.Destination
                        };
            }

            var result = GenerateExcelNPOI(query.AsNoTracking().ToList());
            //var result = GenerateExcel(query.AsNoTracking().ToList());
            //File.WriteAllBytes("Report.xlsx", result);
            return await Task.FromResult(result);
        }

        private static byte[] GenerateExcelNPOI(List<ReportBookingsPerPeriod> request)
        {
            byte[] result = null;
            using (MemoryStream ms = new())
            {
                int colsCount = 15;
                XSSFWorkbook workbook = new();
                XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("Бронирования за период");
                ICreationHelper createHelper = workbook.GetCreationHelper();
                var cellsName = new[]
                {
                "AWB",
                "Аэропорт отправления AWB",
                "Аэропорт назначения AWB",
                "Мест по AWB",
                "Вес (кг) по AWB",
                "Объем (м3) по AWB",
                "№ рейса",
                "Дата рейса",
                "Рейс из",
                "Рейс в",
                "Мест по AWB на рейсе",
                "Вес (кг) по AWB на рейсе",
                "Объем (м3) по AWB на рейсе",
                "Статус брони на рейсе",
                "Агент по AWB"
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
                sheet.SetColumnWidth(1, 3500);
                sheet.SetColumnWidth(2, 3500);
                sheet.SetColumnWidth(7, 4500);
                sheet.SetColumnWidth(14, 5000);

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
                    row.CreateCell(0).SetCellValue(data.AwbNumder);
                    row.CreateCell(1).SetCellValue(data.AwbOrigin);
                    row.CreateCell(2).SetCellValue(data.AwbDestination);
                    row.CreateCell(3).SetCellValue(data.AwbNumberOfPieces);
                    row.CreateCell(4).SetCellValue((double)data.AwbWeight);
                    row.CreateCell(5).SetCellValue((double)data.AwbVolume);
                    row.CreateCell(6).SetCellValue(data.FlightNumber);
                    row.CreateCell(7).SetCellValue(data.FlightDate);
                    row.CreateCell(8).SetCellValue(data.FlightOrigin);
                    row.CreateCell(9).SetCellValue(data.FlightDestination);
                    row.CreateCell(10).SetCellValue((double)data.BookingNumberOfPieces);
                    row.CreateCell(11).SetCellValue((double)data.BookingWeight);
                    row.CreateCell(12).SetCellValue((double)data.BookingVolume);
                    row.CreateCell(13).SetCellValue(data.BookingSpaceAllocationCode);
                    row.CreateCell(14).SetCellValue(data.AwbAgent);


                    row.Cells[0].CellStyle = cellStringStyle;
                    row.Cells[6].CellStyle = cellStringStyle;
                    row.Cells[7].CellStyle = cellDateTimeStyle;
                    row.Cells[4].CellStyle = cellNumberStyle;
                    row.Cells[5].CellStyle = cellNumberStyle;
                    row.Cells[11].CellStyle = cellNumberStyle;
                    row.Cells[12].CellStyle = cellNumberStyle;

                    rowIdx++;
                }
                workbook.Write(ms);
                result = ms.ToArray();
            }
            return result;
        }

    }
    internal class ReportBookingsPerPeriod
    {
        public string AwbNumder { get; set; }
        public string AwbOrigin { get; set; }
        public string AwbDestination { get; set; }
        public int AwbNumberOfPieces { get; set; }
        public decimal AwbWeight { get; set; }
        public decimal AwbVolume { get; set; }
        public int? AwbAgentId { get; set; }
        public string AwbAgent { get; set; }
        public decimal BookingNumberOfPieces { get; set; }
        public decimal BookingWeight { get; set; }
        public decimal BookingVolume { get; set; }
        public string BookingSpaceAllocationCode { get; set; }
        public ulong BookingFlightSceduleId { get; set; }
        public string FlightNumber { get; set; }
        public DateTime FlightDate { get; set; }
        public string FlightOrigin { get; set; }
        public string FlightDestination { get; set; }



    }

}
