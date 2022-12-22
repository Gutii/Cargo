using Cargo.Contract.DTOs.Bookings;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Cargo.Infrastructure.Report
{

    public class AirportName
    {
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
    }

    public class AwbDtoExtension
    {
        public string path = "";

        public byte[] ToPdf(
            AwbDto awb,
            string path = "",
            bool contract = false,
            int pageCount = 1,
            bool isBlank = false,
            bool asGreed = false)
        {
            const string contractPdf = "FwbBlank/awb_contract.pdf";
            const string airWaybillPdf = "FwbBlank/air_waybill.pdf";
            this.path = path;

            PdfPage contractPage = null;
            if (contract)
            {
                var doc = PdfReader.Open(path + contractPdf, PdfDocumentOpenMode.Import);
                contractPage = doc.Pages[0];
            }
            var pdfDoc = PdfReader.Open(path + airWaybillPdf, PdfDocumentOpenMode.Import);
            var pdfNewDoc = new PdfSharpCore.Pdf.PdfDocument();

            foreach (var page in pdfDoc.Pages)
            {
                DrawPage(awb, pdfNewDoc, page, contractPage, "ORIGINAL 1 (FOR ISSUING CARRIER)", true, isBlank, asGreed);
                if (pageCount < 9)
                    continue;
                DrawPage(awb, pdfNewDoc, page, contractPage, "ORIGINAL 2 (FOR CONSIGNEE)", true, isBlank, asGreed);
                DrawPage(awb, pdfNewDoc, page, contractPage, "ORIGINAL 3 (FOR CONSIGNOR)", true, isBlank, asGreed);
                DrawPage(awb, pdfNewDoc, page, contractPage, "COPY 4 (DELIVERY RECEPT)", false, isBlank, asGreed);
                DrawPage(awb, pdfNewDoc, page, contractPage, "COPY 5 (EXTRA COPY)", false, isBlank, asGreed);
                DrawPage(awb, pdfNewDoc, page, contractPage, "COPY 6 (EXTRA COPY)", false, isBlank, asGreed);
                DrawPage(awb, pdfNewDoc, page, contractPage, "COPY 7 (EXTRA COPY)", false, isBlank, asGreed);
                DrawPage(awb, pdfNewDoc, page, contractPage, "COPY 8 (EXTRA COPY)", false, isBlank, asGreed);
                DrawPage(awb, pdfNewDoc, page, contractPage, "COPY 9 (EXTRA COPY)", false, isBlank, asGreed);
                if (pageCount > 9)
                {
                    DrawPage(awb, pdfNewDoc, page, contractPage, "COPY 10 (EXTRA COPY)", false, isBlank, asGreed);
                    DrawPage(awb, pdfNewDoc, page, contractPage, "COPY 11 (EXTRA COPY)", false, isBlank, asGreed);
                    DrawPage(awb, pdfNewDoc, page, contractPage, "COPY 12 (EXTRA COPY)", false, isBlank, asGreed);
                }
            }

            return GetByteArray(pdfNewDoc);
        }
        private void DrawPage(AwbDto awb,
            PdfSharpCore.Pdf.PdfDocument document,
            PdfPage templatePage,
            PdfPage? contractPage,
            string to,
            bool addContractPage = false,
            bool isBlank = false,
            bool asGreed = false)
        {
            var pp = isBlank ? document.AddPage() : document.AddPage(templatePage);
            Draw(awb, pp, to, isBlank, asGreed);
            if (contractPage != null && addContractPage)
                document.AddPage(contractPage);
            if (!addContractPage)
                document.AddPage();
        }

        private XGraphics Draw(AwbDto awb, PdfPage pp, string to, bool isBlank = false, bool asGreed = false)
        {
            var gfx = XGraphics.FromPdfPage(pp);


            XFont font12 = new("Verdana", 12, XFontStyle.Regular, XPdfFontOptions.UnicodeDefault);
            XFont font10 = new("Verdana", 10, XFontStyle.Regular, XPdfFontOptions.UnicodeDefault);
            XFont font8 = new("Verdana", 8, XFontStyle.Regular, XPdfFontOptions.UnicodeDefault);
            XFont font5 = new("Verdana", 5, XFontStyle.Regular, XPdfFontOptions.UnicodeDefault);

            this.DrawStringMM(gfx, $"{awb.AcPrefix}", font12, 9, 9, 18, 14, XStringFormats.BottomCenter);//1A
            this.DrawStringMM(gfx, $"{awb.Origin}", font12, 19, 9, 31, 14, XStringFormats.BottomCenter);//1
            this.DrawStringMM(gfx, $"{awb.SerialNumber}", font12, 32, 9, 60, 14);//1B

            this.DrawStringMM(gfx, $"{awb.AcPrefix}-{awb.SerialNumber}", font12, 157, 9, 190, 14);//99

            this.DrawStringMM(gfx, $"{awb.Destination}", font12, 9, 101, 18, 106);

            this.DrawStringMM(gfx, $"{awb.Currency}", font12, 100, 101, 108, 106);

            this.DrawStringMM(gfx, $"{awb.NDV}", font12, 144, 101, 167, 106);
            this.DrawStringMM(gfx, $"{awb.NCV}", font12, 169, 101, 195, 106);

            this.DrawStringMM(gfx, $"{awb.Consignor?.NameEn}", font8, 9, 21, 98, 26);
            this.DrawStringMM(gfx, $"{awb.Consignor?.AddressEn}", font8, 9, 24, 98, 29);
            this.DrawStringMM(gfx, $"{awb.Consignor?.CityEn} {awb.Consignor.RegionEn}", font8, 9, 27, 98, 42);
            this.DrawStringMM(gfx, $"{awb.Consignor?.CountryISOInfo?.EnglishShortName} {awb.Consignor?.CountryISO} {awb.Consignor?.ZipCode}", font8, 9, 30, 98, 45);
            this.DrawStringMM(gfx, $"PH:{awb.Consignor?.Phone} FAX:{awb.Consignor?.Fax}", font8, 9, 33, 98, 45);

            this.DrawStringMM(gfx, $"{awb.Consignee?.NameEn}", font8, 9, 47, 98, 52);
            this.DrawStringMM(gfx, $"{awb.Consignee?.AddressEn}", font8, 9, 50, 98, 58);
            this.DrawStringMM(gfx, $"{awb.Consignee?.CityEn} {awb.Consignee?.RegionEn}", font8, 9, 53, 98, 63);
            this.DrawStringMM(gfx, $"{awb.Consignee?.CountryISOInfo?.EnglishShortName} {awb.Consignee.CountryISO} {awb.Consignee.ZipCode}", font8, 9, 56, 98, 63);
            this.DrawStringMM(gfx, $"PH:{awb.Consignee?.Phone} FAX:{awb.Consignee?.Fax}", font8, 9, 59, 98, 63);

            if (awb.Carrier != null)
            {
                this.DrawStringMM(gfx, $"{awb.Carrier.Name}", font5, 176, 18, 196, 32);
                this.DrawStringMM(gfx, $"{awb.Carrier.IataCode}", font5, 176, 21, 196, 32);
                this.DrawStringMM(gfx, $"{awb.Carrier.Email}", font5, 176, 24, 196, 32);
            }

            this.DrawStringMM(gfx, $"{awb.Agent.NameEn}", font8, 9, 69, 98, 74);
            this.DrawStringMM(gfx, $"{awb.Agent.CityEn} {awb.Consignor.RegionEn}", font8, 9, 72, 98, 80);

            this.DrawStringMM(gfx, $"{awb.Agent.IataCode}", font12, 9, 86, 53, 89); //{awb.Consignor.RegionEn}

            this.DrawStringMM(gfx, $"{awb.OriginInfo?.Name}", font8, 9, 93, 99, 97);

            int count = 0;
            foreach (var str in MaxCountCharToString(awb.DestinationInfo?.Name, 27))
            {
                this.DrawStringMM(gfx, $"{str}", font8, 9, 110 + (count * 3), 49, 117);
                count++;
            }




            this.DrawStringMM(gfx, $"{awb.SpecialServiceRequest}", font8, 9, 123, 195, 123);



            var firstBooking = awb.Bookings?.FirstOrDefault(b => b.FlightSchedule != null && b.FlightSchedule.Origin == awb.Origin);
            if (firstBooking?.FlightSchedule != null)
            {
                var car = firstBooking.FlightSchedule.Number[..2];

                if (!isBlank)
                {
                    if (awb.AcPrefix == "555")
                    {
                        PdfSharpCore.Drawing.XImage image = PdfSharpCore.Drawing.XImage.FromFile(path + "FwbBlank/afl_logo.jpg");
                        gfx.DrawImage(image, XUnit.FromMillimeter(115).Point, XUnit.FromMillimeter(17.5).Point, XUnit.FromMillimeter(60).Point, XUnit.FromMillimeter(14).Point);
                    } /*else if (car == "FV")
                {
                    XImage image = XImage.FromFile("wwwroot/assets/RussiaLogo.jpg");
                    gfx.DrawImage(image, XUnit.FromMillimeter(125).Point, XUnit.FromMillimeter(16).Point, XUnit.FromMillimeter(60).Point, XUnit.FromMillimeter(14).Point);
                } else if (car == "HZ")
                {
                    XImage image = XImage.FromFile("wwwroot/assets/hz_logo.jpg");
                    gfx.DrawImage(image, XUnit.FromMillimeter(125).Point, XUnit.FromMillimeter(16).Point, XUnit.FromMillimeter(60).Point, XUnit.FromMillimeter(14).Point);
                }*/
                }

                this.DrawStringMM(gfx, car, font12, 19, 101, 33, 106, XStringFormats.BottomCenter);
                this.DrawStringMM(gfx, $"{firstBooking.FlightSchedule.Destination}", font12, 34, 101, 64, 106, XStringFormats.BottomCenter);
                this.DrawStringMM(gfx, $"{firstBooking.FlightSchedule.Number}", font12, 52, 112, 77, 118, XStringFormats.BottomCenter);
                this.DrawStringMM(gfx, $"{firstBooking.FlightSchedule.FlightDate?.ToString("ddMMM", CultureInfo.CreateSpecificCulture("en-US")).ToUpper() ?? ""}", font12, 77, 112, 100, 118, XStringFormats.BottomCenter);
            }

            this.DrawStringMM(gfx, $"XXX", font12, 100, 110, 128, 118, XStringFormats.BottomCenter);

            this.DrawStringMM(gfx, $"{awb.NumberOfPieces}", font10, 9, 141, 18, 146);
            this.DrawStringMM(gfx, $"{awb.Weight:F}", font10, 19, 141, 36, 146);
            this.DrawStringMM(gfx, $"{awb.WeightCode}", font10, 36.7, 141, 38, 146);
            this.DrawStringMM(gfx, $"{awb.TariffClass}", font10, 41.9, 141, 44, 146);
            this.DrawStringMM(gfx, $"{awb.ChargeWeight:F}", font10, 65, 141, 81, 146);
            if (asGreed)
            {
                this.DrawStringMM(gfx, "AS AGREED", font10, 108, 141, 137, 146);
                this.DrawStringMM(gfx, "AS AGREED", font10, 108, 189, 137, 194);
            }
            else
            {
                this.DrawStringMM(gfx, $"{awb.BaseTariffRate:F}", font10, 45, 141, 64, 146);


                this.DrawStringMM(gfx, $"{awb.TariffRate:F}", font10, 85, 141, 104, 146);
                this.DrawStringMM(gfx, $"{awb.Total:F}", font10, 108, 141, 137, 146);


                this.DrawStringMM(gfx, $"{awb.TariffClass}", font10, 41.9, 189, 44, 194);
                this.DrawStringMM(gfx, $"{awb.Total:F}", font10, 108, 189, 137, 194);

            }
            this.DrawStringMM(gfx, $"{awb.ManifestDescriptionOfGoods}", font10, 141, 141, 196, 146);
            this.DrawStringMM(gfx, $"VOL {awb.VolumeAmount:F} {awb.VolumeCode}", font10, 141, 148, 196, 152);

            this.DrawStringMM(gfx, $"{awb.NumberOfPieces}", font10, 9, 189, 18, 194);
            this.DrawStringMM(gfx, $"{awb.Weight:F}", font10, 19, 189, 36, 194);
            this.DrawStringMM(gfx, $"{awb.WeightCode}", font10, 36.7, 189, 38, 194);

            if (asGreed)
            {
                this.DrawStringMM(gfx, "AS AGREED", font10, 9, 198, 41, 202);
                this.DrawStringMM(gfx, "AS AGREED", font10, 9, 246, 41, 250);
            }
            else
            {
                //TAX
                if (awb.Charge.Prepaid?.Charge > 0)
                    this.DrawStringMM(gfx, $"{awb.Charge.Prepaid?.Charge:F}", font10, 9, 198, 41, 202);
                if (awb.Charge.Collect?.Charge > 0)
                    this.DrawStringMM(gfx, $"{awb.Charge.Collect?.Charge:F}", font10, 44, 198, 78, 202);

                if (awb.Charge.Prepaid?.ValuationCharge > 0)
                    this.DrawStringMM(gfx, $"{awb.Charge.Prepaid?.ValuationCharge:F}", font10, 9, 208, 41, 212);
                if (awb.Charge.Collect?.ValuationCharge > 0)
                    this.DrawStringMM(gfx, $"{awb.Charge.Collect?.ValuationCharge:F}", font10, 44, 208, 78, 212);

                if (awb.Charge.Prepaid?.Tax > 0)
                    this.DrawStringMM(gfx, $"{awb.Charge.Prepaid?.Tax:F}", font10, 9, 216, 41, 220);
                if (awb.Charge.Collect?.Tax > 0)
                    this.DrawStringMM(gfx, $"{awb.Charge.Collect?.Tax:F}", font10, 44, 216, 78, 220);

                if (awb.Charge.Prepaid?.TotalOtherChargesDueAgent > 0)
                    this.DrawStringMM(gfx, $"{awb.Charge.Prepaid?.TotalOtherChargesDueAgent:F}", font10, 9, 224, 41, 228);
                if (awb.Charge.Collect?.TotalOtherChargesDueAgent > 0)
                    this.DrawStringMM(gfx, $"{awb.Charge.Collect?.TotalOtherChargesDueAgent:F}", font10, 44, 224, 78, 228);

                if (awb.Charge.Prepaid?.TotalOtherChargesDueCarrier > 0)
                    this.DrawStringMM(gfx, $"{awb.Charge.Prepaid?.TotalOtherChargesDueCarrier:F}", font10, 9, 233, 41, 236);
                if (awb.Charge.Collect?.TotalOtherChargesDueCarrier > 0)
                    this.DrawStringMM(gfx, $"{awb.Charge.Collect?.TotalOtherChargesDueCarrier:F}", font10, 44, 233, 78, 236);

                if (awb.Charge.Prepaid?.Total > 0)
                    this.DrawStringMM(gfx, $"{awb.Charge.Prepaid?.Total:F}", font10, 9, 246, 41, 250);
                if (awb.Charge.Collect?.Total > 0)
                    this.DrawStringMM(gfx, $"{awb.Charge.Collect?.Total:F}", font10, 44, 246, 78, 250);
            }

            var dateTime = DateTime.Now;
            var day = dateTime.Day;
            var mounth = DateTime.Now.ToString("MMMM", new CultureInfo("en-US")).ToUpper().Substring(0, 3);
            var year = dateTime.Year.ToString();
            year = year.Substring(year.Length - 2);

            this.DrawStringMM(gfx, $"{day}/{mounth}/{year}", font8, 81, 250, 121, 254);
            this.DrawStringMM(gfx, $"{awb.OriginInfo?.Name}", font8, 108, 250, 118, 260);

            this.DrawStringMM(gfx, $"{awb.AcPrefix}-{awb.SerialNumber}", font12, 157, 260, 190, 266);

            this.DrawStringMM(gfx, to, font8, 118, 268, 196, 270);
            return gfx;
        }

        private void DrawStringMM(XGraphics gfx, string text, XFont font, double x1, double y1, double x2, double y2)
        {

            var rec = new XRect(
                XUnit.FromMillimeter(x1).Point,
                XUnit.FromMillimeter(y1).Point,
                XUnit.FromMillimeter(x2 - x1).Point,
                XUnit.FromMillimeter(y2 - y1).Point);

            gfx.DrawString(text, font, XBrushes.Black, rec, XStringFormats.TopLeft);
        }

        private void DrawStringMM(XGraphics gfx, string text, XFont font, double x1, double y1, double x2, double y2, XStringFormat format)
        {

            var rec = new XRect(
                XUnit.FromMillimeter(x1).Point,
                XUnit.FromMillimeter(y1).Point,
                XUnit.FromMillimeter(x2 - x1).Point,
                XUnit.FromMillimeter(y2 - y1).Point);

            gfx.DrawString(text, font, XBrushes.Black, rec, format);
        }

        private string[] MaxCountCharToString(string thisString, int maxChar)
        {
            var maxStr = thisString.Split(Environment.NewLine);
            string[] res = new string[0];
            foreach (string str in maxStr)
            {
                if (maxChar < str.Length)
                {
                    var words = str.Split(' ');
                    int countChar = 0;
                    for (int i = 0; i <= words.Length; i++)
                    {
                        countChar += words[i].Length + 1;
                        if (countChar > maxChar)
                        {
                            string stringUp = string.Join(' ', words, 0, i);
                            if (words.Length > i)
                            {
                                string stringDown = string.Join(' ', words, i, words.Length - i);
                                res = res.Append(stringUp).ToArray();
                                res = res.Append(stringDown).ToArray();
                                break;
                            }
                            else
                            {
                                res = res.Append(stringUp).ToArray();
                                break;
                            }
                        }
                    }

                }
                else
                {
                    res = res.Append(str).ToArray();
                }
            }
            return res;

        }

        private byte[] GetByteArray(PdfSharpCore.Pdf.PdfDocument pdfNewDoc)
        {
            using var ms = new MemoryStream();
            pdfNewDoc.Save(ms, false);
            byte[] buffer = new byte[ms.Length];
            ms.Seek(0, SeekOrigin.Begin);
            ms.Flush();
            ms.Read(buffer, 0, (int)ms.Length);
            return buffer;
        }

    }
}
