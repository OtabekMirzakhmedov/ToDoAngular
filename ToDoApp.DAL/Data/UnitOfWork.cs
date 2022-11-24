
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DAL.Data;
using ToDoApp.DAL.Interfaces;
using ToDoApp.DAL.Repositories;

namespace GameStore.DAL.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;

            ToDoRepository = new ToDoRepository(_dbContext);
            StepRepository = new StepRepository(_dbContext);
        }

        public IToDoRepository ToDoRepository { get; private set; }
        public IStepRepository StepRepository { get; private set; }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
