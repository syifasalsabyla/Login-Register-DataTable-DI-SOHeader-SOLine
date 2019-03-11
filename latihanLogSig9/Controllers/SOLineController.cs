using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using latihanLogSig9.Data;
using latihanLogSig9.Models;

namespace latihanLogSig9.Controllers
{
    public class SOLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SOLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SOLine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SOLine.Include(s => s.Produk).Include(s => s.SOHeader);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SOLine/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sOLine = await _context.SOLine
                .Include(s => s.Produk)
                .Include(s => s.SOHeader)
                .FirstOrDefaultAsync(m => m.SOLineID == id);
            if (sOLine == null)
            {
                return NotFound();
            }

            return View(sOLine);
        }

        // GET: SOLine/Create
        public IActionResult Create()
        {
            ViewData["ProdukID"] = new SelectList(_context.Set<Produk>(), "ProdukID", "NamaProduk");
            ViewData["SOHeaderID"] = new SelectList(_context.SOHeader, "SOHeaderID", "SOHeaderID");
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult Create([FromRoute] int id)
        {
            ViewData["ProdukID"] = new SelectList(_context.Set<Produk>(), "ProdukID", "NamaProduk");
            ViewData["SOHeaderID"] = new SelectList(_context.SOHeader, "SOHeaderID", "SOHeaderID", id);
            return View();
        }

        // POST: SOLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SOLineID,SOHeaderID,ProdukID,Quantity")] SOLine sOLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sOLine);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "SOHeader", new { id = sOLine.SOHeaderID });
            }
            ViewData["ProdukID"] = new SelectList(_context.Set<Produk>(), "ProdukID", "NamaProduk", sOLine.ProdukID);
            ViewData["SOHeaderID"] = new SelectList(_context.SOHeader, "SOHeaderID", "SOHeaderID", sOLine.SOHeaderID);
            return View(sOLine);
        }

        // GET: SOLine/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sOLine = await _context.SOLine.FindAsync(id);
            if (sOLine == null)
            {
                return NotFound();
            }
            ViewData["ProdukID"] = new SelectList(_context.Set<Produk>(), "ProdukID", "NamaProduk", sOLine.ProdukID);
            ViewData["SOHeaderID"] = new SelectList(_context.SOHeader, "SOHeaderID", "SOHeaderID", sOLine.SOHeaderID);
            return View(sOLine);
        }

        // POST: SOLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SOLineID,SOHeaderID,ProdukID,Quantity")] SOLine sOLine)
        {
            if (id != sOLine.SOLineID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sOLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SOLineExists(sOLine.SOLineID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdukID"] = new SelectList(_context.Set<Produk>(), "ProdukID", "NamaProduk", sOLine.ProdukID);
            ViewData["SOHeaderID"] = new SelectList(_context.SOHeader, "SOHeaderID", "SOHeaderID", sOLine.SOHeaderID);
            return View(sOLine);
        }

        // GET: SOLine/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sOLine = await _context.SOLine
                .Include(s => s.Produk)
                .Include(s => s.SOHeader)
                .FirstOrDefaultAsync(m => m.SOLineID == id);
            if (sOLine == null)
            {
                return NotFound();
            }

            return View(sOLine);
        }

        // POST: SOLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sOLine = await _context.SOLine.FindAsync(id);
            _context.SOLine.Remove(sOLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SOLineExists(int id)
        {
            return _context.SOLine.Any(e => e.SOLineID == id);
        }
    }
}
