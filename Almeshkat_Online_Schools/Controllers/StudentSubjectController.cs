//using Almeshkat_Online_Schools.Utilities;
//using BL.Services;
//using Domains.Dtos;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace Almeshkat_Online_Schools.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StudentSubjectController : ControllerBase
//    {
//        private readonly IStudentSubjectService _studentSubjectService;

//        public StudentSubjectController(IStudentSubjectService studentSubjectService)
//        {
//            _studentSubjectService = studentSubjectService;
//        }

//        [HttpGet]
//        public async Task<ActionResult<ApiResponse<IEnumerable<StudentSubjectDto>>>> GetAll()
//        {
//            var studentSubjects = await _studentSubjectService.GetAllAsync();
//            return Ok(ApiResponseFactory.Success(studentSubjects));
//        }

//        [HttpGet("{studentId:int}/{subjectId:int}")]
//        public async Task<ActionResult<ApiResponse<StudentSubjectDto>>> GetById(int studentId, int subjectId)
//        {
//            var studentSubject = await _studentSubjectService.GetByIdAsync(studentId, subjectId);
//            if (studentSubject == null)
//                return NotFound(ApiResponseFactory.Error<StudentSubjectDto>(ApiMessages.NotFound));

//            return Ok(ApiResponseFactory.Success(studentSubject));
//        }

//        [HttpPost]
//        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] StudentSubjectDto studentSubjectDto)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ApiResponseFactory.ValidationError(ApiMessages.ValidationError));

//            var createdBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            await _studentSubjectService.CreateAsync(studentSubjectDto, createdBy);

//            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Created));
//        }

//        [HttpDelete("{studentId:int}/{subjectId:int}")]
//        public async Task<ActionResult<ApiResponse<string>>> Delete(int studentId, int subjectId)
//        {
//            var deletedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            var result = await _studentSubjectService.DeleteAsync(studentId, subjectId, deletedBy);
//            if (!result)
//                return NotFound(ApiResponseFactory.Error<string>(ApiMessages.NotFound));

//            return Ok(ApiResponseFactory.SuccessMessage(ApiMessages.Deleted));
//        }
//    }

//}
