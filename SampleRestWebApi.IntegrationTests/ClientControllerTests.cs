using SampleRestWebApi.Contracts.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using SampleRestWebApi.Domain;
using NUnit.Framework;
using SampleRestWebApi.Contracts.V1.Requests;

namespace SampleRestWebApi.IntegrationTests
{
    class ClientControllerTests : IntegrationTest
    {
        [TearDown]
        public void Cleanup()
        {
            Dispose();
        }

        [Test]
        public async Task GetAll_WithoutAnyClient_ReturnsEmptyResponse()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync(ApiRoutes.Clients.GetAll);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Client>>()).Should().BeEmpty();
        }

        [Test]
        public async Task Get_ReturnsClient_WhenClientExistsInDatabase()
        {
            // Arrange
            await AuthenticateAsync();
            var createdClient = await CreateClientAsync(new CreateClientRequest { Name = "Vasya" });

            // Act
            var response = await TestClient.GetAsync(ApiRoutes.Clients.Get.Replace("{clientId}", createdClient.Id.ToString()));
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedClient = await response.Content.ReadAsAsync<Client>();
            returnedClient.Id.Should().Be(createdClient.Id);
            returnedClient.Name.Should().Be("Vasya");
        }
    }
}
