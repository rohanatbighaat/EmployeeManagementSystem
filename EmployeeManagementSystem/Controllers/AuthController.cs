using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthRequest authRequest)
        {
            var response= await _authService.Authenticate(authRequest);
            if(response == null) {
                return BadRequest(new { message = "Authentication Failure" });
            }
            return Ok(response);
        }
    }
}
