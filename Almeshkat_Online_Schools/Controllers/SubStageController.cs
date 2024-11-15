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
    public class SubStageController : ControllerBase
    {
        private readonly ISubStageService _SubStageService;

        public SubStageController(ISubStageService SubStageService)
        {
            _SubStageService = SubStageService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<SubStageDto>>>> GetAll()
        {
            var SubStages = await _SubStageService.GetAllAsync();
            return Ok(ApiResponseFactory.Success(SubStages));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<SubStageDto>>> GetById(int id)
        {
            var SubStage = await _SubStageService.GetByIdAsync(id);
            if (SubStage == null)
                return NotFound(ApiResponseFactory.Error<SubStageDto>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.Success(SubStage));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] SubStageDto SubStageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var createdBy = User.GetUserId();
            await _SubStageService.CreateAsync(SubStageDto, createdBy);

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(int id, [FromBody] SubStageDto SubStageDto)
        {
            if (id != SubStageDto.SubStageId)
                return BadRequest(ApiResponseFactory.Error<string>(ApiMessages.GetMismatchMessage("معرف القسم")));

            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var updatedBy = User.GetUserId();
            var result = await _SubStageService.UpdateAsync(SubStageDto, updatedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var deletedBy = User.GetUserId();
            var result = await _SubStageService.DeleteAsync(id, deletedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Deleted));
        }
    }
}
