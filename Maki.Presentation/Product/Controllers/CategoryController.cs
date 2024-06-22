using AutoMapper;
using Maki.Domain.Product.Models.Commands;
using Maki.Domain.Product.Models.Queries;
using Maki.Domain.Product.Services;
using Microsoft.AspNetCore.Mvc;

namespace Maki.Presentation.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryCommandService _categoryCommandService;
        private readonly ICategoryQueryService _categoryQueryService;
        private readonly IMapper _mapper;
        
        public CategoryController(ICategoryCommandService categoryCommandService, ICategoryQueryService categoryQueryService, IMapper mapper)
        {
            _categoryCommandService = categoryCommandService;
            _categoryQueryService = categoryQueryService;
            _mapper = mapper;
        }
        
        // GET: api/Category
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _categoryQueryService.Handle(new GetAllCategoriesQuery());
            if (result.Count == 0) return NotFound();
            return Ok(result);
        }
        
        // GET: api/Category/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _categoryQueryService.Handle(new GetCategoryByIdQuery(id));
            
            if (result != null)
                return Ok(result);

            return StatusCode(StatusCodes.Status404NotFound);
        }
        
        // POST: api/Category
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateCategoryCommand command)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _categoryCommandService.Handle(command);
            if (result > 0)
                return StatusCode(StatusCodes.Status201Created, result);
            return BadRequest();
        }
        
        //PUT: api/Category/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateCategoryCommand command)
        {
            command.Id = id;
            if(!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest);
            var result = await _categoryCommandService.Handle(command);
            return Ok();
        }
        
        // DELETE: api/Category/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            DeleteCategoryCommand command = new DeleteCategoryCommand { Id = id };
            var result = await _categoryCommandService.Handle(command);
            return Ok();
        }

    }
    
}

