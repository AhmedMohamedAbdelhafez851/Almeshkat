using Almeshkat_Online_Schools.Utilities;
using BL.Interfaces;
using Domains.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace Almeshkat_Online_Schools.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService SubjectService)
        {
            _subjectService = SubjectService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<SubjectDto>>>> GetAll()
        {
            var Subjects = await _subjectService.GetAllAsync();
            return Ok(ApiResponseFactory.Success(Subjects));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<SubjectDto>>> GetById(int id)
        {
            var Subject = await _subjectService.GetByIdAsync(id);
            if (Subject == null)
                return NotFound(ApiResponseFactory.Error<SubjectDto>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.Success(Subject));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] SubjectDto SubjectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var createdBy = User.GetUserId();
            await _subjectService.CreateAsync(SubjectDto, createdBy);

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(int id, [FromBody] SubjectDto subjectDto)
        {
            if (id != subjectDto.SubjectId)
                return BadRequest(ApiResponseFactory.Error<string>(ApiMessages.GetMismatchMessage("معرف القسم")));

            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var updatedBy = User.GetUserId();
            var result = await _subjectService.UpdateAsync(subjectDto, updatedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var deletedBy = User.GetUserId();
            var result = await _subjectService.DeleteAsync(id, deletedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Deleted));
        }
    }
}
