using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ToDoApp.DAL.Entities
{
    public class ToDo : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Text { get; set; }
        
        [MaybeNull]
        public DateTime Deadline { get; set; }
        public int Progress { get; set; }
        public bool IsCompleted { get; set; }

        public string AppUserId { get; set; }
        public ICollection<Step> Steps { get; set; }
    }
}
