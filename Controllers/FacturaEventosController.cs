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
    public class FacturaEventosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FacturaEventosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FacturaEventos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaEvento>>> GetFacturaEvento()
        {
            return await _context.FacturaEventos.ToListAsync();
        }

        // GET: api/FacturaEventos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaEvento>> GetFacturaEvento(int id)
        {
            var facturaEvento = await _context.FacturaEventos.FindAsync(id);

            if (facturaEvento == null)
            {
                return NotFound();
            }

            return facturaEvento;
        }

        // PUT: api/FacturaEventos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacturaEvento(int id, FacturaEvento facturaEvento)
        {
            if (id != facturaEvento.Id)
            {
                return BadRequest();
            }

            _context.Entry(facturaEvento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaEventoExists(id))
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

        // POST: api/FacturaEventos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FacturaEvento>> PostFacturaEvento(FacturaEvento facturaEvento)
        {
            _context.FacturaEventos.Add(facturaEvento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacturaEvento", new { id = facturaEvento.Id }, facturaEvento);
        }

        // DELETE: api/FacturaEventos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacturaEvento(int id)
        {
            var facturaEvento = await _context.FacturaEventos.FindAsync(id);
            if (facturaEvento == null)
            {
                return NotFound();
            }

            _context.FacturaEventos.Remove(facturaEvento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacturaEventoExists(int id)
        {
            return _context.FacturaEventos.Any(e => e.Id == id);
        }
    }
}
