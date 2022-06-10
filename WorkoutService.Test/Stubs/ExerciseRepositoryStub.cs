using WorkoutService.Interfaces;
using WorkoutService.Models;

namespace WorkoutService.Stubs
{
    public class ExerciseRepositoryStub : IExerciseRepository
    {
        private List<Exercise> _exercises = new List<Exercise>();

        public ExerciseRepositoryStub()
        {
            var exerciseCount = 100;

            for (var i = 0; i < exerciseCount; i++)
            {
                _exercises.Add(new Exercise
                {
                    Id = i,
                    Name = $"Exercise {i}",
                    Sets = 3,
                    Reps = 12,
                    CreatedAt = new DateTime(2020, 1, 1)
                });
            }
        }

        public List<Exercise> Get()
        {
            return _exercises;
        }

        public Exercise? Get(int id)
        {
            return _exercises.FirstOrDefault(e => e.Id == id);
        }

        public Exercise Post(Exercise exercise)
        {
            return new Exercise
            {
                Id = _exercises.Count + 1,
                Name = exercise.Name,
                Sets = exercise.Sets,
                Reps = exercise.Reps,
                CreatedAt = exercise.CreatedAt
            };
        }
    }
}