using Maki.Domain.IAM.Models.Commands;
using Maki.Domain.IAM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maki_backend.IAM.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserCommandService _userCommandService;
        
        public UserController(IUserCommandService userCommandService)
        {
            _userCommandService = userCommandService;
        }
        
        // GET: api/User
        [HttpGet]
        [Route("getall")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/id
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost("register")]
        [AllowAnonymous]
        
        public async Task<IActionResult> RegisterAsync([FromBody] SignUpCommand command)
        {
            var result =  await _userCommandService.Handle(command);
            return CreatedAtAction("register", new { id = result });
        }
        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] SignInCommand command)
        {
            var result =  await _userCommandService.Handle(command);
            return CreatedAtAction("login", new { jwt = result });
        }

        // PUT: api/User/id
        [HttpPut("{id}")]
        // [Route("Update")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

