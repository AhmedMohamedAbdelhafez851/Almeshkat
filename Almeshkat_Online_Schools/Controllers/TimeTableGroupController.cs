using Almeshkat_Online_Schools.Utilities;
using BL.Interfaces;
using Domains.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace Almeshkat_Online_Schools.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class TimeTableGroupController : ControllerBase
    {
        private readonly ITimeTableGroupService _TimeTableGroupService;

        public TimeTableGroupController(ITimeTableGroupService TimeTableGroupService)
        {
            _TimeTableGroupService = TimeTableGroupService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TimeTableGroupDto>>>> GetAll()
        {
            var TimeTableGroups = await _TimeTableGroupService.GetAllAsync();
            return Ok(ApiResponseFactory.Success(TimeTableGroups));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<TimeTableGroupDto>>> GetById(int id)
        {
            var TimeTableGroup = await _TimeTableGroupService.GetByIdAsync(id);
            if (TimeTableGroup == null)
                return NotFound(ApiResponseFactory.Error<TimeTableGroupDto>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.Success(TimeTableGroup));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] TimeTableGroupDto TimeTableGroupDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var createdBy = User.GetUserId();
            await _TimeTableGroupService.CreateAsync(TimeTableGroupDto, createdBy);

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(int id, [FromBody] TimeTableGroupDto TimeTableGroupDto)
        {
            if (id != TimeTableGroupDto.Id)
                return BadRequest(ApiResponseFactory.Error<string>(ApiMessages.GetMismatchMessage("معرف القسم")));

            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var updatedBy = User.GetUserId();
            var result = await _TimeTableGroupService.UpdateAsync(TimeTableGroupDto, updatedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var deletedBy = User.GetUserId();
            var result = await _TimeTableGroupService.DeleteAsync(id, deletedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Deleted));
        }
    }
}
