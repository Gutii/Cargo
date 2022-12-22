using Cargo.Infrastructure.Data.Model.Dictionary.Settings;
using System;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Settings.PoolAwbs
{
    /// <summary>
    /// Договор на продажу бронирования
    /// </summary>
    public class AgentContract
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sale Agent Id
        /// </summary>
        public int SaleAgentId { get; set; }

        /// <summary>
        /// Агент
        /// </summary>
        public Agent SaleAgent { get; set; }

        /// <summary>
        /// Канал продаж
        /// </summary>
        public string SalesChannel { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Date open contract
        /// </summary>
        public DateTime DateAt { get; set; }

        /// <summary>
        /// Date close contract
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Collection created AWBs
        /// </summary>
        public ICollection<AgentContractPoolAwb> PoolAwbs { get; set; }

    }
}
