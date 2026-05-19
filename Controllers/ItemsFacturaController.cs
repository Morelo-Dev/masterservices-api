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
    public class ItemsFacturaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemsFacturaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemsFactura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemsFactura>>> GetItemsFactura()
        {
            return await _context.ItemsFactura.ToListAsync();
        }

        // GET: api/ItemsFactura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemsFactura>> GetItemsFactura(int id)
        {
            var itemsFactura = await _context.ItemsFactura.FindAsync(id);

            if (itemsFactura == null)
            {
                return NotFound();
            }

            return itemsFactura;
        }

        // PUT: api/ItemsFactura/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemsFactura(int id, ItemsFactura itemsFactura)
        {
            if (id != itemsFactura.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemsFactura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemsFacturaExists(id))
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

        // POST: api/ItemsFactura
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemsFactura>> PostItemsFactura(ItemsFactura itemsFactura)
        {
            _context.ItemsFactura.Add(itemsFactura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemsFactura", new { id = itemsFactura.Id }, itemsFactura);
        }

        // DELETE: api/ItemsFactura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemsFactura(int id)
        {
            var itemsFactura = await _context.ItemsFactura.FindAsync(id);
            if (itemsFactura == null)
            {
                return NotFound();
            }

            _context.ItemsFactura.Remove(itemsFactura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemsFacturaExists(int id)
        {
            return _context.ItemsFactura.Any(e => e.Id == id);
        }
    }
}
