namespace Cargo.Contract.DTOs
{
    public class ChangeDto
    {
        public string Id { get; set; }
        public string State { get; set; }
        public string ObjectName { get; set; }
        public string ObjectId { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
