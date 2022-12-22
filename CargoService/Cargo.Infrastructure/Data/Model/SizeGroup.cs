namespace Cargo.Infrastructure.Data.Model
{
    public class SizeGroup
    {
        public int Id { get; set; }
        public int AwbId { get; set; }
        public Awb Awb { get; set; }
        public int Pieces { get; set; }

        /// <summary>
        /// Длина
        /// </summary>
        public double Lenght { get; set; }

        /// <summary>
        /// Ширина
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Высота
        /// </summary>

        public double Height { get; set; }
    }
}
