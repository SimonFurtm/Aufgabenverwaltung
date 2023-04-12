using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xunit;

namespace XUnitTests.ApiTests
{
    public class ApiGetRequestTest
    {
        private const string API_URL = "https://localhost:44325/api/AufgabenAPI";

        [Fact]
        public async Task Test_GetAufgaben()
        {
            // Arrange
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, API_URL);

            // Act
            HttpResponseMessage response = await httpClient.SendAsync(request);

            // Assert
            Assert.True(response.IsSuccessStatusCode, "HTTP GET-Request war nicht erfolgreich");
            // Hier können weitere Assertions für den Response-Inhalt oder andere Eigenschaften des Response-Objekts hinzugefügt werden
        }
    }
}
