using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.BL.Interfaces;
using ToDoApp.BL.Models;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Interfaces;

namespace ToDoApp.BL.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ToDoService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            unitOfWork = UnitOfWork;
            mapper = Mapper;
        }

        public async Task AddAsync(ToDoModel model)
        {

            await unitOfWork.ToDoRepository.AddAsync(mapper.Map<ToDo>(model));
            await unitOfWork.SaveAsync();
        }

        public Task DeleteAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ToDoModel>> GetAllAsync()
        {
            IEnumerable<ToDo> todos = await unitOfWork.ToDoRepository.GetAllWithDetailsAsync();
            return todos.Select(i => mapper.Map<ToDoModel>(i));
        }

        public Task<ToDoModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ToDoModel model)
        {
            throw new NotImplementedException();
        }
    }
}
