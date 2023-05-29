using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using N5.Challenge.API.ApplicationServices;
using N5.Challenge.API.Queries;
using N5.Challenge.Infrastructure.Settings;

namespace N5.Challenge.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueriesController : ControllerBase
    {
        private readonly EmployeeApplicationService _employeApplicationService;
        private readonly PersistenceSetting _persistenceSetting;

        public QueriesController(
            EmployeeApplicationService employeApplicationService
            , IOptions<PersistenceSetting> persistenceSetting)
        {
            _employeApplicationService = employeApplicationService;
            _persistenceSetting = persistenceSetting.Value;
        }

        [HttpGet("GetAllPermissionTypes")]
        public async Task<IActionResult> GetAllPermissionTypes()
        {
            string query = @"SELECT Id, Name FROM PermissionTypes";
            List<dynamic> result;
            if (_persistenceSetting.UseMsSql)
            {
                using var connection = new SqlConnection(_persistenceSetting.ConnectionString);
                result = (await connection.QueryAsync(query)).ToList();
            }
            else
            {
                using var connection = new SqliteConnection(_persistenceSetting.ConnectionString);
                result = (await connection.QueryAsync(query)).ToList();
            }
            return Ok(result);
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            string query = @"SELECT Id, Name, LastName FROM Employees";
            List<dynamic> result;
            if (_persistenceSetting.UseMsSql)
            {
                using var connection = new SqlConnection(_persistenceSetting.ConnectionString);
                result = (await connection.QueryAsync(query)).ToList();
            }
            else
            {
                using var connection = new SqliteConnection(_persistenceSetting.ConnectionString);
                result = (await connection.QueryAsync(query)).ToList();
            }
            return Ok(result);
            //return Ok(await _employeApplicationService.HandleQueryAsync(new GetEmployeesQuery()));
        }

        [HttpGet("GetEmployeeById/{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetEmployeeByIdQuery(id);
            return Ok(await _employeApplicationService.HandleQueryAsync(query));
        }
    }
}