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
    public class DescuentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DescuentosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Descuentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Descuento>>> GetDescuento()
        {
            return await _context.Descuentos.ToListAsync();
        }

        // GET: api/Descuentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Descuento>> GetDescuento(int id)
        {
            var descuento = await _context.Descuentos.FindAsync(id);

            if (descuento == null)
            {
                return NotFound();
            }

            return descuento;
        }

        // PUT: api/Descuentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDescuento(int id, Descuento descuento)
        {
            if (id != descuento.Id)
            {
                return BadRequest();
            }

            _context.Entry(descuento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DescuentoExists(id))
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

        // POST: api/Descuentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Descuento>> PostDescuento(Descuento descuento)
        {
            _context.Descuentos.Add(descuento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDescuento", new { id = descuento.Id }, descuento);
        }

        // DELETE: api/Descuentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDescuento(int id)
        {
            var descuento = await _context.Descuentos.FindAsync(id);
            if (descuento == null)
            {
                return NotFound();
            }

            _context.Descuentos.Remove(descuento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DescuentoExists(int id)
        {
            return _context.Descuentos.Any(e => e.Id == id);
        }
    }
}
