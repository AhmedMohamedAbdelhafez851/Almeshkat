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
    // [Authorize]
    public class StageSubjectController : ControllerBase
    {
        private readonly IStageSubjectService _StageSubjectService;

        public StageSubjectController(IStageSubjectService StageSubjectService)
        {
            _StageSubjectService = StageSubjectService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<StageSubjectsDto>>>> GetAll()
        {
            var StageSubjects = await _StageSubjectService.GetAllAsync();
            return Ok(ApiResponseFactory.Success(StageSubjects));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<StageSubjectsDto>>> GetById(int id)
        {
            var StageSubject = await _StageSubjectService.GetByIdAsync(id);
            if (StageSubject == null)
                return NotFound(ApiResponseFactory.Error<StageSubjectsDto>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.Success(StageSubject));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] StageSubjectsDto StageSubjectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var createdBy = User.GetUserId();
            await _StageSubjectService.CreateAsync(StageSubjectDto, createdBy);

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(int id, [FromBody] StageSubjectsDto StageSubjectDto)
        {
            if (id != StageSubjectDto.StageSubjectId)
                return BadRequest(ApiResponseFactory.Error<string>(ApiMessages.GetMismatchMessage("معرف القسم")));

            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var updatedBy = User.GetUserId();
            var result = await _StageSubjectService.UpdateAsync(StageSubjectDto, updatedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var deletedBy = User.GetUserId();
            var result = await _StageSubjectService.DeleteAsync(id, deletedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Deleted));
        }
    }
}
