using System;

namespace Cargo.Contract.DTOs
{
    /// <summary>
    /// Заголовки сообщений
    /// </summary>
    public class MessageDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Standard Message Identifier : FBL, FWB, FSA...
        /// </summary>
        public string MessageIdentifier { get; set; }
        public ushort VersionNumber { get; set; }

        /// <summary>
        /// Входящее, исходящее
        /// </summary>
        public string Direction { get; set; }
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Отправитель
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        public string To { get; set; }


        public string Text { get; set; }

    }
}
