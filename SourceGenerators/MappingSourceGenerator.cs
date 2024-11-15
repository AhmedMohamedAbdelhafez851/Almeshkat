//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Almeshkat.SourceGenerators
//{
//    [Generator]
//    public class MappingSourceGenerator : ISourceGenerator
//    {
//        public void Initialize(GeneratorInitializationContext context)
//        {
//            context.RegisterForSyntaxNotifications(() => new ClassSyntaxReceiver());
//        }

//        public void Execute(GeneratorExecutionContext context)
//        {
//            if (context.SyntaxContextReceiver is not ClassSyntaxReceiver receiver) return;

//            foreach (var entityClass in receiver.EntityClasses)
//            {
//                var namespaceName = GetNamespace(entityClass);
//                var entityName = entityClass.Identifier.Text;
//                var dtoName = $"{entityName}Dto";

//                var sourceCode = GenerateMappingClass(namespaceName, entityName, dtoName);
//                context.AddSource($"{entityName}Mapping.g.cs", sourceCode);
//            }
//        }

//        private string GenerateMappingClass(string namespaceName, string entityName, string dtoName)
//        {
//            var sb = new StringBuilder();

//            sb.AppendLine($"namespace {namespaceName}");
//            sb.AppendLine("{");
//            sb.AppendLine($"    public static class {entityName}MappingExtensions");
//            sb.AppendLine("    {");

//            // ToDto method
//            sb.AppendLine($"        public static {dtoName} ToDto(this {entityName} entity) => new {dtoName}");
//            sb.AppendLine("        {");
//            sb.AppendLine($"            DepartmentId = entity.DepartmentId,");
//            sb.AppendLine($"            DepartmentName = entity.DepartmentName,");
//            sb.AppendLine($"            YearId = entity.YearId,");
//            sb.AppendLine($"            YearName = entity.Year?.YearName ?? string.Empty");
//            sb.AppendLine("        };");

//            // ToEntity method
//            sb.AppendLine($"        public static {entityName} ToEntity(this {dtoName} dto) => new {entityName}");
//            sb.AppendLine("        {");
//            sb.AppendLine($"            DepartmentId = dto.DepartmentId,");
//            sb.AppendLine($"            DepartmentName = dto.DepartmentName,");
//            sb.AppendLine($"            YearId = dto.YearId");
//            sb.AppendLine("        };");

//            // UpdateEntity method
//            sb.AppendLine($"        public static void UpdateEntity(this {dtoName} dto, {entityName} entity)");
//            sb.AppendLine("        {");
//            sb.AppendLine($"            entity.DepartmentName = dto.DepartmentName;");
//            sb.AppendLine($"            entity.YearId = dto.YearId;");
//            sb.AppendLine("        }");

//            sb.AppendLine("    }");
//            sb.AppendLine("}");

//            return sb.ToString();
//        }

//        private string GetNamespace(ClassDeclarationSyntax classSyntax)
//        {
//            SyntaxNode? potentialNamespaceParent = classSyntax.Parent;
//            while (potentialNamespaceParent != null && potentialNamespaceParent is not NamespaceDeclarationSyntax)
//            {
//                potentialNamespaceParent = potentialNamespaceParent.Parent;
//            }
//            return (potentialNamespaceParent as NamespaceDeclarationSyntax)?.Name.ToString() ?? "UnknownNamespace";
//        }

//        private class ClassSyntaxReceiver : ISyntaxContextReceiver
//        {
//            public List<ClassDeclarationSyntax> EntityClasses { get; } = new();

//            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
//            {
//                if (context.Node is ClassDeclarationSyntax classDeclaration &&
//                    classDeclaration.Identifier.Text.EndsWith("Department")) // Adjust if needed for other entities
//                {
//                    EntityClasses.Add(classDeclaration);
//                }
//            }
//        }
//    }
//}
