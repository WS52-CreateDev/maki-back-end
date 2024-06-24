using AutoMapper;
using Maki.Domain.Customer.Models.Commands;
using Maki.Domain.Customer.Models.Queries;
using Maki.Domain.Customer.Services;
using Microsoft.AspNetCore.Mvc;

namespace maki_backend.Customer.Controller;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerCommandService _customerCommandService;
    private readonly ICustomerQueryService _customerQueryService;
    private readonly IMapper _mapper;

    
   
    public CustomerController(ICustomerCommandService customerCommandService, ICustomerQueryService customerQueryService,  IMapper mapper)   
    {
        _customerCommandService = customerCommandService;
        _customerQueryService = customerQueryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _customerQueryService.Handle(new GetAllCustomersQuery());
        if (result.Count == 0) return NotFound();
        return Ok(result);
    }
    
    // GET: api/Category/id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var result = await _customerQueryService.Handle(new GetCustomerByIdQuery(id));
            
        if (result != null)
            return Ok(result);

        return StatusCode(StatusCodes.Status404NotFound);
    }
    
 
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] RegisterCustomerCommand command)
    {
        if (!ModelState.IsValid) return BadRequest();
        var result = await _customerCommandService.Handle(command);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateCustomerCommand command)
    {
        command.Id = id;
        if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest);

        var result = await _customerCommandService.Handle(command);
        if (!result) return StatusCode(StatusCodes.Status500InternalServerError);
    
        return Ok();
    }
//copia
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _customerCommandService.Handle(new DeleteCustomerCommand { Id = id });
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginCustomerCommand command)
    {
        if (!ModelState.IsValid) return BadRequest();
        var result = await _customerCommandService.Handle(command);
        if (result == null) return Unauthorized();
        return Ok(result);
    }
}