using Almeshkat_Online_Schools.Utilities;
using BL.Services;
using Domains.Dtos;
using Microsoft.AspNetCore.Mvc;
namespace Almeshkat_Online_Years.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YearController : ControllerBase
    {
        private readonly IYearService _yearService;

        public YearController(IYearService yearService)
        {
            _yearService = yearService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<YearDto>>>> GetAll()
        {
            var years = await _yearService.GetAllAsync();
            return Ok(ApiResponseFactory.Success(years));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<YearDto>>> GetById(int id)
        {
            var year = await _yearService.GetByIdAsync(id);
            if (year == null)
                return NotFound(ApiResponseFactory.Error<YearDto>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.Success(year));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] YearDto yearDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var createdBy = User.GetUserId();
            await _yearService.CreateAsync(yearDto, createdBy);

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(int id, [FromBody] YearDto yearDto)
        {
            if (id != yearDto.YearId)
                return BadRequest(ApiResponseFactory.Error<string>(ApiMessages.GetMismatchMessage("معرف القسم")));

            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var updatedBy = User.GetUserId();
            var result = await _yearService.UpdateAsync(yearDto, updatedBy);

            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var deletedBy = User.GetUserId();
            var result = await _yearService.DeleteAsync(id, deletedBy);

            if (!result)
                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Deleted));
        }
    }
}
