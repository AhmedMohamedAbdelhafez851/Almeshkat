using Almeshkat_Online_Schools.Utilities;
using BL.Services;
using Domains.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Almeshkat_Online_Schools.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TeacherSubjectController : ControllerBase
    {
        private readonly ITeacherSubjectService _teacherSubjectService;

        public TeacherSubjectController(ITeacherSubjectService teacherSubjectService)
        {
            _teacherSubjectService = teacherSubjectService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TeacherSubjectDto>>>> GetAll()
        {
            var teacherSubjects = await _teacherSubjectService.GetAllAsync();
            return Ok(ApiResponseFactory.Success(teacherSubjects));
        }

        [HttpGet("{teacherId:int}/{subjectId:int}")]
        public async Task<ActionResult<ApiResponse<TeacherSubjectDto>>> GetById(int teacherId, int subjectId)
        {
            var teacherSubject = await _teacherSubjectService.GetByIdAsync(teacherId, subjectId);
            if (teacherSubject == null)
                return NotFound(ApiResponseFactory.Error<TeacherSubjectDto>(ApiMessages.NotFound));

            return Ok(ApiResponseFactory.Success(teacherSubject));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] TeacherSubjectDto teacherSubjectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

            var createdBy = User.GetUserId();
            await _teacherSubjectService.CreateAsync(teacherSubjectDto, createdBy);

            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Created));
        }
        [HttpPut("{teacherId:int}/{subjectId:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(int teacherId, int subjectId, [FromBody] TeacherSubjectDto teacherSubjectDto)
        {
            try
            {
                // Check for mismatched teacherId and subjectId between URL and request body
                if (teacherId != teacherSubjectDto.TeacherId || subjectId != teacherSubjectDto.StaSubjId)
                {
                    return BadRequest(ApiResponseFactory.Error<string>("حدث خطأ في معرفات العلاقة بين المدرس والموضوع."));
                }

                // Check model state for validation errors
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponseFactory.ValidationError("بيانات الإدخال غير صالحة."));

                // Get the ID of the user making the update request
                var updatedBy = User.GetUserId();

                // Attempt to update the TeacherSubject record
                var result = await _teacherSubjectService.UpdateAsync(teacherSubjectDto, updatedBy);

                // If update fails (record not found), return a not-found response
                if (!result)
                    return NotFound(ApiResponseFactory.Error<string>("العنصر غير موجود."));

                // Return success message if update is successful
                return Ok(ApiResponseFactory.SuccessMessage("تم التحديث بنجاح."));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponseFactory.Error<string>("حدث خطأ في الخادم."));
            }
        }



        [HttpDelete("{teacherId:int}/{subjectId:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int teacherId, int subjectId)
        {
            try
            {
                var deletedBy = User.GetUserId();
                var result = await _teacherSubjectService.DeleteAsync(teacherId, subjectId, deletedBy);

                if (!result)
                    return NotFound(ApiResponseFactory.Error<string>("العنصر غير موجود."));

                return Ok(ApiResponseFactory.SuccessMessage("تم الحذف بنجاح."));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponseFactory.Error<string>("حدث خطأ في الخادم."));
            }
        }

    }
}
