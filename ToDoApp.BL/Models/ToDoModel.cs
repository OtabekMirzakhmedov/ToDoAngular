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

        public DateTime? Deadline { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Progress { get; set; }

        public bool IsCompleted { get; set; }

        public string AppUserId { get; set; }

        public ICollection<Step> Steps { get; set; }


    }
}
