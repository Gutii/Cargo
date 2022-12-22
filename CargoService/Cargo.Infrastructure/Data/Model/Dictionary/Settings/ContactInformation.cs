namespace Cargo.Infrastructure.Data.Model.Dictionary.Settings
{
    public class ContactInformation
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }

        public string Email1 { get; set; }
        public string Email2 { get; set; }

        public int AgentId { get; set; }
        public Agent Agent { get; set; }
    }
}
