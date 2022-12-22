namespace Cargo.Contract.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string IataCode { get; set; }
        public string AcPrefix { get; set; }
        public string AcMailPrefix { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
