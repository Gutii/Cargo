using Cargo.Contract.DTOs.Tariffs;
using Cargo.ServiceHost.Transmittable.Outgoing;
using IDeal.Common.Components.Paginator;

namespace Cargo.ServiceHost.Transmittable.Payloads
{
    public class XmtPayloadRateGridPage : XmtPayloadBase
    {
        PagedResult<RateGridDto> mPage = null;

        public XmtPayloadRateGridPage() : base()
        {
        }

        public XmtPayloadRateGridPage(PagedResult<RateGridDto> pg) : this()
        {
            page = pg;
        }

        public PagedResult<RateGridDto> page
        {
            get => mPage;
            set => mPage = value;
        }
    }
}