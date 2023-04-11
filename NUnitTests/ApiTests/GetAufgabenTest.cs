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
    [TestFixture]
    public class GetAufgabenTest
    {
        private AufgabenController _aufgabenController;
        private Mock<WebAppContext> _mockWebAppContext;

        [SetUp]
        public void SetUp()
        {
            // Set up the mock DbContext
            _mockWebAppContext = new Mock<WebAppContext>(new object[] { }); // Use Strict behavior

            // Create the controller with the mock DbContext
            _aufgabenController = new AufgabenController(_mockWebAppContext.Object);
        }

        [Test]
        public async Task Index_ReturnsViewWithAufgaben()
        {
            // Arrange
            var aufgabenList = new List<Aufgabe>
            {
                new Aufgabe { Id = 1, Titel = "Aufgabe 1", Beschreibung = "Beschreibung 1", Fälligkeitsdatum = DateTime.Now, Erstelldatum = DateTime.Now, Abgeschlossen = false },
                new Aufgabe { Id = 2, Titel = "Aufgabe 2", Beschreibung = "Beschreibung 2", Fälligkeitsdatum = DateTime.Now, Erstelldatum = DateTime.Now, Abgeschlossen = false }
            };

            var mockAufgabenDbSet = new Mock<DbSet<Aufgabe>>(); // Create a mock DbSet
            mockAufgabenDbSet.As<IQueryable<Aufgabe>>().Setup(m => m.Provider).Returns(aufgabenList.AsQueryable().Provider);
            mockAufgabenDbSet.As<IQueryable<Aufgabe>>().Setup(m => m.Expression).Returns(aufgabenList.AsQueryable().Expression);
            mockAufgabenDbSet.As<IQueryable<Aufgabe>>().Setup(m => m.ElementType).Returns(aufgabenList.AsQueryable().ElementType);
            mockAufgabenDbSet.As<IQueryable<Aufgabe>>().Setup(m => m.GetEnumerator()).Returns(aufgabenList.AsQueryable().GetEnumerator());

            _mockWebAppContext.Setup(m => m.Aufgabe).Returns(mockAufgabenDbSet.Object);

            // Act
            var result = await _aufgabenController.Index();
            var viewResult = result as ViewResult;
            var model = viewResult?.Model as List<Aufgabe>;

            // Assert
            Assert.IsNotNull(viewResult);
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count);
        }

        [Test]
        public async Task Index_ReturnsProblemWhenAufgabenNull()
        {
            // Arrange
            _mockWebAppContext.Setup(m => m.Aufgabe).Returns((DbSet<Aufgabe>)null);

            // Act
            var result = await _aufgabenController.Index();
            var problemResult = result as ProblemDetails;

            // Assert
            Assert.IsNotNull(problemResult);
            Assert.AreEqual("Entity set 'WebAppContext.Aufgabe' is null.", problemResult.Title);
        }
    }

}
