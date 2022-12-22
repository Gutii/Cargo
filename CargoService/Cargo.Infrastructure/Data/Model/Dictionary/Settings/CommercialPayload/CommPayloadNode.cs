using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Settings.CommPayloads
{
    /// <summary>
    /// Cтруктура уточненяющих правил полезной коммерческой загрузки (ПКЗ)
    /// </summary>
    public class CommPayloadNode
    {
        /// <summary>
        /// Идентификатор в структуре
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор родителя
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Родитель (для навигации)
        /// </summary>
        public CommPayloadNode Parent { get; set; }

        /// <summary>
        /// Наследники
        /// </summary>
        public ICollection<CommPayloadNode> Childs { get; set; }

        /// <summary>
        /// Ссылка на справочник полезных коммерческих загрузок
        /// </summary>
        public int CommPayloadId { get; set; }

        /// <summary>
        /// Навигационное свойство на объект ПКЗ
        /// </summary>
        public CommPayload CommercialPayload { get; set; }

        /// <summary>
        /// Действие правила по отношению к родителю (замещение, добавление и.т.д.)
        /// </summary>
        public string ActionToParent { get; set; }
    }
}
