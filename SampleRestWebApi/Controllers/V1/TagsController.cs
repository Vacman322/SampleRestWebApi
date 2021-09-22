using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using SampleRestWebApi.Contracts.V1;
using SampleRestWebApi.Contracts.V1.Responses;
using SampleRestWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SampleRestWebApi.Contracts.V1.Requests;
using SampleRestWebApi.Domain;

namespace SampleRestWebApi.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TagsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;


        public TagsController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Tags.GetAll)]
        //[Authorize(Policy = "TagViewer")]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _clientService.GetAllTagsAsync();
            return Ok(_mapper.Map<List<TagResponse>>(tags));
        }

        [HttpGet(ApiRoutes.Tags.Get)]
        public async Task<IActionResult> Get([FromRoute] int tagId)
        {
            var tag = await _clientService.GetTagByIdAsync(tagId);

            if (tag is null)
                return NotFound();

            return Ok(_mapper.Map<TagResponse>(tag));
        }

        [HttpPost(ApiRoutes.Tags.Create)]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest tagRequest)
        {
            var tag = new Tag()
            {
                Name = tagRequest.Name
            };

            if ((await _clientService.GetTagByNameAsync(tagRequest.Name)) != null)
            {
                return BadRequest(new { Error = "Tag with the same name already exists" });
            }

            if (!await _clientService.CreateTagAsync(tag))
            {
                return BadRequest(new { Error = "Failed to create tag" });
            }        

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Tags.Get.Replace("{tagId}", tag.Name);
            return Created(locationUrl, _mapper.Map<TagResponse>(tag));
        }

        [HttpDelete(ApiRoutes.Tags.Delete)]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Policy = "MustWorkForVacman")]
        public async Task<IActionResult> Delete([FromRoute] int tagId)
        {

            var deleted = await _clientService.DeleteTagAsync(tagId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
