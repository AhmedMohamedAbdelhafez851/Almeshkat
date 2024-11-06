using Almeshkat_Online_Schools.Utilities;
using BL.Services;
using Domains.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Almeshkat_Online_Schools.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _TeacherService;

        public TeacherController(ITeacherService TeacherService)
        {
            _TeacherService = TeacherService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TeacherDto>>>> GetAll()
        {
            var Teachers = await _TeacherService.GetAllAsync();
            return Ok(ApiResponseFactory.Success(Teachers));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<TeacherDto>>> GetById(int id)
        {
            var Teacher = await _TeacherService.GetByIdAsync(id);
            if (Teacher == null)
                return NotFound(ApiResponseFactory.Error<TeacherDto>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.Success(Teacher));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] TeacherDto TeacherDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var createdBy = User.GetUserId();
            await _TeacherService.CreateAsync(TeacherDto, createdBy);

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(int id, [FromBody] TeacherDto TeacherDto)
        {
            if (id != TeacherDto.TeacherId)
                return BadRequest(ApiResponseFactory.Error<string>(ApiMessages.GetMismatchMessage("معرف القسم")));

            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var updatedBy = User.GetUserId();
            var result = await _TeacherService.UpdateAsync(TeacherDto, updatedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var deletedBy = User.GetUserId();
            var result = await _TeacherService.DeleteAsync(id, deletedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Deleted));
        }
    }
}
