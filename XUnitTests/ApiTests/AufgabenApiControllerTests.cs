using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using WebApp.Controllers;
using WebApp.Data;
using WebApp.Models;

namespace XUnitTests.ApiTests
{
    public class AufgabenApiControllerTests
    {
        [Fact]
        public async void GetAufgaben()
        {
            // Testdaten
            var data = new List<Aufgabe>
            {
                new Aufgabe {
                    Id = 0,
                    Titel = "Aufgabe",
                    Beschreibung= "Hallo ich bin eine Aufgabe"
                }
            }.AsQueryable(); //Zum Mocken als Datensatz wird es als IQueryable benötigt 

            //--DBMOCK
            //Eine leere Optionen-Klasse wird für die Erstellung des Mocks der Context-Klasse benötigt (Abhängikeit)
            var options = new DbContextOptions<WebAppContext>(); 
            
            //Die Controller-Klasse benötigt den Datenbankkontext, dieser wird mithilfe von Moq simuliert
            var webAppContextMock = new Mock<WebAppContext>(options);
            
            webAppContextMock
                .Setup(context => context.Aufgabe) //Bei Aufruf der "Aufgabe"-Funktion des Context
                .ReturnsDbSet(data); //Wird ein Datensatz basierend auf den Testdaten zurückgegeben
            //--DBMOCK

            //Erstellung des AufgabenApiController-Services mit der simulierten Datenbank-Klasse
            var service = new AufgabenApiController(webAppContextMock.Object);

            //GetAufgabe wird aufgerufen
            var result = service.GetAufgabe();

            //Die Werte aus dem Resultat
            var aufgaben = result.Result.Value;

            //Ist die Methode OK (HTTP)
            Assert.True(result.IsCompletedSuccessfully);
            //Sind Aufgaben zurückgekommen
            Assert.NotNull(aufgaben);
            //Anzahl der Aufgaben
            Assert.Equal(1, aufgaben?.Count());
            //Id der ersten Aufgabe
            Assert.Equal(0, aufgaben?.First().Id);
            //Titel der ersten Aufgabe
            Assert.Equal("Aufgabe", aufgaben?.First().Titel);
        }
    }
}
