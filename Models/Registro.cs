
namespace api_inges_dev.Models
{
    public class Registro
    {
        public int id { get; set; }
        public required string nombre_completo { get; set; }
        public required string telefono { get; set; }
        public required string email { get; set; }
        public required string profesion { get; set; }
        public required string experiencia { get; set; }
        public required bool eliminado { get; set; }
        public required int fk_technology { get; set; }
        public required int? score { get; set; }
        public required byte? correct_answers { get; set; }
        public required string? level { get; set; }
        public DateTime? date { get; set; }


    }
}