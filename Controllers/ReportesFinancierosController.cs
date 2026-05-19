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
    public class ReportesFinancierosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportesFinancierosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ReportesFinancieros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportesFinanciero>>> GetReportesFinanciero()
        {
            return await _context.ReportesFinancieros.ToListAsync();
        }

        // GET: api/ReportesFinancieros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportesFinanciero>> GetReportesFinanciero(int id)
        {
            var reportesFinanciero = await _context.ReportesFinancieros.FindAsync(id);

            if (reportesFinanciero == null)
            {
                return NotFound();
            }

            return reportesFinanciero;
        }

        // PUT: api/ReportesFinancieros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportesFinanciero(int id, ReportesFinanciero reportesFinanciero)
        {
            if (id != reportesFinanciero.Id)
            {
                return BadRequest();
            }

            _context.Entry(reportesFinanciero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportesFinancieroExists(id))
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

        // POST: api/ReportesFinancieros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReportesFinanciero>> PostReportesFinanciero(ReportesFinanciero reportesFinanciero)
        {
            _context.ReportesFinancieros.Add(reportesFinanciero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportesFinanciero", new { id = reportesFinanciero.Id }, reportesFinanciero);
        }

        // DELETE: api/ReportesFinancieros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportesFinanciero(int id)
        {
            var reportesFinanciero = await _context.ReportesFinancieros.FindAsync(id);
            if (reportesFinanciero == null)
            {
                return NotFound();
            }

            _context.ReportesFinancieros.Remove(reportesFinanciero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportesFinancieroExists(int id)
        {
            return _context.ReportesFinancieros.Any(e => e.Id == id);
        }
    }
}
