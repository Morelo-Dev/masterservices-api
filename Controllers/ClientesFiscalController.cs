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
    public class ClientesFiscalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesFiscalController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ClientesFiscal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesFiscal>>> GetClientesFiscal()
        {
            return await _context.ClientesFiscal.ToListAsync();
        }

        // GET: api/ClientesFiscal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientesFiscal>> GetClientesFiscal(int id)
        {
            var clientesFiscal = await _context.ClientesFiscal.FindAsync(id);

            if (clientesFiscal == null)
            {
                return NotFound();
            }

            return clientesFiscal;
        }

        // PUT: api/ClientesFiscal/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientesFiscal(int id, ClientesFiscal clientesFiscal)
        {
            if (id != clientesFiscal.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientesFiscal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesFiscalExists(id))
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

        // POST: api/ClientesFiscal
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientesFiscal>> PostClientesFiscal(ClientesFiscal clientesFiscal)
        {
            _context.ClientesFiscal.Add(clientesFiscal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientesFiscal", new { id = clientesFiscal.Id }, clientesFiscal);
        }

        // DELETE: api/ClientesFiscal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientesFiscal(int id)
        {
            var clientesFiscal = await _context.ClientesFiscal.FindAsync(id);
            if (clientesFiscal == null)
            {
                return NotFound();
            }

            _context.ClientesFiscal.Remove(clientesFiscal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientesFiscalExists(int id)
        {
            return _context.ClientesFiscal.Any(e => e.Id == id);
        }
    }
}
