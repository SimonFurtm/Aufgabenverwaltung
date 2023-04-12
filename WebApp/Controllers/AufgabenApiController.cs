using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AufgabenApiController : ControllerBase
    {
        private readonly WebAppContext _context;

        public AufgabenApiController(WebAppContext context)
        {
            _context = context;
        }

        // GET: api/AufgabenApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aufgabe>>> GetAufgabe()
        {
          if (_context.Aufgabe == null)
          {
              return NotFound();
          }
            return await _context.Aufgabe.ToListAsync();
        }

        // GET: api/AufgabenApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aufgabe>> GetAufgabe(int id)
        {
          if (_context.Aufgabe == null)
          {
              return NotFound();
          }
            var aufgabe = await _context.Aufgabe.FindAsync(id);

            if (aufgabe == null)
            {
                return NotFound();
            }

            return aufgabe;
        }

        // PUT: api/AufgabenApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAufgabe(int id, Aufgabe aufgabe)
        {
            if (id != aufgabe.Id)
            {
                return BadRequest();
            }

            _context.Entry(aufgabe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AufgabeExists(id))
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

        // POST: api/AufgabenApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aufgabe>> PostAufgabe(Aufgabe aufgabe)
        {
          if (_context.Aufgabe == null)
          {
              return Problem("Entity set 'WebAppContext.Aufgabe'  is null.");
          }
            _context.Aufgabe.Add(aufgabe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAufgabe", new { id = aufgabe.Id }, aufgabe); //wos mocht des 🤡🤡🤡🤡🤡
        }

        // DELETE: api/AufgabenApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAufgabe(int id)
        {
            if (_context.Aufgabe == null)
            {
                return NotFound();
            }
            var aufgabe = await _context.Aufgabe.FindAsync(id);
            if (aufgabe == null)
            {
                return NotFound();
            }

            _context.Aufgabe.Remove(aufgabe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AufgabeExists(int id)
        {
            return (_context.Aufgabe?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
