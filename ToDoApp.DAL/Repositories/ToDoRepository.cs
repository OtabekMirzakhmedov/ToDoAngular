using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DAL.Data;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Interfaces;

namespace ToDoApp.DAL.Repositories
{
    public class ToDoRepository : Repository<ToDo>, IToDoRepository
    {
        public ToDoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ToDo>> GetAllWithDetailsAsync()
        {
            return await dbContext.Set<ToDo>().Include(i => i.Steps).ToListAsync();
        }

        public async Task<IEnumerable<ToDo>> GetAllWithDetailsByUserId(string UserId)
        {
            return await dbContext.Set<ToDo>().Include(i => i.Steps).Where(i => i.AppUserId == UserId).ToListAsync();
        }
    }
}
