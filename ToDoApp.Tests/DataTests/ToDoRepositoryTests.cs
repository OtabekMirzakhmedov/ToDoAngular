using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ToDoApp.DAL.Data;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Repositories;

namespace ToDoApp.Tests.DataTests
{
    [TestFixture]
    public class ToDoRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task ToDoRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var toDoRepository = new ToDoRepository(context);
            var todo = await toDoRepository.GetByIdAsync(id);

            var expected = ExpectedToDos.FirstOrDefault(x => x.Id == id);

            Assert.That(todo, Is.EqualTo(expected).Using(new ToDoEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }



        [Test]
        public async Task ToDoRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var toDoRepository = new ToDoRepository(context);
            var toDos = await toDoRepository.GetAllAsync();

            Assert.That(toDos, Is.EqualTo(ExpectedToDos).Using(new ToDoEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task ToDoRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var toDoRepository = new ToDoRepository(context);
            var toDo = new ToDo { Id = 4, AppUserId = "user3", CreatedAt = new DateTime(2022, 12, 3), Text = "unit test", IsCompleted= false, Deadline = null, Progress = 0 };

            await toDoRepository.AddAsync(toDo);
            await context.SaveChangesAsync();

            Assert.That(context.ToDos.Count(), Is.EqualTo(4), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task ToDoRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var toDoRepository = new ToDoRepository(context);

            await toDoRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.ToDos.Count(), Is.EqualTo(2), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task ToDoRepository_Update_UpdatesEntity()
        {
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var toDoRepository = new ToDoRepository(context);
            var toDo = new ToDo
            {
                Id = 1,
                AppUserId = "user1",
                CreatedAt = new DateTime(2022, 11, 28),
                Deadline = new DateTime(2022, 11, 29),
                IsCompleted = false,
                Progress = 0,
                Text = "Organize meeting text updated"
            };

            toDoRepository.Update(toDo);
            await context.SaveChangesAsync();

            Assert.That(toDo, Is.EqualTo(new ToDo
            {
                Id = 1,
                AppUserId = "user1",
                CreatedAt = new DateTime(2022, 11, 28),
                Deadline = new DateTime(2022, 11, 29),
                IsCompleted = false,
                Progress = 0,
                Text = "Organize meeting text updated"
            }).Using(new ToDoEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task ToDoRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var toDoRepository = new ToDoRepository(context);

            var todos = await toDoRepository.GetAllWithDetailsAsync();
            var todo = todos.FirstOrDefault(x => x.Id == 1);

            var expectedStepsCount = 1;

            Assert.That(todos, Is.EqualTo(ExpectedToDos).Using(new ToDoEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");
            Assert.That(todo.Steps.Count(), Is.EqualTo(expectedStepsCount), message: "GetAllWithDetailsAsync method doesnt't return included entities");
            Assert.That(todo.Steps, Is.Not.Null, message: "GetByIdWithDetailsAsync method doesnt't return included entities");
        }

        [TestCase("user2")]
        public async Task ToDoRepository_GetAllWithDetailsByUserIdAsync_ReturnsWithIncludedEntities(string id)
        {
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var toDoRepository = new ToDoRepository(context);

            var todos = await toDoRepository.GetAllWithDetailsByUserId(id);
            var todo = todos.FirstOrDefault(x => x.Id == 2);

            var expectedStepsCount = 2;

            Assert.That(todos, Is.EqualTo(ExpectedToDosForGetByUserIdCheck).Using(new ToDoEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");
            Assert.That(todo.Steps.Count(), Is.EqualTo(expectedStepsCount), message: "GetAllWithDetailsAsync method doesnt't return included entities");
            Assert.That(todo.Steps, Is.Not.Null, message: "GetAllByUserIdWithDetailsAsync method doesnt't return included entities");
        }




        private static IEnumerable<ToDo> ExpectedToDos =>
            new[]
            {
                new ToDo { Id = 1, AppUserId = "user1", CreatedAt = new DateTime(2022, 11, 28), Deadline = new DateTime(2022, 11, 29), IsCompleted = false, Progress = 0, Text = "Organize meeting"},
                new ToDo { Id = 2, AppUserId = "user2", CreatedAt = new DateTime(2022, 11, 27), Deadline = new DateTime(2022, 11, 29), IsCompleted = true, Progress = 25, Text = "Shopping list" },
                new ToDo { Id = 3, AppUserId = "user2", CreatedAt = new DateTime(2022, 11, 28), Deadline = new DateTime(2022, 1, 12), IsCompleted = false, Progress = 0, Text = "Visit doctor" }
            };

        private static IEnumerable<ToDo> ExpectedToDosForGetByUserIdCheck =>
            new[]
            {
                new ToDo { Id = 2, AppUserId = "user2", CreatedAt = new DateTime(2022, 11, 27), Deadline = new DateTime(2022, 11, 29), IsCompleted = true, Progress = 25, Text = "Shopping list" },
                new ToDo { Id = 3, AppUserId = "user2", CreatedAt = new DateTime(2022, 11, 28), Deadline = new DateTime(2022, 1, 12), IsCompleted = false, Progress = 0, Text = "Visit doctor" }
            };

    }
}
