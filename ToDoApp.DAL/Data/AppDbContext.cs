using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Step> Steps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>()
                .HasOne<AppUser>()
                .WithMany(a => a.toDos)
                .HasForeignKey(t => t.AppUserId);

            modelBuilder.Entity<Step>()
                .HasOne<ToDo>()
                .WithMany(t => t.Steps)
                .HasForeignKey(s => s.ToDoId);

            base.OnModelCreating(modelBuilder);

        }

    }
}
