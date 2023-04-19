using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Data;
using System;
using System.Linq;

namespace WebApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //stellt sicher, dass die Context-Instanz ordnungsgemäß verwaltet wird 
            using (var context = new WebAppContext(
                serviceProvider.GetRequiredService<DbContextOptions<WebAppContext>>())) //EF
            {
                // Überprüfen, ob bereits Aufgaben in der Datenbank vorhanden sind
                if (context.Aufgabe.Any())
                {
                    return;   // Datenbank wurde bereits mit Daten befüllt, daher abbrechen
                }

                // Hinzufügen von Beispieldaten in die Datenbank
                context.Aufgabe.AddRange(
                    new Aufgabe
                    {
                        Titel = "Einkaufsliste",
                        Beschreibung = "Erstellen einer Einkaufsliste für den wöchentlichen Einkauf",
                        Fälligkeitsdatum = DateTime.Parse("2023-04-12 10:00 AM"),
                        Erstelldatum = DateTime.Parse("2023-04-11 2:30 PM"),
                        Abgeschlossen = false
                    },
                    new Aufgabe
                    {
                        Titel = "Projektpräsentation",
                        Beschreibung = "Vorbereitung einer Projektpräsentation für das Teammeeting",
                        Fälligkeitsdatum = DateTime.Parse("2023-04-13 3:45 PM"),
                        Erstelldatum = DateTime.Parse("2023-04-10 9:15 AM"),
                        Abgeschlossen = true
                    },
                    new Aufgabe
                    {
                        Titel = "Sportübung",
                        Beschreibung = "Durchführung einer 30-minütigen Sportübungseinheit",
                        Fälligkeitsdatum = DateTime.Parse("2023-04-14 1:00 PM"),
                        Erstelldatum = DateTime.Parse("2023-04-09 5:30 PM"),
                        Abgeschlossen = false
                    },
                    new Aufgabe
                    {
                        Titel = "Dokumentation aktualisieren",
                        Beschreibung = "Aktualisierung der Projekt-Dokumentation mit den neuesten Informationen",
                        Fälligkeitsdatum = DateTime.Parse("2023-04-15 8:30 AM"),
                        Erstelldatum = DateTime.Parse("2023-04-08 4:00 PM"),
                        Abgeschlossen = true
                    }
                );

                context.SaveChanges(); // Speichern der Änderungen in der Datenbank
            }
        }
    }
}
