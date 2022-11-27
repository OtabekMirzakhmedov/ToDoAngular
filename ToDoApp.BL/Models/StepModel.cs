using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.BL.Models
{
    public class StepModel
    {
        public int Id { get; set; }

        public string StepText { get; set; }

        public bool IsFinished { get; set; }

        public int ToDoId { get; set; }
    }
}
