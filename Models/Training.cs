namespace HOSTEE.Models
{
    public class Training
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }
        public int? TrainingProgramId { get; set; }
        public TrainingProgram? TrainingProgram { get; set; }

        public ICollection<TrainingExercise> Exercises { get; set; }
    }
}
