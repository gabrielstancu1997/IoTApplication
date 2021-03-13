using IoTApplication.Data;
using IoTApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicatieLicentaIoT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Value>>> GetValues()
        {
            return await _context.Values.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Value>> GetValue(int id)
        {
            var value = await _context.Values.FindAsync(id);

            if (value == null)
            {
                return NotFound();
            }

            return value;

        }

        [HttpPost]
        public async Task<ActionResult<Value>> PostValue(Value value)
        {
            _context.Values.Add(value);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostValue", new { id = value.Id }, value);
        }

    }
}
