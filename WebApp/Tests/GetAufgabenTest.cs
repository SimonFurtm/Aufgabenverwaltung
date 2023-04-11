using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using WebApp.Controllers;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Tests
{
    //wird nicht aufgerufen aufgrund verschiedener Konfigurationen der Tests
    public class GetAufgabenTest
    {

        private AufgabenApiController _controller;
        private Mock<WebAppContext> _contextMock;

        [SetUp]
        public void SetUp()
        {
            // Erstellen des Mock-Objekts für den WebAppContext
            _contextMock = new Mock<WebAppContext>();

            // Hinzufügen von Beispiel-Testdaten zum Mock-WebAppContext
            _contextMock.Setup(c => c.Aufgabe)
                .Returns((DbSet<Aufgabe>)new[]{
                    new Aufgabe
                    {
                        Id = 1,
                        Titel = "Aufgabe 1",
                        Beschreibung = "Beschreibung 1",
                        Fälligkeitsdatum = DateTime.Now.AddDays(1),
                        Erstelldatum = DateTime.Now,
                        Abgeschlossen = false
                    },
                    new Aufgabe
                    {
                        Id = 2,
                        Titel = "Aufgabe 2",
                        Beschreibung = "Beschreibung 2",
                        Fälligkeitsdatum = DateTime.Now.AddDays(2),
                        Erstelldatum = DateTime.Now,
                        Abgeschlossen = false
                    }
                }.AsQueryable());

            // Erstellen der AufgabenApiController mit dem Mock-WebAppContext
            _controller = new AufgabenApiController(_contextMock.Object);
        }

        [Test]
        public async Task GetAufgabe_ReturnsListOfAufgaben()
        {
            // Act
            var result = await _controller.GetAufgabe();

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<Aufgabe>>>(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(2, result.Value.Count());
        }

        [Test]
        public async Task GetAufgabe_ReturnsNotFound_WhenAufgabenListIsEmpty()
        {
            // Arrange
            Moq.Language.Flow.IReturnsResult<WebAppContext> returnsResult = _contextMock.Setup(c => c.Aufgabe).Returns((DbSet<Aufgabe>)(IQueryable<Aufgabe>)null);

            // Act
            var result = await _controller.GetAufgabe();

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
