namespace HOSTEE.Models
{
    public class ProgramExercise
    {
        public int TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
