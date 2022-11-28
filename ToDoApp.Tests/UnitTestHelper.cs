using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.BL;
using ToDoApp.DAL.Data;
using ToDoApp.DAL.Entities;

namespace ToDoApp.Tests
{
    internal static class UnitTestHelper
    {
        public static DbContextOptions<AppDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase( Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDbContext(options))
            {
                SeedData(context);
            }

            return options;
        }

        public static IMapper CreateMapperProfile()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }

        public static void SeedData(AppDbContext context)
        {
            context.AppUsers.AddRange(
                new AppUser { Id ="user1", Email="user1@unit.com", FullName = "Otabek Mirzakhmedov"},
                new AppUser { Id = "user2", Email = "user2@unit.com", FullName = "User Mirzakhmedov" });
            context.ToDos.AddRange(
                new ToDo { Id = 1, AppUserId = "user1", CreatedAt = new DateTime(2022, 11, 28), Deadline = new DateTime(2022, 11, 29), IsCompleted = false, Progress = 0, Text = "Organize meeting"},
                new ToDo { Id = 2, AppUserId = "user2", CreatedAt = new DateTime(2022, 11, 27), Deadline = new DateTime(2022, 11, 29), IsCompleted = true, Progress = 25, Text = "Shopping list" },
                new ToDo { Id = 3, AppUserId = "user2", CreatedAt = new DateTime(2022, 11, 28), Deadline = new DateTime(2022, 1, 12), IsCompleted = false, Progress = 0, Text = "Visit doctor" });
            context.Steps.AddRange(
                new Step { Id = 1, isFinished = false, StepText = "prepare room", ToDoId = 1},
                new Step{ Id = 2,  isFinished = false, StepText ="apple", ToDoId = 2 },
                new Step { Id = 3, isFinished = false, StepText = "banana", ToDoId = 2 });
            
            context.SaveChanges();
        }

    }
}
