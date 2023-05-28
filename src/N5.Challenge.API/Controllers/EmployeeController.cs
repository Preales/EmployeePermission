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
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(
            EmployeeApplicationService employeApplicationService
            , ILogger<EmployeeController> logger)
        {
            _employeApplicationService = employeApplicationService;
            _logger = logger;
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


    }
}
