using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Interfaces
{
    public interface IToDoRepository : IRepository<ToDo>
    {
        Task<IEnumerable<ToDo>> GetAllWithDetailsByUserId(string UserId);
        Task<IEnumerable<ToDo>> GetAllWithDetailsAsync();
    }

    

}
