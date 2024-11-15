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
    //    [Authorize]
    public class TermController : ControllerBase
    {
        private readonly ITermService _TermService;

        public TermController(ITermService TermService)
        {
            _TermService = TermService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TermDto>>>> GetAll()
        {
            var Terms = await _TermService.GetAllAsync();
            return Ok(ApiResponseFactory.Success(Terms));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<TermDto>>> GetById(int id)
        {
            var Term = await _TermService.GetByIdAsync(id);
            if (Term == null)
                return NotFound(ApiResponseFactory.Error<TermDto>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.Success(Term));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] TermDto TermDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var createdBy = User.GetUserId();
            await _TermService.CreateAsync(TermDto, createdBy);

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(int id, [FromBody] TermDto TermDto)
        {
            if (id != TermDto.TermId)
                return BadRequest(ApiResponseFactory.Error<string>(ApiMessages.GetMismatchMessage("معرف القسم")));

            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var updatedBy = User.GetUserId();
            var result = await _TermService.UpdateAsync(TermDto, updatedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var deletedBy = User.GetUserId();
            var result = await _TermService.DeleteAsync(id, deletedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Deleted));
        }
    }
}
