using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.BL.Interfaces;
using ToDoApp.BL.Models;

namespace ToDoApp.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService toDoService;

        public ToDoController(IToDoService ToDoService)
        {
            toDoService = ToDoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoModel>>> Get()
        {
            return Ok(await toDoService.GetAllAsync());
        }
        
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ToDoModel value)
        {
            await toDoService.AddAsync(value);
            return Created("", value);
        }

    }
}
