using System.ComponentModel.DataAnnotations;

namespace HOSTEE.Models
{
    public class TrainingProgram
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        //public string Author { get; set; }
        public int UserId   { get; set; }
        public User User { get; set; }

        public ICollection<ProgramExercise> Exercises { get; set; }
        public ICollection<Training> Trainings { get; set; }

        public TrainingProgram()
        {
            Exercises = new List<ProgramExercise>();
            Trainings = new List<Training>();
        }
    }
}
