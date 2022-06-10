using WorkoutService.DTOs;
using WorkoutService.Services;
using WorkoutService.Stubs;

namespace WorkoutService.Test
{
    [TestClass]
    public class ExerciseServiceUnitTest
    {
        private readonly ExerciseService _exerciseService;
        private readonly int _exerciseAmount;

        public ExerciseServiceUnitTest()
        {
            _exerciseService = new ExerciseService(new ExerciseRepositoryStub());

            _exerciseAmount = 100;
        }

        [TestMethod]
        public void ExerciseService_GetExercises_ReturnsExercises()
        {
            // Arrange
            var expectedAmount = 100;

            // Act
            var exercises = _exerciseService.Get();

            // Assert
            Assert.AreEqual(exercises.Count, expectedAmount);
        }

        [TestMethod]
        public void ExerciseService_GetExercise_ReturnsExercise()
        {
            // Arrange
            var expectedExercise = new ShowExerciseDTO
            {
                Id = 1,
                Name = "Exercise 1",
                Sets = 3,
                Reps = 12,
                CreatedAt = new DateTime(2020, 1, 1)
            };

            // Act
            var exercise = _exerciseService.Get(1);

            // Assert
            Assert.AreEqual(exercise.Id, expectedExercise.Id);
            Assert.AreEqual(exercise.Name, expectedExercise.Name);
            Assert.AreEqual(exercise.Sets, expectedExercise.Sets);
            Assert.AreEqual(exercise.Reps, expectedExercise.Reps);
            Assert.AreEqual(exercise.CreatedAt, expectedExercise.CreatedAt);
        }

        [TestMethod]
        public void ExerciseService_PostExercise_ReturnsExercise()
        {
            // Arrange
            var expectedExercise = new ShowExerciseDTO
            {
                Id = _exerciseAmount + 1,
                Name = "Exercise 101",
                Sets = 3,
                Reps = 12,
                CreatedAt = new DateTime(2020, 1, 1)
            };
            var exerciseToCreate = new CreateExerciseDTO
            {
                Name = "Exercise 101",
                Sets = 3,
                Reps = 12
            };

            // Act
            var exercise = _exerciseService.Post(exerciseToCreate);

            // Assert
            Assert.AreEqual(expectedExercise.Id, exercise.Id);
            Assert.AreEqual(expectedExercise.Name, exercise.Name);
            Assert.AreEqual(expectedExercise.Sets, exercise.Sets);
            Assert.AreEqual(expectedExercise.Reps, exercise.Reps);
            Assert.IsNotNull(exercise.CreatedAt);
        }
    }
}