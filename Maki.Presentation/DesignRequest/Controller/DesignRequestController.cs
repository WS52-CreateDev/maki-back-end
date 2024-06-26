using System.Net.Mime;
using AutoMapper;
using Maki.Domain.DesignRequest.Models.Commands;
using Maki.Domain.DesignRequest.Models.Queries;
using Maki.Domain.DesignRequest.Models.Response;
using Maki.Domain.DesignRequest.Services;
using Microsoft.AspNetCore.Mvc;

namespace maki_backend.DesignRequest.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignRequestController : ControllerBase
    {
        private readonly IDesignRequestCommandService _designRequestCommandService;
        private readonly IDesignRequestQueryService _designRequestQueryService;
        private readonly IMapper _mapper;

        public DesignRequestController(IDesignRequestCommandService designRequestCommandService,
            IDesignRequestQueryService designRequestQueryService, IMapper mapper)
        {
            _designRequestCommandService = designRequestCommandService;
            _designRequestQueryService = designRequestQueryService;
            _mapper = mapper;
        }

        // GET: api/DesignRequest
        ///<summary>Obtain all the active design requests</summary>
        /// <remarks> GET /api/DesignRequest </remarks>
        /// <response code="200">Returns all the design requests</response>
        /// <response code="404">If there are no design requests</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<DesignRequestResponse>), 200)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _designRequestQueryService.Handle(new GetAllDesignRequestsQuery());
            if (result.Count == 0) return NotFound();
            return Ok(result);
        }

        // GET: api/DesignRequest/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _designRequestQueryService.Handle(new GetDesignRequestByIdQuery(id));
            if (result == null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(result);
        }

        // POST: api/DesignRequest
        /// <summary>
        /// Creates a new design request.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/DesignRequest
        ///     {
        ///         "name": "string",
        ///         "characteristics": "string",
        ///         "photo": "string",
        ///         "email": "string",
        ///         "artisanId": 0
        ///     }
        /// </remarks>
        /// <param name="CreateDesignRequestCommand">The design request to create</param>
        /// <returns>A newly created design request</returns>
        /// <response code="201">Returns the newly created design request</response>
        /// <response code="400">If the design request has invalid property</response>
        /// <response code="409">Error validating data</response>
        /// <response code="500">Unexpected error</response>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> PostAsync([FromBody] CreateDesignRequestCommand command)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _designRequestCommandService.Handle(command);
            if (result > 0)
                return StatusCode(StatusCodes.Status201Created, result);
            return BadRequest();
        }

        // PUT: api/DesignRequest/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateDesignRequestCommand command)
        {
            command.Id = id;
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest);
            var result = await _designRequestCommandService.Handle(command);
            return Ok();
        }

        // DELETE: api/DesignRequest/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            DeleteDesignRequestCommand command = new DeleteDesignRequestCommand { Id = id };
            var result = await _designRequestCommandService.Handle(command);
            return Ok();
        }

        // GET: api/DesignRequest/artisan/{artisanId}
        [HttpGet("artisan/{artisanId}")]
        public async Task<IActionResult> GetDesignRequestsByArtisanId(int artisanId)
        {
            try
            {
                var result = await _designRequestQueryService.Handle(new GetDesignRequestsByArtisanIdQuery(artisanId));
                if (result == null || result.Count == 0)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                return StatusCode(500, "An error occurred while retrieving the data. See the inner exception for details.");
            }
        }
    }
}
