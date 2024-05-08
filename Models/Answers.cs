// id INT IDENTITY(1,1) PRIMARY KEY,
//     answer VARCHAR(255) NOT NULL,
//     fk_question INT NOT NULL,
//     iscorrect bit NOT NULL DEFAULT 0,
//     create_at DATETIME DEFAULT GETDATE(),
//     update_at DATETIME DEFAULT GETDATE(),

namespace api_inges_dev.Models
{
    public class Answers
    {
        public int id { get; set; }
        public required string answer { get; set; }
        public int fk_question { get; set; }
        public bool iscorrect { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }

    }
}