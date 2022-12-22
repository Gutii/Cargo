using Cargo.Contract.Commands;
using Cargo.Contract.DTOs.Bookings;
using FluentValidation;
using IDeal.Common.Components;
using System.Linq;

namespace Cargo.Application.Validation
{
    public sealed class SaveAwbCommandValidator : AbstractValidator<SaveAwbCommand>
    {
        public SaveAwbCommandValidator()
        {
            When(awb => awb.Status == StatusAwb.Draft.Value || string.IsNullOrEmpty(awb.Status), () =>
            {
                RuleFor(a => a.Awb).NotNull();
                //  RuleFor(a => a.Awb.PoolAwbNumId).NotNull();
                RuleFor(a => a.Awb.AcPrefix).NotNull().Matches(@"^\d{3}$");
                RuleFor(a => a.Awb.SerialNumber).NotNull().Matches(@"^\d{8}$");
                RuleFor(a => a.Awb.QuanDetShipmentDescriptionCode).NotNull().Length(1);
                RuleFor(a => a.Awb.NumberOfPieces).GreaterThan(-1).LessThan(9999);
                RuleFor(a => a.Awb.WeightCode).NotNull().Length(1);
                RuleFor(a => a.Awb.Weight).GreaterThan(-1).LessThan(9999999);
                RuleFor(a => a.Awb.VolumeCode).NotNull().Length(2);
                RuleFor(a => a.Awb.VolumeAmount).GreaterThan(-1).LessThan(9999999);
                RuleFor(a => a.Awb.Product).MaximumLength(16);
                RuleFor(a => a.Awb.SpecialHandlingRequirements).Matches(@"^(\/[A-Z0-9]{3})+$").MaximumLength(36);
            });

            When(awb => awb.Status == StatusAwb.Booking.Value, () =>
            {
                RuleFor(a => a.Awb).NotNull();
                // RuleFor(a => a.Awb.PoolAwbNumId).NotNull();
                RuleFor(a => a.Awb.AcPrefix).NotNull().Matches(@"^\d{3}$");
                RuleFor(a => a.Awb.SerialNumber).NotNull().Matches(@"^\d{8}$");
                // RuleFor(a => a.Awb.ForwardingAgentId).NotNull().GreaterThan(0);
                //RuleFor(a => a.Awb.ForwardingAgent).NotNull().NotEmpty().MaximumLength(17);
                RuleFor(a => a.Awb.Origin).NotNull().Length(3);
                RuleFor(a => a.Awb.Destination).NotNull().Length(3);
                RuleFor(a => a.Awb.ManifestDescriptionOfGoods).NotNull().NotEmpty();
                //RuleFor(a => a.Awb.ManifestDescriptionOfGoodsRu).NotNull();
                RuleFor(a => a.Awb.SpecialHandlingRequirements).Matches(@"^(\/[A-Z0-9]{3})+$").MaximumLength(36);
                RuleFor(a => a.Awb.QuanDetShipmentDescriptionCode).NotNull().Length(1);
                RuleFor(a => a.Awb.NumberOfPieces).NotNull().GreaterThan(0).LessThan(9999);
                RuleFor(a => a.Awb.WeightCode).NotNull().Length(1);
                RuleFor(a => a.Awb.Weight).NotNull().GreaterThan(0).LessThan(9999999);
                RuleFor(a => a.Awb.VolumeCode).NotNull().Length(2);
                RuleFor(a => a.Awb.VolumeAmount).NotNull().GreaterThan(0).LessThan(9999999);
                RuleFor(a => a.Awb.Product).NotNull().NotEmpty().MaximumLength(16);
                RuleFor(a => a.Awb.Bookings).NotNull().Must(bs => bs != null && bs.Any());
                //RuleFor(a => a.Awb.Consignee).SetValidator(new ContragentAwbValidator()).When(a => a.Awb.Consignee != null);
                //RuleFor(a => a.Awb.Consignor).SetValidator(new ContragentAwbValidator()).When(a => a.Awb.Consignor != null);
                //RuleFor(a => a.Awb.Agent).SetValidator(new ContragentAwbValidator()).When(a => a.Awb.Agent != null);
                RuleForEach(x => x.Awb.Bookings).SetValidator(new BookingDtoValidator());
            });

            When(awb => awb.Status == StatusAwb.Cargo.Value, () =>
            {
                RuleFor(a => a.Awb).NotNull();
                // RuleFor(a => a.Awb.PoolAwbNumId).NotNull();
                RuleFor(a => a.Awb.AcPrefix).NotNull().Matches(@"^\d{3}$");
                RuleFor(a => a.Awb.SerialNumber).NotNull().Matches(@"^\d{8}$");
                RuleFor(a => a.Awb.Origin).NotNull().Length(3);
                RuleFor(a => a.Awb.Destination).NotNull().Length(3);
                RuleFor(a => a.Awb.ManifestDescriptionOfGoods).NotNull().NotEmpty();
                //RuleFor(a => a.Awb.ManifestDescriptionOfGoodsRu).NotNull();
                RuleFor(a => a.Awb.SpecialHandlingRequirements).Matches(@"^(\/[A-Z0-9]{3})+$").MaximumLength(36);
                RuleFor(a => a.Awb.QuanDetShipmentDescriptionCode).NotNull().Length(1);
                RuleFor(a => a.Awb.NumberOfPieces).NotNull().GreaterThan(0).LessThan(9999);
                RuleFor(a => a.Awb.WeightCode).NotNull().Length(1);
                RuleFor(a => a.Awb.Weight).NotNull().GreaterThan(0).LessThan(9999999);
                RuleFor(a => a.Awb.VolumeCode).NotNull().Length(2);
                RuleFor(a => a.Awb.VolumeAmount).NotNull().GreaterThan(0).LessThan(9999999);
                RuleFor(a => a.Awb.Product).NotNull().NotEmpty().MaximumLength(16);
                RuleFor(a => a.Awb.Bookings).NotNull().Must(bs => bs != null && bs.Any());
                RuleFor(a => a.Awb.Consignee).NotNull().SetValidator(new ContragentAwbValidator());
                RuleFor(a => a.Awb.Consignor).NotNull().SetValidator(new ContragentAwbValidator());
                //RuleFor(a => a.Awb.Agent).SetValidator(new ContragentAwbValidator()).When(a => a.Awb.Agent != null);
                RuleForEach(x => x.Awb.Bookings).SetValidator(new BookingDtoValidator());
            });
        }
    }

    public class BookingDtoValidator : AbstractValidator<BookingDto>
    {
        public BookingDtoValidator()
        {
            //   RuleFor(b => b.Awb).Null();
            //  RuleFor(b => b.AwbId).Equal(0);
            //RuleFor(b => b.FlightSchedule).Null();
            // RuleFor(b => b.FlightScheduleId).NotNull().GreaterThan((ulong)0);
            //RuleFor(b => b.QuanDetShipmentDescriptionCode).Null();
            RuleFor(b => b.NumberOfPieces).NotNull().GreaterThan(0).LessThan(9999);
            //RuleFor(b => b.WeightCode).Null();
            RuleFor(b => b.Weight).GreaterThan(0).NotNull().LessThan(9999999);
            //RuleFor(b => b.VolumeCode).Null();
            RuleFor(b => b.VolumeAmount).GreaterThan(0).NotNull().LessThan(9999999);
            // RuleFor(b => b.SpaceAllocationCode).Null();
        }
    }

    public class ContragentAwbValidator : AbstractValidator<AwbContragentDto>
    {
        public ContragentAwbValidator()
        {
            //RuleFor(p => p.NameRu).MaximumLength(30);
            RuleFor(p => p.NameEn).NotNull().NotEmpty();
            //RuleFor(p => p.NameExRu).MaximumLength(50);
            //RuleFor(p => p.NameExEn).MaximumLength(50);
            //RuleFor(p => p.CityRu).MaximumLength(15);
            RuleFor(p => p.CityEn).NotNull().NotEmpty();
            RuleFor(p => p.CountryISO).NotNull().NotEmpty();
            // RuleFor(p => p.ZipCode).MaximumLength(9);
            // RuleFor(p => p.Passport).MaximumLength(20);
            //RuleFor(p => p.RegionRu).MaximumLength(30);
            // RuleFor(p => p.RegionEn).MaximumLength(30);
            // RuleFor(p => p.CodeEn).MaximumLength(9);
            // RuleFor(p => p.Phone).MaximumLength(17);
            // RuleFor(p => p.Fax).MaximumLength(17);
            //RuleFor(p => p.AddressRu).MaximumLength(70);
            RuleFor(p => p.AddressEn).NotNull().NotEmpty();
            //RuleFor(p => p.AgentCass).MaximumLength(15);
            //RuleFor(p => p.IataCode).MaximumLength(15);
            //RuleFor(a => a.Email).MaximumLength(25);
        }
    }
}
