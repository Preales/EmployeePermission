using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using N5.Challenge.API.ApplicationServices;
using N5.Challenge.API.Commands.Permission;
using N5.Challenge.Common.Kafka;
using N5.Challenge.Infrastructure.Settings;
using System.Text.Json;

namespace N5.Challenge.API.Controllers
{
    public class PermissionController : ControllerBase
    {
        private readonly PermissionApplicationService _permissionApplicationService;
        private readonly PersistenceSetting _persistenceSetting;
        private readonly IKafkaService _kafkaService;
        private readonly ILogger<PermissionController> _logger;

        public PermissionController(
            PermissionApplicationService permissionApplicationService
            , IOptions<PersistenceSetting> persistenceSetting
            , ILogger<PermissionController> logger
            , IKafkaService kafkaService)
        {
            _permissionApplicationService = permissionApplicationService;
            _persistenceSetting = persistenceSetting.Value;
            _logger = logger;
            _kafkaService = kafkaService;
        }

        [HttpPost("request")]
        public async Task<IActionResult> Post(CreatePermissionCommand command)
        {
            _logger.LogInformation("request : {@command}", command);
            try
            {
                await _permissionApplicationService.HandleCommandAsync(command);
                await SendMessageKafka("request", JsonSerializer.Serialize(command));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("modify")]
        public async Task<IActionResult> Put(SetPermissionCommand command)
        {
            _logger.LogInformation("modify : {@command}", command);
            try
            {
                await _permissionApplicationService.HandleCommandAsync(command);
                await SendMessageKafka("modify", JsonSerializer.Serialize(command));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            string query = @"SELECT 
                                per.Id,
                                per.EmployeeId, 
                                emp.Name     AS EmployeeName, 
                                emp.LastName AS EmployeeLastName,
                                per.PermissionTypeId,
                                typ.Name AS PermissionTypeName
                             FROM Permissions AS per
                             JOIN Employees AS emp ON emp.Id = per.EmployeeId
                             JOIN PermissionTypes AS typ ON typ.Id = per.PermissionTypeId";
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
            _logger.LogInformation("get : {@query}", query);
            await SendMessageKafka("get", query);
            return Ok(result);
        }

        private async Task SendMessageKafka(string operation, string @object)
        {
            var kafkaMessage = new KafkaMessageDto
            {
                Id = Guid.NewGuid(),
                OperationName = operation,
                Object = @object
            };
            await _kafkaService.SendMessage(kafkaMessage);
        }
    }
}
