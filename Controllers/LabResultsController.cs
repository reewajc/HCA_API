using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HCA_API.Model;
using Microsoft.Extensions.Caching.Memory;

namespace HCA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabResultsController : ControllerBase
    {
        private readonly PatientContext _context;
        private IMemoryCache _cache;

        public LabResultsController(PatientContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // GET: api/LabResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabResult>>> GetLabResult()
        {
            _cache.Set("LabResultKey", _context.LabResult);
            return await _context.LabResult.ToListAsync();
        }

        // GET: api/LabResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LabResult>> GetLabResult(int id)
        {
            var labResult = await _context.LabResult.FindAsync(id);

            if (labResult == null)
            {
                return NotFound();
            }

            return labResult;
        }

        // PUT: api/LabResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLabResult(int id, LabResult labResult)
        {
            if (id != labResult.LabID)
            {
                return BadRequest();
            }

            _context.Entry(labResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LabResultExists(id))
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

        // POST: api/LabResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LabResult>> PostLabResult(LabResult labResult)
        {
            if (ModelState.IsValid)
            {
                _context.LabResult.Add(labResult);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetLabResult", new { id = labResult.LabID }, labResult);
            }
            else
            {
                return BadRequest();
            }

          
        }

        // DELETE: api/LabResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLabResult(int id)
        {
            var labResult = await _context.LabResult.FindAsync(id);
            if (labResult == null)
            {
                return NotFound();
            }

            _context.LabResult.Remove(labResult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LabResultExists(int id)
        {
            return _context.LabResult.Any(e => e.LabID == id);
        }
    }
}
