using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class WebAppContext : DbContext // Definiert einen DbContext für die Datenbankkommunikation
    {
        public WebAppContext(DbContextOptions<WebAppContext> options) //neue instanz von DBContext
            : base(options) // Konstruktor, der DbContextOptions entgegennimmt und an die Basisklasse DbContext weiterleitet
        {
        }
        // Eigenschaft, die die DbSet für die Tabelle 'Aufgabe' in der Datenbank darstellt (Wird automatisch erstellt)
        public virtual DbSet<WebApp.Models.Aufgabe> Aufgabe { get; set; } = default!; 
    }
}
