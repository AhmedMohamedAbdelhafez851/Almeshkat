using Almeshkat_Online_Schools.Utilities;
using BL.Interfaces;
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
   // [Authorize(Roles ="Admin")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _StudentService;

        public StudentController(IStudentService StudentService)
        {
            _StudentService = StudentService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<StudentDto>>>> GetAll()
        {
            var Students = await _StudentService.GetAllAsync();
            return Ok(ApiResponseFactory.Success(Students));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<StudentDto>>> GetById(int id)
        {
            var Student = await _StudentService.GetByIdAsync(id);
            if (Student == null)
                return NotFound(ApiResponseFactory.Error<StudentDto>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.Success(Student));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] StudentDto StudentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var createdBy = User.GetUserId();
            await _StudentService.CreateAsync(StudentDto, createdBy);

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(int id, [FromBody] StudentDto StudentDto)
        {
            if (id != StudentDto.StudentId)
                return BadRequest(ApiResponseFactory.Error<string>(ApiMessages.GetMismatchMessage("معرف القسم")));

            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var updatedBy = User.GetUserId();
            var result = await _StudentService.UpdateAsync(StudentDto, updatedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var deletedBy = User.GetUserId();
            var result = await _StudentService.DeleteAsync(id, deletedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Deleted));
        }
    }
}
