using WorkoutService.DTOs;
using WorkoutService.Services;
using WorkoutService.Stubs;

namespace WorkoutService.Test
{
    [TestClass]
    public class WorkoutServiceUnitTest
    {
        private readonly WorkoutService_ _workoutService;

        public WorkoutServiceUnitTest() 
        {
            _workoutService = new WorkoutService_(new WorkoutRepositoryStub(), new ExerciseRepositoryStub());
        }

        [TestMethod]
        public void WorkoutService_GetWorkout_ReturnsWorkout()
        {
            // Arrange
            var expectedAmount = 100;

            // Act
            var workout = _workoutService.Get();

            // Assert
            Assert.AreEqual(workout.Count, expectedAmount);
        }

        [TestMethod]
        public void WorkoutService_GetWorkout_ReturnsWorkoutWithExercises()
        {
            // Arrange

            // Act
            var workout = _workoutService.Get();

            // Assert
            Assert.IsTrue(workout.All(w => w.Exercises.Count > 0));
        }

        [TestMethod]
        public void WorkoutSerivce_GetSingleWorkout_ReturnsWorkout()
        {
            // Arrange
            var expectedWorkout = new ShowWorkoutDTO
            {
                Id = 1,
                Name = "Workout 1",
                CreatedAt = new DateTime(2020, 1, 1)
            };

            // Act
            var workout = _workoutService.Get(1);

            // Assert
            Assert.AreEqual(workout.Id, expectedWorkout.Id);
            Assert.AreEqual(workout.Name, expectedWorkout.Name);
            Assert.AreEqual(workout.CreatedAt, expectedWorkout.CreatedAt);
        }

        [TestMethod]
        public void WorkoutService_GetSingleWorkout_ReturnWorkoutWithExercises()
        {
            // Arrange
            var expectedWorkout = new ShowWorkoutDTO
            {
                Id = 1,
                Name = "Workout 1",
                CreatedAt = new DateTime(2020, 1, 1),
                Exercises = new List<ShowExerciseDTO>
                {
                    new ShowExerciseDTO
                    {
                        Id = 1,
                        Name = "Bench Press",
                        Sets = 3,
                        Reps = 12,
                        CreatedAt = new DateTime(2020, 1, 1)
                    }
                }
            };

            // Act
            var workout = _workoutService.Get(1);

            // Assert
            Assert.AreEqual(workout.Id, expectedWorkout.Id);
            Assert.AreEqual(workout.Name, expectedWorkout.Name);
            Assert.AreEqual(workout.CreatedAt, expectedWorkout.CreatedAt);

            Assert.AreEqual(workout.Exercises.Count, expectedWorkout.Exercises.Count);
            Assert.AreEqual(workout.Exercises.First().Id, expectedWorkout.Exercises.First().Id);
            Assert.AreEqual(workout.Exercises.First().Name, expectedWorkout.Exercises.First().Name);
            Assert.AreEqual(workout.Exercises.First().Sets, expectedWorkout.Exercises.First().Sets);
            Assert.AreEqual(workout.Exercises.First().Reps, expectedWorkout.Exercises.First().Reps);
            Assert.AreEqual(workout.Exercises.First().CreatedAt, expectedWorkout.Exercises.First().CreatedAt);
        }
    }
}