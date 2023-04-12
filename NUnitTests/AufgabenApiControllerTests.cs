using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using WebApp.Controllers;
using WebApp.Data;
using WebApp.Models;

namespace NUnitTests
{
    public class AufgabenApiControllerTests
    {
        [Test]
        public void PostAufgabe()
        {
            // Testdaten
            var data = new List<Aufgabe>
            {

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

            //PutAufgabe wird aufgerufen
            var result = service.PostAufgabe(new Aufgabe()
            {
                Id = 1,
                Titel = "Aufgabe",
                Beschreibung = "Ich bin eine Aufgabe"
            });

            //Ist die Methode OK (HTTP)
            Assert.True(result.IsCompletedSuccessfully);
        }
    }
}