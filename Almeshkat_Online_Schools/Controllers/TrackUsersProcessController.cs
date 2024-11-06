using BL;
using Domains.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Almeshkat_Online_Schools.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackUsersProcessController : ControllerBase
    {
        private readonly TrackUsersProcessService _processService;

        public TrackUsersProcessController(TrackUsersProcessService processService)
        {
            _processService = processService;
        }

        // GET: api/TrackUsersProcess
        [HttpGet]
        public async Task<IActionResult> GetAllProcesses()
        {
            var processes = await _processService.GetAllProcessesAsync();
            return Ok(processes);
        }

        // POST: api/TrackUsersProcess
        [HttpPost]
        public async Task<IActionResult> LogProcess([FromBody] TrackUsersProcess process)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _processService.LogUserProcessAsync(
                process.UserId,
                process.ProcessName,
                process.TableName,
                process.RowNumber,
                process.DeviceIp
            );

            return Ok("Process logged successfully.");
        }

        // GET: api/TrackUsersProcess/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProcessById(int id)
        {
            var process = await _processService.GetProcessByIdAsync(id);
            if (process == null)
                return NotFound("Process not found.");

            return Ok(process);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcess(int id)
        {
            var result = await _processService.DeleteProcessByIdAsync(id);
            if (!result)
            {
                return NotFound(); // Process not found
            }

            return NoContent(); // Successfully deleted
        }

    }
}
