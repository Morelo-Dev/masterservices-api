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
    public class ServiciosClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServiciosClienteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ServiciosCliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiciosCliente>>> GetServiciosCliente()
        {
            return await _context.ServiciosClientes.ToListAsync();
        }

        // GET: api/ServiciosCliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiciosCliente>> GetServiciosCliente(int id)
        {
            var serviciosCliente = await _context.ServiciosClientes.FindAsync(id);

            if (serviciosCliente == null)
            {
                return NotFound();
            }

            return serviciosCliente;
        }

        // PUT: api/ServiciosCliente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiciosCliente(int id, ServiciosCliente serviciosCliente)
        {
            if (id != serviciosCliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(serviciosCliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiciosClienteExists(id))
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

        // POST: api/ServiciosCliente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiciosCliente>> PostServiciosCliente(ServiciosCliente serviciosCliente)
        {
            _context.ServiciosClientes.Add(serviciosCliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServiciosCliente", new { id = serviciosCliente.Id }, serviciosCliente);
        }

        // DELETE: api/ServiciosCliente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiciosCliente(int id)
        {
            var serviciosCliente = await _context.ServiciosClientes.FindAsync(id);
            if (serviciosCliente == null)
            {
                return NotFound();
            }

            _context.ServiciosClientes.Remove(serviciosCliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiciosClienteExists(int id)
        {
            return _context.ServiciosClientes.Any(e => e.Id == id);
        }
    }
}
