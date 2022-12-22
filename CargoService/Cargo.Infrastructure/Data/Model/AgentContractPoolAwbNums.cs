using Cargo.Infrastructure.Data.Model.Settings.PoolAwbs;

namespace Cargo.Infrastructure.Data.Model
{
    public class AgentContractPoolAwbNums
    {
        public int AwbPoolId { get; set; }
        public int SerialNumber { get; set; }
        public AgentContractPoolAwb AwbPool { get; set; }

    }
}
