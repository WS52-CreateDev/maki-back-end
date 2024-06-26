using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Maki.Domain.Artisan.Models.Commands;
using Maki.Domain.Artisan.Models.Queries;
using Maki.Domain.Artisan.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace maki_backend.Artisans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ArtisanController : ControllerBase
    {
        private readonly IArtisanCommandService _artisanCommandService;
        private readonly IArtisanQueryService _artisanQueryService;
        private readonly IMapper _mapper;


        public ArtisanController(IArtisanCommandService artisanCommandService, IArtisanQueryService artisanQueryService, IMapper mapper)
        {
            _artisanCommandService = artisanCommandService;
            _artisanQueryService = artisanQueryService;
            _mapper = mapper;
        }
        
        //Get: api/artisans/
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _artisanQueryService.Handle(new GetAllArtisansQuery());
            if (result.Count == 0) return NotFound();

            return Ok(result);
        }
        
        //Get: api/artisans/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _artisanQueryService.Handle(new GetArtisanByIdQuery(id));
            if (result != null)
                return Ok(result);
            return StatusCode(StatusCodes.Status404NotFound);
        }
        
        //Post: api/artisans
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RegisterArtisanCommand command)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _artisanCommandService.Handle(command);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        //Put: api/artisans/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateArtisanCommand command)
        {
            command.Id = id;
            if (!ModelState.IsValid) return BadRequest();
            var result = await _artisanCommandService.Handle(command);
            if (!result) return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok();
        }
        
        
    }
    
}