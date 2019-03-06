using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using latihanLogSig9.Data;
using latihanLogSig9.Models;

namespace latihanLogSig9.Controllers.api
{
    [Route("api/SOHeader")]
    [ApiController]
    public class SOHeaderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SOHeaderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SOHeader
        [HttpGet]
        public IEnumerable<SOHeader> GetSOHeader()
        {
            return _context.SOHeader.Include(x => x.Member).ToList();
        }

        // GET: api/SOHeader/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSOHeader([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sOHeader = await _context.SOHeader
                                .Include(x => x.Member)
                                .Where(x => x.SOHeaderID.Equals(id)).FirstOrDefaultAsync();

            if (sOHeader == null)
            {
                return NotFound();
            }

            return Ok(sOHeader);
        }

        // PUT: api/SOHeader/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSOHeader([FromRoute] int id, [FromBody] SOHeader sOHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sOHeader.SOHeaderID)
            {
                return BadRequest();
            }

            _context.Entry(sOHeader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SOHeaderExists(id))
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

        // POST: api/SOHeader
        [HttpPost]
        public async Task<IActionResult> PostSOHeader([FromBody] SOHeader sOHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SOHeader.Add(sOHeader);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSOHeader", new { id = sOHeader.SOHeaderID }, sOHeader);
        }

        // DELETE: api/SOHeader/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSOHeader([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sOHeader = await _context.SOHeader.FindAsync(id);
            if (sOHeader == null)
            {
                return NotFound();
            }

            _context.SOHeader.Remove(sOHeader);
            await _context.SaveChangesAsync();

            return Ok(sOHeader);
        }

        private bool SOHeaderExists(int id)
        {
            return _context.SOHeader.Any(e => e.SOHeaderID == id);
        }
    }
}