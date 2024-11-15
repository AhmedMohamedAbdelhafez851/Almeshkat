using Almeshkat_Online_Schools.Utilities;
using BL.Interfaces;
using Domains.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Almeshkat_Online_Schools.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // Ensure that only authenticated users can access these methods
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _SchoolService;

        public SchoolController(ISchoolService SchoolService)
        {
            _SchoolService = SchoolService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<SchoolDto>>>> GetAll()
        {
            var Schools = await _SchoolService.GetAllAsync();
            return Ok(ApiResponseFactory.Success(Schools));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<SchoolDto>>> GetById(int id)
        {
            var School = await _SchoolService.GetByIdAsync(id);
            if (School == null)
                return NotFound(ApiResponseFactory.Error<SchoolDto>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.Success(School));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] SchoolDto SchoolDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var createdBy = User.GetUserId();
            await _SchoolService.CreateAsync(SchoolDto , createdBy);

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(int id, [FromBody] SchoolDto SchoolDto)
        {
            if (id != SchoolDto.SchoolId)
                return BadRequest(ApiResponseFactory.Error<string>(ApiMessages.GetMismatchMessage("معرف القسم")));

            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var updatedBy = User.GetUserId();
            var result = await _SchoolService.UpdateAsync(SchoolDto , updatedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var deletedBy = User.GetUserId();
            var result = await _SchoolService.DeleteAsync(id , deletedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Deleted));
        }
    }
}
