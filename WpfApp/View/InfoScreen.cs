using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Net.Http;
using WpfApp.Model;

namespace WpfApp.View
{
    class InfoScreen : Window
    {
        public InfoScreen(Aufgabe aufgabe)
        {
            // Set the title of the popup window
            Title = "Additional Information: " + aufgabe.Titel;
            Width = 400;
            Height = 200;

            // Create a TextBlock to display the additional information
            var infoTextBlock = new TextBlock();
            infoTextBlock.Text = "Titel: " + aufgabe.Titel + "\n" +
                                    "Beschreibung: " + aufgabe.Beschreibung + "\n" +
                                    "Fälligkeitsdatum: " + aufgabe.Fälligkeitsdatum + "\n" +
                                    "Erstelldatum: " + aufgabe.Erstelldatum + "\n" +
                                    "Abgeschlossen: " + aufgabe.Abgeschlossen;

            // Create a Button for sending the HTTPDelete request
            var deleteButton = new Button();
            deleteButton.Content = "Delete";
            deleteButton.Click += async (sender, e) =>
            {
                // Create an instance of HttpClient
                var httpClient = new HttpClient();

                // Send the HTTPDelete request to the API endpoint
                HttpResponseMessage response = await httpClient.DeleteAsync("https://localhost:7047/api/AufgabenApi/" + aufgabe.Id);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Delete request was successful.");
                }
                else
                {
                    MessageBox.Show("Delete request failed.");
                }

                // Dispose the HttpClient instance
                httpClient.Dispose();
            };

            // Add the TextBlock and Button to a StackPanel
            var stackPanel = new StackPanel();
            stackPanel.Children.Add(infoTextBlock);
            stackPanel.Children.Add(deleteButton);

            // Set the StackPanel as the Content of the Window
            Content = stackPanel;
        }
    }
}
