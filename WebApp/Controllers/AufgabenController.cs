using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AufgabenController : Controller
    {
        private readonly WebAppContext _context;

        public AufgabenController(WebAppContext context)
        {
            _context = context;
        }

        // GET: Aufgaben
        public async Task<IActionResult> Index()
        {
              return _context.Aufgabe != null ? 
                          View(await _context.Aufgabe.ToListAsync()) :
                          Problem("Entity set 'WebAppContext.Aufgabe'  is null.");
        }

        // GET: Aufgaben/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Aufgabe == null)
            {
                return NotFound();
            }

            var aufgabe = await _context.Aufgabe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aufgabe == null)
            {
                return NotFound();
            }

            return View(aufgabe);
        }

        // GET: Aufgaben/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aufgaben/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titel,Beschreibung,Fälligkeitsdatum,Erstelldatum,Abgeschlossen")] Aufgabe aufgabe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aufgabe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aufgabe);
        }

        // GET: Aufgaben/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Aufgabe == null)
            {
                return NotFound();
            }

            var aufgabe = await _context.Aufgabe.FindAsync(id);
            if (aufgabe == null)
            {
                return NotFound();
            }
            return View(aufgabe);
        }

        // POST: Aufgaben/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titel,Beschreibung,Fälligkeitsdatum,Erstelldatum,Abgeschlossen")] Aufgabe aufgabe)
        {
            if (id != aufgabe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aufgabe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AufgabeExists(aufgabe.Id))
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
            return View(aufgabe);
        }

        // GET: Aufgaben/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Aufgabe == null)
            {
                return NotFound();
            }

            var aufgabe = await _context.Aufgabe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aufgabe == null)
            {
                return NotFound();
            }

            return View(aufgabe);
        }

        // POST: Aufgaben/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Aufgabe == null)
            {
                return Problem("Entity set 'WebAppContext.Aufgabe'  is null.");
            }
            var aufgabe = await _context.Aufgabe.FindAsync(id);
            if (aufgabe != null)
            {
                _context.Aufgabe.Remove(aufgabe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AufgabeExists(int id)
        {
          return (_context.Aufgabe?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
