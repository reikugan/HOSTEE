using HOSTEE.Models;
using Microsoft.EntityFrameworkCore;
namespace HOSTEE.Services
{
    public class WorkoutServices
    {
        public AppDbContext _context;

        public WorkoutServices(AppDbContext context)
        {
            _context = context;
        }

        // EXERCISE CRUD:
        public async Task<IEnumerable<Exercise>> GetUserExercisesAsync(string userId)
        {
            return await _context.Exercises.Where(e => e.UserId == userId).ToListAsync();
        }

        public async Task<Exercise> CreateExerciseAsync(Exercise ex)
        {
            _context.Exercises.Add(ex);
            await _context.SaveChangesAsync();
            return ex;
        }

        public async Task DeleteExerciseAsync(int exId, string userId)
        {
            var exercise = await _context.Exercises.Where(ex => ex.Id == exId && ex.UserId == userId).FirstOrDefaultAsync();
            if (exercise != null)
            {
                Console.WriteLine($"Removing exercise: {exercise.Name}, {exercise.Id}");
                _context.Exercises.Remove(exercise);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateExerciseAsync(Exercise exercise)
        {
            var ex = await _context.Exercises.FindAsync(exercise.Id);
            if (ex != null)
            {
                ex.Name = exercise.Name;
                ex.Description = exercise.Description;
                ex.Type = exercise.Type;
                ex.MuscleGroup = exercise.MuscleGroup;

                await _context.SaveChangesAsync();
            }
        }


        //Training Program CRUD:
        public async Task<IEnumerable<TrainingProgram>> GetUserProgramsAsync(string userId)
        {
            return await _context.TrainingPrograms.Where(tp => tp.UserId == userId)
                .Include(tp => tp.Exercises).ThenInclude(pe => pe.Exercise)
                .Include(tp => tp.Trainings)
                .ToListAsync();
        }


        public async Task<TrainingProgram> CreateProgramAsync(TrainingProgram program, List<Exercise> exList)
        {
            program.CreatedDate = DateTime.UtcNow;

            _context.TrainingPrograms.Add(program);

            foreach (Exercise ex in exList)
            {
                _context.ProgramExercises.Add(new ProgramExercise
                {
                    TrainingProgramId = program.Id,
                    ExerciseId = ex.Id,
                    TrainingProgram = program,
                    Exercise = ex
                });
            }
            _context.SaveChangesAsync();
            return program;
        }

        public async Task UpdateProgramAsync(TrainingProgram program, List<int> newExercises) 
        {
            var oldProgram = await _context.TrainingPrograms
                .Include(tp => tp.Exercises)
                .FirstOrDefaultAsync(tp => tp.Id == program.Id && tp.UserId == program.UserId);
            
            if (oldProgram != null)
            {
                oldProgram.Name = program.Name;
                oldProgram.Description = program.Description;
                oldProgram.CreatedDate = DateTime.UtcNow;

                _context.ProgramExercises.RemoveRange(oldProgram.Exercises);

                foreach (int ex in newExercises)
                {
                    _context.ProgramExercises.Add(new ProgramExercise
                    {
                        TrainingProgramId = program.Id,
                        ExerciseId = ex
                    });
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Training Program {program.Id} not found");
            }
           
        }

        public async Task DeleteProgramAsync(int programId, string userId)
        {
            var tp = await _context.TrainingPrograms
                .Include(tp => tp.Exercises)
                .FirstOrDefaultAsync(tp => tp.Id == programId && tp.UserId == userId);

            if(tp != null)
            {
                _context.ProgramExercises.RemoveRange(tp.Exercises);
                _context.TrainingPrograms.Remove(tp);

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Training program {programId} not found");
            }
            
        }


        //Training CRUD:
        public async Task SaveTrainingAsync(Training training, List<TrainingExercise> exercises)
        {
            training.Date = DateTime.UtcNow;

            if (training.TrainingProgramId.HasValue)
            {
                var program = await _context.TrainingPrograms.FirstOrDefaultAsync(tp => tp.Id == training.TrainingProgramId && tp.UserId == training.UserId);
                training.TrainingProgram = program;
            }

            training.Id = 0;
            _context.TrainingSessions.Add(training);
            await _context.SaveChangesAsync();

            foreach (var ex in exercises)
            {
                ex.TrainingId = training.Id;
                _context.TrainingExercises.Add(ex);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Training>> GetUserTrainingSessionsAsync(string userId)
        {
            var trainings = await _context.TrainingSessions
                .Where(t => t.UserId == userId)
                .Include(t => t.Exercises)
                .ToListAsync();

            if(trainings.Any())
            {
                return trainings;
            }
            else
            {
                throw new Exception($"No Training sessions found for user {userId}");
            }
        }

        public async Task<IEnumerable<TrainingExercise>> GetCompletedExercisesAsync(int exerciseId, string userId)
        {
            var ex = await _context.TrainingExercises
                .Where(te => te.Training.UserId == userId && te.ExerciseId == exerciseId)
                .Include(te => te.Training)
                .ToListAsync();

            return ex;
        }


    }
    
    
}
