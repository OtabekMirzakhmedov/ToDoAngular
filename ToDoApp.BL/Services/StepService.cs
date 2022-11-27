using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.BL.Interfaces;
using ToDoApp.BL.Models;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Interfaces;

namespace ToDoApp.BL.Services
{
    public class StepService : IStepService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public StepService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            unitOfWork = UnitOfWork;
            mapper = Mapper;
        }
        public async Task AddAsync(StepModel model)
        {
            await unitOfWork.StepRepository.AddAsync(mapper.Map<Step>(model));
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await unitOfWork.StepRepository.DeleteByIdAsync(modelId);
            await unitOfWork.SaveAsync();
        }

        public Task<IEnumerable<StepModel>> GetAllAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<StepModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(StepModel model)
        {
            throw new NotImplementedException();
        }
    }
}
