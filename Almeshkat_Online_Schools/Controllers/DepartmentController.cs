using Almeshkat_Online_Schools.Utilities;
using BL.Interfaces;
using Domains.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Almeshkat_Online_Schools.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<DepartmentDto>>>> GetAll()
        {
            var departments = await _departmentService.GetAllAsync();
            return Ok(ApiResponseFactory.Success(departments));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<DepartmentDto>>> GetById(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            return department == null
                ? NotFound(ApiResponseFactory.Error<DepartmentDto>(ApiMessages.NotFound))
                : Ok(ApiResponseFactory.Success(department));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] DepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var createdBy = User.GetUserId();
            await _departmentService.CreateAsync(departmentDto, createdBy);
            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(int id, [FromBody] DepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            departmentDto.DepartmentId = id; // Ensure ID consistency
            var updatedBy = User.GetUserId();
            var updateResult = await _departmentService.UpdateAsync(departmentDto, updatedBy);
            return updateResult
                ? Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Updated))
                : NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var deletedBy = User.GetUserId();
            var deleteResult = await _departmentService.DeleteAsync(id, deletedBy);
            return deleteResult
                ? Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Deleted))
                : NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));
        }
    }
}
