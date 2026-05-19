using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasterServicesAPI.Models;

namespace MasterServicesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodosPagoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MetodosPagoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MetodosPago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetodosPago>>> GetMetodosPago()
        {
            return await _context.MetodosPago.ToListAsync();
        }

        // GET: api/MetodosPago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MetodosPago>> GetMetodosPago(int id)
        {
            var metodosPago = await _context.MetodosPago.FindAsync(id);

            if (metodosPago == null)
            {
                return NotFound();
            }

            return metodosPago;
        }

        // PUT: api/MetodosPago/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetodosPago(int id, MetodosPago metodosPago)
        {
            if (id != metodosPago.Id)
            {
                return BadRequest();
            }

            _context.Entry(metodosPago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetodosPagoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MetodosPago
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MetodosPago>> PostMetodosPago(MetodosPago metodosPago)
        {
            _context.MetodosPago.Add(metodosPago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMetodosPago", new { id = metodosPago.Id }, metodosPago);
        }

        // DELETE: api/MetodosPago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetodosPago(int id)
        {
            var metodosPago = await _context.MetodosPago.FindAsync(id);
            if (metodosPago == null)
            {
                return NotFound();
            }

            _context.MetodosPago.Remove(metodosPago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MetodosPagoExists(int id)
        {
            return _context.MetodosPago.Any(e => e.Id == id);
        }
    }
}
