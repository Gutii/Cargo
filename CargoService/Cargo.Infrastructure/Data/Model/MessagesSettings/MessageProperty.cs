namespace Cargo.Infrastructure.Data.Model.MessageSettings
{
    public class MessageProperty
    {
        public uint id { get; set; }
        public uint MessageIdentifierId { get; set; }
        public int CustomerId { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionRu { get; set; }

        public MessageIdentifier MessageIdentifier { get; set; }
    }
}
