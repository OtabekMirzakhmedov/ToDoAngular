using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.BL.Models;

namespace ToDoApp.BL.Interfaces
{
    public interface IToDoService : ICrud<ToDoModel>
    {
    }
}
