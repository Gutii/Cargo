using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cargo.Application.Services.Reports;

public static class ReportToExcelArrayBytes
{
    public static byte[] ToExcelArrayBytes(this List<ReportBookingOnFlightsDto> source, string lang)
    {
        byte[] result;
        using (MemoryStream ms = new())
        {
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet(new StringBuilder("Отчет").ToString());
            var createHelper = workbook.GetCreationHelper();
            var cellsName = new ReportHeaders(ReportHeaders.Reports.BookingOnFlights, lang).Names;
            var cellHeaderStyle = workbook.CreateCellStyle();

            cellHeaderStyle.WrapText = true;
            cellHeaderStyle.VerticalAlignment = VerticalAlignment.Center;
            cellHeaderStyle.Alignment = HorizontalAlignment.Center;
            cellHeaderStyle.BorderTop = BorderStyle.Thin;
            cellHeaderStyle.BorderLeft = BorderStyle.Thin;
            cellHeaderStyle.BorderBottom = BorderStyle.Thin;
            cellHeaderStyle.BorderRight = BorderStyle.Thin;

            sheet.SetColumnWidth(0, 4500);
            sheet.SetColumnWidth(1, 4500);

            var i = 0;
            var rowHeader = sheet.CreateRow(0);
            cellsName.ToList().ForEach(ch =>
            {
                rowHeader.CreateCell(i).SetCellValue(ch);
                rowHeader.Cells[i].CellStyle = cellHeaderStyle;
                i++;
            });
            rowHeader.HeightInPoints = 60;
            var cellDateTimeStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            cellDateTimeStyle.SetDataFormat(createHelper.CreateDataFormat().GetFormat("dd.mm.yyyy hh:mm"));
            var cellNumberStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            cellNumberStyle.SetDataFormat(createHelper.CreateDataFormat().GetFormat("# ##0.00"));
            var cellStringStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            cellStringStyle.Alignment = HorizontalAlignment.Center;

            var rowIdx = 1;
            source.ForEach(data =>
            {
                var row = sheet.CreateRow(rowIdx);
                row.CreateCell(0).SetCellValue(data.FlightDate);
                row.CreateCell(1).SetCellValue(data.FlightStDestination);
                row.CreateCell(2).SetCellValue(data.FlightNumber);
                row.CreateCell(3).SetCellValue(data.TypeVs);
                row.CreateCell(4).SetCellValue(data.FlightOrigin);
                row.CreateCell(5).SetCellValue(data.FlightDestination);
                row.CreateCell(6).SetCellValue(data.WeightFact);
                row.CreateCell(7).SetCellValue(data.VolumeFact);
                row.CreateCell(8).SetCellValue(data.WeightPlan);
                row.CreateCell(9).SetCellValue(data.VolumePlan);
                row.CreateCell(10).SetCellValue(data.WeightFact / data.WeightPlan * 100);
                row.CreateCell(11).SetCellValue(data.VolumeFact / data.VolumePlan * 100);

                row.Cells[0].CellStyle = cellDateTimeStyle;
                row.Cells[1].CellStyle = cellDateTimeStyle;
                row.Cells[2].CellStyle = cellStringStyle;
                row.Cells[6].CellStyle = cellNumberStyle;
                row.Cells[7].CellStyle = cellNumberStyle;
                row.Cells[8].CellStyle = cellNumberStyle;
                row.Cells[9].CellStyle = cellNumberStyle;
                row.Cells[10].CellStyle = cellNumberStyle;
                rowIdx++;
            });
            workbook.Write(ms);
            result = ms.ToArray();
        }
        return result;
    }

    public static byte[] ToExcelArrayBytes(this List<ReportBookingsPerPeriodDto> source, string lang)
    {
        byte[] result;
        using (MemoryStream ms = new())
        {
            var colsCount = 15;
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet(new StringBuilder("Отчет").ToString());
            var createHelper = workbook.GetCreationHelper();
            var cellsName = new ReportHeaders(ReportHeaders.Reports.BookingsPerPeriod, lang).Names;
            var cellHeaderStyle = workbook.CreateCellStyle();
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

            var i = 0;
            var rowHeader = sheet.CreateRow(0);
            cellsName.ToList().ForEach(ch =>
            {
                rowHeader.CreateCell(i).SetCellValue(ch);
                rowHeader.Cells[i].CellStyle = cellHeaderStyle;
                i++;
            });
            rowHeader.HeightInPoints = 60;
            var cellDateTimeStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            cellDateTimeStyle.SetDataFormat(createHelper.CreateDataFormat().GetFormat("dd.mm.yyyy hh:mm"));
            var cellNumberStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            cellNumberStyle.SetDataFormat(createHelper.CreateDataFormat().GetFormat("# ##0.00"));
            var cellStringStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            cellStringStyle.Alignment = HorizontalAlignment.Center;

            var rowIdx = 1;
            source.ForEach(data =>
            {
                var row = sheet.CreateRow(rowIdx);
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
            });
            workbook.Write(ms);
            result = ms.ToArray();
        }
        return result;
    }
}