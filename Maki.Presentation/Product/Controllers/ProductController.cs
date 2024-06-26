using System.Net.Mime;
using AutoMapper;
using maki_backend.Filters;
using Maki.Domain.Product.Models.Commands;
using Maki.Domain.Product.Models.Queries;
using Maki.Domain.Product.Models.Response;
using Maki.Domain.Product.Services;
using Maki.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Maki.Presentation.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductCommandService _productCommandService;
        private readonly IProductQueryService _productQueryService;
        private readonly IMapper _mapper;
        
        public ProductController(IProductCommandService productCommandService, IProductQueryService productQueryService, IMapper mapper)
        {
            _productCommandService = productCommandService;
            _productQueryService = productQueryService;
            _mapper = mapper;
        }
        
        // GET: api/Product
        ///<summary>Obtain all the active products</summary>
        /// <remarks> GET /api/Product </remarks>
        /// <response code="200">Returns all the products</response>
        /// <response code="404">If there are no products</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet]
        [ProducesResponseType( typeof(List<ProductResponse>), 200)]
        [ProducesResponseType( typeof(void),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void),StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _productQueryService.Handle(new GetAllProductsQuery());
            if (result.Count == 0) return NotFound();
            return Ok(result);
        }
        
        // GET: api/Product/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _productQueryService.Handle(new GetProductByIdQuery(id));
            if (result==null) StatusCode(StatusCodes.Status404NotFound);
            return Ok(result);
        }

        // POST: api/Product
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/Product
        ///     {
        ///         "name": "string",
        ///         "description": "string",
        ///         "price": 0,
        ///         "Image": "string",
        ///         "CategoryId": 0,
        ///         "ArtisanId": 0,
        ///         "Width": "string",
        ///         "Height": "string",
        ///         "Depth": "string",
        ///         "Material": "string"
        ///     }
        /// </remarks>
        /// <param name="CreateProductCommand">The product to create</param>
        /// <returns>A newly created product</returns>
        /// <response code="201">Returns the newly created product</response>
        /// <response code="400">If the product has invalid property</response>
        /// <response code="409">Error validating data</response>
        /// <response code="500">Unexpected error</response>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(void),StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [CustomAuthorize("artisan", "admin")]
        public async Task<IActionResult> PostAsync([FromBody] CreateProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo inválido.");
            }
            try
            {
                var result = await _productCommandService.Handle(command);
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, result);
                }
                return BadRequest("Error al crear el producto.");
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "No autorizado.");
            }
            catch (ForbiddenAccessException)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Su rol de usuario no posee acceso a este recurso.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno: {ex.Message}");
            }
        }

        //PUT: api/Product/id
        [HttpPut("{id}")]
        [CustomAuthorize("artisan", "admin")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateProductCommand command)
        {
            command.Id = id;
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest);
            try
            {
                var result = await _productCommandService.Handle(command);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "No autorizado.");
            }
            catch (ForbiddenAccessException) 
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Su rol de usuario no posee acceso a este recurso.");
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno: {ex.Message}");
            }
        }

        // DELETE: api/Product/id
        [HttpDelete("{id}")]
        [CustomAuthorize("artisan", "admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            DeleteProductCommand command = new DeleteProductCommand { Id = id };
            try
            {
                var result = await _productCommandService.Handle(command);
                if (result)
                {
                    return Ok("Producto eliminado exitosamente.");
                }
                return NotFound("Producto no encontrado.");
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "No autorizado.");
            }
            catch (ForbiddenAccessException) 
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Su rol de usuario no posee acceso a este recurso.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno: {ex.Message}");
            }
        }
    }
}
