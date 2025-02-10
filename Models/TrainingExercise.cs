namespace HOSTEE.Models
{
    public class TrainingExercise
    {
        public int TrainingId { get; set; }
        public Training Training { get; set; }

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public int Sets { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }
        public Boolean Completed { get; set; }
        public string Comments { get; set; }


    }
}
