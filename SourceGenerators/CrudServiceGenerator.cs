//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.Text;
//using System.Text;

//namespace SourceGenerators
//{
//    [Generator]
//    public class CrudServiceGenerator : ISourceGenerator
//    {
//        public void Initialize(GeneratorInitializationContext context) { }

//        public void Execute(GeneratorExecutionContext context)
//        {
//            var sourceBuilder = new StringBuilder();
//            sourceBuilder.AppendLine("using System.Collections.Generic;");
//            sourceBuilder.AppendLine("using System.Threading.Tasks;");
//            sourceBuilder.AppendLine("namespace BL.Services {");

//            // Dynamically create a CRUD service for the Department
//            sourceBuilder.AppendLine(@"
//                public partial class DepartmentService
//                {
//                    private readonly ApplicationDbContext _context;

//                    public DepartmentService(ApplicationDbContext context)
//                    {
//                        _context = context;
//                    }

//                    public async Task<DepartmentDto> GetByIdAsync(int id) => /* Generated code */;
//                    public async Task<IEnumerable<DepartmentDto>> GetAllAsync() => /* Generated code */;
//                    public async Task CreateAsync(DepartmentDto departmentDto, string createdBy) => /* Generated code */;
//                    public async Task<bool> UpdateAsync(DepartmentDto departmentDto, string updatedBy) => /* Generated code */;
//                    public async Task<bool> DeleteAsync(int id, string deletedBy) => /* Generated code */;
//                }
//            ");

//            sourceBuilder.AppendLine("}");
//            context.AddSource("CrudServiceGenerator", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
//        }
//    }
//}
