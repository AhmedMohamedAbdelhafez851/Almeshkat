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
    // [Authorize] // Uncomment this if authorization is required
    public class StudentsStudyInfController : ControllerBase
    {
        private readonly IStudentsStudyInfService _studentsStudyInfService;

        public StudentsStudyInfController(IStudentsStudyInfService studentsStudyInfService)
        {
            _studentsStudyInfService = studentsStudyInfService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<StudentStudyInfDto>>>> GetAll()
        {
            var studentsStudyInfs = await _studentsStudyInfService.GetAllAsync();
            return Ok(ApiResponseFactory.Success(studentsStudyInfs));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<StudentStudyInfDto>>> GetById(int id)
        {
            var studentsStudyInf = await _studentsStudyInfService.GetByIdAsync(id);
            if (studentsStudyInf == null)
                return NotFound(ApiResponseFactory.Error<StudentStudyInfDto>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.Success(studentsStudyInf));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] StudentStudyInfDto studentsStudyInfDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var createdBy = User.GetUserId();
            await _studentsStudyInfService.CreateAsync(studentsStudyInfDto, createdBy);

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(int id, [FromBody] StudentStudyInfDto studentsStudyInfDto)
        {
            if (id != studentsStudyInfDto.Id)
                return BadRequest(ApiResponseFactory.Error<string>(ApiMessages.GetMismatchMessage("معرف القسم")));

            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var updatedBy = User.GetUserId();
            var result = await _studentsStudyInfService.UpdateAsync(studentsStudyInfDto, updatedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var deletedBy = User.GetUserId();
            var result = await _studentsStudyInfService.DeleteAsync(id, deletedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Deleted));
        }
    }

}
