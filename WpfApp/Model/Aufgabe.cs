using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp.Model
{
    public class Aufgabe
    {
        public int Id { get; set; } // Eigenschaft für die Aufgaben-ID

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Titel { get; set; } // Eigenschaft für den Titel der Aufgabe

        [StringLength(254, MinimumLength = 3)] //max. Zeichen von String = 254 (256 Bytes)
        public string Beschreibung { get; set; } // Eigenschaft für die Beschreibung der Aufgabe

        [Required]
        [Display(Name = "FälligkeitsDatum")] // Anzeigename für das Feld Fälligkeitsdatum
        [DataType(DataType.DateTime)] // Datentyp des Felds Fälligkeitsdatum als DateTime
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)] // Formatierung des Datums und der Uhrzeit
        public DateTime Fälligkeitsdatum { get; set; } // Eigenschaft für das Fälligkeitsdatum der Aufgabe

        [Required]
        [Display(Name = "ErstellDatum")] // Anzeigename für das Feld Erstellungsdatum
        [DataType(DataType.DateTime)] // Datentyp des Felds Erstellungsdatum als DateTime
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)] // Formatierung des Datums und der Uhrzeit
        public DateTime Erstelldatum { get; set; } // Eigenschaft für das Erstellungsdatum der Aufgabe

        public bool Abgeschlossen { get; set; } // Eigenschaft für den Status der Aufgabe (abgeschlossen oder nicht)
    }
}
