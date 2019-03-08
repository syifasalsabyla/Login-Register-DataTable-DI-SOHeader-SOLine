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
    [Route("api/SOLine")]
    [ApiController]
    public class SOLineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SOLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SOLine
        [HttpGet]
        public IEnumerable<SOLine> GetSOLine()
        {
            return _context.SOLine.Include(x => x.Produk).Include(x => x.SOHeader).ToList();
        }

        // GET: api/SOLine/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSOLine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sOLine = await _context.SOLine
                .Include(x => x.Produk)
                .Include(x => x.SOHeader) //ini yang ditambahkan
                .Where(x => x.SOLineID.Equals(id)).FirstOrDefaultAsync();

            if (sOLine == null)
            {
                return NotFound();
            }

            return Ok(sOLine);
        }

        // PUT: api/SOLine/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSOLine([FromRoute] int id, [FromBody] SOLine sOLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sOLine.SOLineID)
            {
                return BadRequest();
            }

            _context.Entry(sOLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SOLineExists(id))
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

        // POST: api/SOLine
        [HttpPost]
        public async Task<IActionResult> PostSOLine([FromBody] SOLine sOLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SOLine.Add(sOLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSOLine", new { id = sOLine.SOLineID }, sOLine);
        }

        // DELETE: api/SOLine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSOLine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sOLine = await _context.SOLine.FindAsync(id);
            if (sOLine == null)
            {
                return NotFound();
            }

            _context.SOLine.Remove(sOLine);
            await _context.SaveChangesAsync();

            return Ok(sOLine);
        }

        private bool SOLineExists(int id)
        {
            return _context.SOLine.Any(e => e.SOLineID == id);
        }
    }
}