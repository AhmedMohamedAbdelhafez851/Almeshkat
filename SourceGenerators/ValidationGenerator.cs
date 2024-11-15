//using Microsoft.CodeAnalysis.Text;
//using Microsoft.CodeAnalysis;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SourceGenerators
//{
//    [Generator]
//    public class ValidationGenerator : ISourceGenerator
//    {
//        public void Initialize(GeneratorInitializationContext context) { }

//        public void Execute(GeneratorExecutionContext context)
//        {
//            var sourceBuilder = new StringBuilder();
//            sourceBuilder.AppendLine("using System.ComponentModel.DataAnnotations;");
//            sourceBuilder.AppendLine("namespace Domains.Dtos {");

//            sourceBuilder.AppendLine(@"
//            public partial class DepartmentDto
//            {
//                [Required(ErrorMessage = ErrorMessages.Required)]
//                [StringLength(100, MinimumLength = 3, ErrorMessage = ErrorMessages.StringLengthRange)]
//                public string DepartmentName { get; set; } = string.Empty;

//                [Required(ErrorMessage = ErrorMessages.Required)]
//                public int YearId { get; set; }
//            }
//        ");

//            sourceBuilder.AppendLine("}");
//            context.AddSource("ValidationGenerator", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
//        }
//    }
//}
