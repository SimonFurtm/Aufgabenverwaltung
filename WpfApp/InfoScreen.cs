using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    class InfoScreen : Window
	{
		public InfoScreen(Aufgabe aufgabe)
		{
			// Set the title of the popup window
			this.Title = "Additional Information: " + aufgabe.Titel;
			this.Width = 400;
			this.Height = 200;

			// Create a TextBlock to display the additional information
			var uniInfoTextBlock = new TextBlock();
			uniInfoTextBlock.Text = "Titel: " + aufgabe.Titel + "\n" +
									"Beschreibung: " + aufgabe.Beschreibung + "\n" +
									"Fälligkeitsdatum: " + aufgabe.Fälligkeitsdatum + "\n" +
									"Erstelldatum: " + aufgabe.Erstelldatum + "\n" +
									"Abgeschlossen: " + aufgabe.Abgeschlossen;

			// Add the TextBlock to the popup window's content
			this.Content = uniInfoTextBlock;
		}
	}
}
