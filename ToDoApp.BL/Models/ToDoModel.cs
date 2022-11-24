using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Entities;

namespace ToDoApp.BL.Models
{
    public class ToDoModel
    { 
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Deadline { get; set; }

        public int Progress { get; set; }

        public string UserId { get; set; }

        public ICollection<Step> Steps { get; set; }


    }
}
