using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToDoApp.DAL.Entities
{
    public class Step : BaseEntity
    {

        [MaxLength(255)]
        public string StepText { get; set; }
        public bool isFinished { get; set; }

        public int ToDoId { get; set; }
    }
}
