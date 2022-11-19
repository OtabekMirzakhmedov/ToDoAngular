using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        Task SaveAsync();

    }
}
