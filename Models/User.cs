namespace HOSTEE.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly BirthDate { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
        public ICollection<TrainingProgram> TrainingPrograms { get; set; }
        public ICollection<Training> Trainings { get; set; }

    }
}
