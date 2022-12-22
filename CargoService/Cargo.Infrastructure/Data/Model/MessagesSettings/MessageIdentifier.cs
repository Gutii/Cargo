using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.MessageSettings
{
    public class MessageIdentifier
    {
        public uint id { get; set; }
        public string Identifier { get; set; }
        public byte ch { get; set; }
        public bool cc { get; set; }
        public bool hh { get; set; }

        public ICollection<MessageProperty> MessageProperties { get; set; }
    }
}
