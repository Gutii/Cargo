namespace Cargo.Contract.DTOs.Bookings
{
    public class SizeGroupDto
    {
        public int? Id { get; set; }
        public int Pieces { get; set; }

        /// <summary>
        /// Длина
        /// </summary>
        public double Length { get; set; }

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
