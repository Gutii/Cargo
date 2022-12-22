using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Settings.PoolAwbs
{
    /// <summary>
    /// Pool Awbs
    /// </summary>
    public class AgentContractPoolAwb
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identifier Contract
        /// </summary>
        public int ContractId { get; set; }

        /// <summary>
        /// First Number of Pool
        /// </summary>
        public int StartNumber { get; set; }

        /// <summary>
        /// Lenght pool
        /// </summary>
        public int PoolLenght { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Collection created AWBs
        /// </summary>
        public ICollection<AgentContractPoolAwbNums> UsedAwbNumbers { get; set; }

        /// <summary>
        /// Договор
        /// </summary>
        public AgentContract Contract { get; set; }
    }
}
