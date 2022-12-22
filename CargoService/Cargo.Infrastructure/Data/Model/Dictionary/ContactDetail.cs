namespace Cargo.Infrastructure.Data.Model.Dictionary
{
    public class ContactDetail
    {
        public int Id { get; set; }
        public string ContactIdentifier { get; set; }
        public string ContactNumber { get; set; }

        public int ContragentId { get; set; }
        public Contragent Contragent { get; set; }
    }
}
