using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using latihanLogSig9.Data;
using latihanLogSig9.Models;
using latihanLogSig9.Models.VM;

namespace latihanLogSig9.Controllers
{
    public class SOHeaderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SOHeaderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SOHeader
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SOHeader.Include(s => s.Member);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SOHeader/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sOHeader = await _context.SOHeader
                .Include(s => s.Member)
                .FirstOrDefaultAsync(m => m.SOHeaderID == id);

            List<SOLine> lines = new List<SOLine>();
            lines = await _context.SOLine.Where(x => x.SOHeaderID.Equals(sOHeader.SOHeaderID)).ToListAsync();

            SOHeaderLine vm = new SOHeaderLine();
            vm.SOHeader = sOHeader;
            vm.SOLine = lines;

            if (sOHeader == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: SOHeader/Create
        public IActionResult Create()
        {
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "MemberID", "NamaMember");
            return View();
        }

        // POST: SOHeader/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SOHeaderID,MemberID,Tanggal")] SOHeader sOHeader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sOHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "MemberID", "NamaMember", sOHeader.MemberID);
            return View(sOHeader);
        }

        // GET: SOHeader/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sOHeader = await _context.SOHeader.FindAsync(id);
            if (sOHeader == null)
            {
                return NotFound();
            }
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "MemberID", "NamaMember", sOHeader.MemberID);
            return View(sOHeader);
        }

        // POST: SOHeader/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SOHeaderID,MemberID,Tanggal")] SOHeader sOHeader)
        {
            if (id != sOHeader.SOHeaderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sOHeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SOHeaderExists(sOHeader.SOHeaderID))
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
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "MemberID", "NamaMember", sOHeader.MemberID);
            return View(sOHeader);
        }

        // GET: SOHeader/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sOHeader = await _context.SOHeader
                .Include(s => s.Member)
                .FirstOrDefaultAsync(m => m.SOHeaderID == id);
            if (sOHeader == null)
            {
                return NotFound();
            }

            return View(sOHeader);
        }

        // POST: SOHeader/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sOHeader = await _context.SOHeader.FindAsync(id);
            _context.SOHeader.Remove(sOHeader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SOHeaderExists(int id)
        {
            return _context.SOHeader.Any(e => e.SOHeaderID == id);
        }
    }
}
