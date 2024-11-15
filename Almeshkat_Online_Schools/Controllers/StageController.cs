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
    //[Authorize]
    public class StageController : ControllerBase
    {
        private readonly IStageService _StageService;

        public StageController(IStageService StageService)
        {
            _StageService = StageService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<StageDto>>>> GetAll()
        {
            var Stages = await _StageService.GetAllAsync();
            return Ok(ApiResponseFactory.Success(Stages));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<StageDto>>> GetById(int id)
        {
            var Stage = await _StageService.GetByIdAsync(id);
            if (Stage == null)
                return NotFound(ApiResponseFactory.Error<StageDto>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.Success(Stage));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] StageDto StageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var createdBy = User.GetUserId();
            await _StageService.CreateAsync(StageDto, createdBy);

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(int id, [FromBody] StageDto StageDto)
        {
            if (id != StageDto.StageId)
                return BadRequest(ApiResponseFactory.Error<string>(ApiMessages.GetMismatchMessage("معرف القسم")));

            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var updatedBy = User.GetUserId();
            var result = await _StageService.UpdateAsync(StageDto, updatedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var deletedBy = User.GetUserId();
            var result = await _StageService.DeleteAsync(id, deletedBy);
            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Deleted));
        }
    }
}
