using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.BL.Models;
using ToDoApp.DAL.Entities;

namespace ToDoApp.BL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ToDo, ToDoModel>().ReverseMap();
        }
    }
}
