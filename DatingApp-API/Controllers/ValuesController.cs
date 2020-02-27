using System.Linq;
using System.Threading.Tasks;
using DatingApp_API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp_API.Controllers
{
    //POST http://localhost:5000/api/values/5
    [Authorize] //anything inside this class must be authorized
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context; //private readonly field to use in our class

        public ValuesController(DataContext context) //inject our DataContext
        {
            _context = context;

        }
        //GET api/values
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetValues() //task of IActionResult 
        {
            //a task is an asynchronous operation that returns a value
            //go to the database and retrieve the values
            //_context gets us access to all entity framework's methods and also our DbSet
            //use the keyword await (to wait for the response) and use ToListAsync
            var values = await _context.Values.ToListAsync();

            return Ok(values);
        }

        //GET api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            
            return Ok(value);
        }


    }
}