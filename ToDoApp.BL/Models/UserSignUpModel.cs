using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.BL.Models
{
    public class UserSignUpModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}
