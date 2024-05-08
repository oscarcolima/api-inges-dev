

namespace api_inges_dev.Models
{
    public class Technologies
    {
        public int Id { get; set; }
        public required string technology { get; set; }
        public string? Icon { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }
    }
}