using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToDoApp.DAL.Entities
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }

        public ICollection<ToDo> toDos { get; set; }

    }
}
