//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HOSTEE.Models;

namespace HOSTEE
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Folder> Folders { get; set; }

        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Training> TrainingSessions { get; set; }
        public DbSet<ProgramExercise> ProgramExercises { get; set; }
        public DbSet<TrainingExercise> TrainingExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Exercise>().HasKey(e => e.Id);
            builder.Entity<Exercise>()
                .HasMany(e => e.Programs)
                .WithOne(pe => pe.Exercise)
                .HasForeignKey(e => e.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Exercise>()
                .HasMany(e => e.Trainings)
                .WithOne(te  => te.Exercise)
                .HasForeignKey(te => te.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TrainingProgram>().HasKey(p => p.Id);
            builder.Entity<TrainingProgram>()
                .HasMany(p => p.Exercises)
                .WithOne(pe => pe.TrainingProgram)
                .HasForeignKey(pe => pe.TrainingProgramId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TrainingProgram>()
                .HasMany(p => p.Trainings)
                .WithOne(t => t.TrainingProgram)
                .HasForeignKey(t => t.TrainingProgramId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProgramExercise>().HasKey(pe => new {pe.TrainingProgramId, pe.ExerciseId});

            builder.Entity<Training>().HasKey(t => t.Id);
            builder.Entity<Training>()
                .HasMany(t => t.Exercises)
                .WithOne(te => te.Training)
                .HasForeignKey(te => te.TrainingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TrainingExercise>().HasKey(te => new {te.TrainingId, te.ExerciseId});

            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>()
                .HasMany(u => u.Exercises)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(u => u.TrainingPrograms)
                .WithOne(tp => tp.User)
                .HasForeignKey(tp => tp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(u => u.Trainings)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }

    }
}
