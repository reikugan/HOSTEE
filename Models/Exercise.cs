using System.ComponentModel.DataAnnotations;

namespace HOSTEE.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Type { get; set; }
        public string MuscleGroup { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<ProgramExercise> Programs { get; set; }
        public ICollection<TrainingExercise> Trainings { get; set; }


        public Exercise()
        {
            Programs = new List<ProgramExercise>();
            Trainings = new List<TrainingExercise>();
        }
    }
}
