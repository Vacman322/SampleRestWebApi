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
using AutoMapper;

namespace SampleRestWebApi.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientController(IClientService clientService, IMapper mapper = null)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Clients.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientService.GetAllAsync();
            return Ok(_mapper.Map<List<ClientResponse>>(clients));
        }

        [HttpGet(ApiRoutes.Clients.Get)]
        public async Task<IActionResult> Get([FromRoute] int clientId)
        {
            var client = await _clientService.GetClientByIdAsync(clientId);

            if (client is null)
                return NotFound();

            return Ok(_mapper.Map<ClientResponse>(client));
        }

        [HttpPost(ApiRoutes.Clients.Create)]
        public async Task<IActionResult> Create([FromBody]CreateClientRequest clientRequest)
        {
            var client = new Client() {
                Name = clientRequest.Name,
                LastName = clientRequest.LastName,
                Patronymic = clientRequest.Patronymic,
                BirthDay = clientRequest.BirthDay,
                PhoneNumber = clientRequest.PhoneNumber,
                Email = clientRequest.Email, 
                UserId = HttpContext.GetUserId(),
                ClientTags = clientRequest.Tags.Select(r => new ClientTag { Tag = new Tag { Name = r.Name} }).ToList()
            };

            if(!await _clientService.CreateClientAsync(client))
            {
                return BadRequest("Failed to create client");
            }

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Clients.Get.Replace("{clientId}", client.Id.ToString());
            return Created(locationUrl, _mapper.Map<ClientResponse>(client));
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
            client.LastName = request.LastName;
            client.Patronymic = request.Patronymic;
            client.BirthDay = request.BirthDay;
            client.PhoneNumber = request.PhoneNumber;
            client.Email = request.Email;
            client.ClientTags.RemoveAll(r => true);
            client.ClientTags = request.Tags.Select(r => new ClientTag { Tag = new Tag { Name = r.Name } }).ToList();


            var updated = await _clientService.UpdateClientAsync(client);
            if (updated)
                return Ok(_mapper.Map<ClientResponse>(client));

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Clients.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int clientId)
        {
            var userOwnsClient = await _clientService.UserOwnClientAsync(clientId, HttpContext.GetUserId());

            if (!userOwnsClient)
            {
                return BadRequest(new { Error = "You do not own this Client" });
            }

            var deleted = await _clientService.DeleteClientAsync(clientId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
