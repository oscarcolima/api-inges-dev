
//  id INT IDENTITY(1,1) PRIMARY KEY,
//     question VARCHAR(255) NOT NULL,
//     type VARCHAR(10) NOT NULL CHECK (type IN ('complete', 'respond')),
//     rightScore TINYINT NOT NULL CHECK (rightScore BETWEEN 0 AND 100),
//     wrongScore TINYINT NOT NULL CHECK (wrongScore BETWEEN 0 AND 100),
//     create_at DATETIME DEFAULT GETDATE(),
//     update_at DATETIME DEFAULT GETDATE()

namespace api_inges_dev.Models
{
    public class Questions
    {
        public int id { get; set; }
        public required string question { get; set; }
        public required string type { get; set; }
        public byte rightScore { get; set; }
        public byte wrongScore { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }

    }

    public class QuestionsAnswers : Questions
    {
        public List<Answers>? answers { get; set; }

    }
}