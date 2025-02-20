using Microsoft.AspNetCore.Identity;

namespace HOSTEE.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        //public string Password { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
        public ICollection<TrainingProgram> TrainingPrograms { get; set; }
        public ICollection<Training> Trainings { get; set; }

        public ICollection<Note> Notes { get; set; }

    }
}
