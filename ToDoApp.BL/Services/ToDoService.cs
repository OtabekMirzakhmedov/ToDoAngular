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
            ToDo toDo = mapper.Map<ToDo>(model);
            await unitOfWork.ToDoRepository.AddAsync(toDo);
            await unitOfWork.SaveAsync();
            model.Id = toDo.Id;
        }

        public async Task DeleteAsync(int modelId)
        {
            await unitOfWork.ToDoRepository.DeleteByIdAsync(modelId);
            await unitOfWork.SaveAsync();
           
        }

        public async Task<IEnumerable<ToDoModel>> GetAllAsync(string userId)
        {
            IEnumerable<ToDo> todos = await unitOfWork.ToDoRepository.GetAllWithDetailsAsync();
            return todos.Where(i => i.AppUserId == userId).Select(i => mapper.Map<ToDoModel>(i));
        }

        public Task<ToDoModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ToDoModel model)
        {
            ToDo toDo = mapper.Map<ToDo>(model);
            unitOfWork.ToDoRepository.Update(toDo);
            await unitOfWork.SaveAsync();
        }
    }
}
