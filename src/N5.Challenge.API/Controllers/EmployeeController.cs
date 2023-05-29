using Microsoft.AspNetCore.Mvc;
using N5.Challenge.API.ApplicationServices;
using N5.Challenge.API.Commands.Employee;

namespace N5.Challenge.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeApplicationService _employeApplicationService;

        public EmployeeController(
            EmployeeApplicationService employeApplicationService
        )
        {
            _employeApplicationService = employeApplicationService;

        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeCommand command)
        {
            try
            {
                await _employeApplicationService.HandleCommandAsync(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(SetEmployeeCommand command)
        {
            try
            {
                await _employeApplicationService.HandleCommandAsync(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("WithPermissionType")]
        public async Task<IActionResult> Post(CreateEmployeeWithPermission command)
        {
            try
            {
                await _employeApplicationService.HandleCommandAsync(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
