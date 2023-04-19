using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.Model;
using WpfApp.View;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            getData();
        }

        private async void getData()
        {
            var apiUrl = "https://localhost:7047/api/AufgabenApi";
            try
            {
                //using (x wird nur im using verwendet){...} besser für speicher
                using (HttpClient Client = new HttpClient())
                {
                    try
                    {
                        using (HttpResponseMessage response = await Client.GetAsync(apiUrl))
                        {
                            HttpContent content = response.Content;

                            List<Aufgabe> apiData = await response.Content.ReadFromJsonAsync<List<Aufgabe>>();

                            if (apiData != null)
                            {

                                for (int i = 0; i < apiData.Count; i++)
                                {
                                    dataListView.Items.Add(apiData[i]);
                                }
                                Console.WriteLine("Data Found.");
                                consoleLabel.Content = "Suppa ois guad :)";
                            }
                            else
                            {
                                Console.WriteLine("No Data?");
                                consoleLabel.Content = "No Data?";
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        consoleLabel.Content = "API offline!";
                        System.Threading.Thread.Sleep(1000);
                        getData();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                consoleLabel.Content = ex.StackTrace;
            }

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAufgabe = (Aufgabe)dataListView.SelectedValue;

            if (selectedAufgabe != null)
            {
                var infoWindow = new InfoScreen(selectedAufgabe);
                infoWindow.Show();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAufgabe = (Aufgabe)dataListView.SelectedValue;

            if (selectedAufgabe != null)
            {
                var infoWindow = new InfoScreen(selectedAufgabe);
                infoWindow.Show();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataListView.Items.Clear();
            getData();
        }
    }
}
