using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using SampleRestWebApi.Contracts.V1;
using SampleRestWebApi.Contracts.V1.Requests;
using SampleRestWebApi.Contracts.V1.Responses;
using SampleRestWebApi.Domain;
using SampleRestWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SampleRestWebApi.Extensions;

namespace SampleRestWebApi.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientController : Controller
    {
        private IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet(ApiRoutes.Clients.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _clientService.GetAllAsync());
        }

        [HttpGet(ApiRoutes.Clients.Get)]
        public async Task<IActionResult> Get([FromRoute] int clientId)
        {
            var client = await _clientService.GetClientByIdAsync(clientId);

            if (client is null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost(ApiRoutes.Clients.Create)]
        public async Task<IActionResult> Create([FromBody]CreateClientRequest clientRequest)
        {
            var client = new Client() {
                Name = clientRequest.Name,
                UserId = HttpContext.GetUserId()
            };
            await _clientService.CreateClientAsyn(client);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Clients.Get.Replace("{clientId}", client.Id.ToString());
            return Created(locationUrl, new ClientResponse() {Id = client.Id });
        }

        [HttpPut(ApiRoutes.Clients.Update)]
        public async Task<IActionResult> Update([FromRoute] int clientId,[FromBody] UpdateClientRequest request)
        {
            var userOwnsPost = await _clientService.UserOwnClientAsync(clientId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { Error = "You do not own this Client" });
            }

            var client = await _clientService.GetClientByIdAsync(clientId);
            client.Name = request.Name;

            var updated = await _clientService.UpdateClientAsyn(client);
            if (updated)
                return Ok(client);

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Clients.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int clientId)
        {
            var userOwnsPost = await _clientService.UserOwnClientAsync(clientId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { Error = "You do not own this Client" });
            }

            var deleted = await _clientService.DeleteClientAsyn(clientId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
