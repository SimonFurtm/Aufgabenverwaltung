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
    //api/aufgabenapi
    [Route("api/[controller]")]
    [ApiController]
    public class AufgabenApiController : ControllerBase
    {
        //Datenbank-Kontext Objekt
        private readonly WebAppContext _context;

        public AufgabenApiController(WebAppContext context)
        {
            _context = context;
        }

        // GET: api/AufgabenApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aufgabe>>> GetAufgabe()
        {
            //Wenn es kein Aufgaben gibt
            if (_context.Aufgabe == null)
            {
                //HTTP-Antwort mit 403 nicht gefunden
                return NotFound();
            }
            //Antwort als Aufgabenliste
            return await _context.Aufgabe.ToListAsync();
        }

        // GET: api/AufgabenApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aufgabe>> GetAufgabe(int id)
        {
            //Wenn es keine Aufgaben gibt
            if (_context.Aufgabe == null)
            {
                //HTTP-Antwort mit 403 nicht gefunden
                return NotFound();
            }

            //Finde eine Aufgabe mit der Id
            var aufgabe = await _context.Aufgabe.FindAsync(id);

            //Wenn es diese gewisse Aufgabe nicht gibt
            if (aufgabe == null)
            {
                //HTTP-Antwort mit 403 nicht gefunden
                return NotFound();
            }

            //Aufgabe als HTTP-Antwort
            return aufgabe;
        }

        // PUT: api/AufgabenApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAufgabe(int id, Aufgabe aufgabe)
        {
            //Wenn die Ids aus dem feld id und aufgabe.id nicht übereinstimmen
            if (id != aufgabe.Id)
            {
                //HTTP-Antwort 400 Fehlerhafte Anfrage
                return BadRequest();
            }

            //Änderung eines Datenbankeintrages mit EntityFramework
            _context
                .Entry(aufgabe)
                .State = EntityState.Modified;

            //Versuche die Änderungen zu übernehmen
            await _context.SaveChangesAsync();

            //Die erfolgreiche HTTP-Verarbeitung wird bestätigt (Code 200)
            return Ok();
        }

        // POST: api/AufgabenApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aufgabe>> PostAufgabe(Aufgabe aufgabe)
        {
            //Aufgabe hizufügen
            _context.Aufgabe.Add(aufgabe);
            //Änderungen übernehmen
            await _context.SaveChangesAsync();
            //Erfolgreiche Verarbeitung bestätigen
            return Ok();
        }

        // DELETE: api/AufgabenApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAufgabe(int id)
        {
            //Wenn es keine Aufgaben gibt
            if (_context.Aufgabe == null)
            {
                return NotFound();
            }
            //Aufgabe finden
            var aufgabe = await _context.Aufgabe.FindAsync(id);
            //Wenn es genau diese Aufgabe nicht gibt
            if (aufgabe == null)
            {
                return NotFound();
            }
            //Aufgabe entfernen
            _context.Aufgabe.Remove(aufgabe);
            //Änderungen übernehmen
            await _context.SaveChangesAsync();
            //Erfolgreiche Verarbeitung bestätigen (HTTP 200)
            return Ok();
        }

        //Helferfunktion, um zu prüfen, ob Aufgabe existiert
        private bool AufgabeExists(int id)
        {
            return (_context.Aufgabe?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
