namespace Cargo.Infrastructure.Data
{
    public class ChangeTrack
    {
        public string State { get; set; }
        public string ObjectName { get; set; }
        public string ObjectId { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}