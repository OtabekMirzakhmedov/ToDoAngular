using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Data;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Interfaces;

namespace ToDoApp.DAL.Repositories
{
    public class StepRepository : Repository<Step>, IStepRepository
    {
        public StepRepository(AppDbContext context) : base(context)
        {
        }
    }
}
