using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityComponentSystem
{
    public class SyntaxReceiver : ISyntaxReceiver
    {
        public HashSet<TypeDeclarationSyntax> TypeDeclarationsWithAttributes { get; } = new HashSet<TypeDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is TypeDeclarationSyntax declaration
                && declaration.AttributeLists.Any())
            {
                TypeDeclarationsWithAttributes.Add(declaration);
            }
        }
    }


    [Generator]
    public class ComponentGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var compilation = context.Compilation;

            var syntaxReceiver = (SyntaxReceiver)context.SyntaxReceiver;
            var targets = syntaxReceiver.TypeDeclarationsWithAttributes;

            var componentAttribute = compilation.GetTypeByMetadataName(typeof(ComponentAttribute).FullName);

#pragma warning disable RS1024 // https://github.com/dotnet/roslyn-analyzers/issues/4568
            var targetComponents = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
#pragma warning restore RS1024

            foreach (var targetTypeSyntax in targets)
            {
                context.CancellationToken.ThrowIfCancellationRequested();

                var semanticModel = compilation.GetSemanticModel(targetTypeSyntax.SyntaxTree);
                var targetType = semanticModel.GetDeclaredSymbol(targetTypeSyntax);
                var hasComponentAttribute = targetType.GetAttributes()
                  .Any(x => x.AttributeClass.Equals(componentAttribute, SymbolEqualityComparer.Default));
                if (!hasComponentAttribute)
                    continue;

                targetComponents.Add(targetType);
            }

            foreach (var targetType in targetComponents)
            {
                context.CancellationToken.ThrowIfCancellationRequested();

                var componentSource = GenerateEcsExtensions(targetType);
                context.AddSource($"{targetType.Name}Extensions.Ecs.cs", componentSource);
            }

            context.AddSource($"EcsExtensions.Ecs.cs", GenerateRegisterBlock(targetComponents));
        }

        public void Initialize(GeneratorInitializationContext context)
          => context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());

        private string GenerateRegisterBlock(HashSet<ITypeSymbol> targetTypes)
        {
            var stringBuilder = new StringBuilder();

            // Start class
            stringBuilder.Append($@"
using System;
using EntityComponentSystem;

namespace EcsExtensions
{{
    public static class EcsExtensions
    {{
");

            // Declare members
            foreach (var targetType in targetTypes)
            {
                var componentName = targetType.Name.Replace("Component", "").Replace("omponent", "");
                var upperComponentName = Char.ToUpperInvariant(componentName[0]) + componentName.Substring(1);

                stringBuilder.Append($@"
        public static int {upperComponentName}Id;
");
            }

            // Declare register method
            // Start method
            stringBuilder.Append($@"
        public static void Register(Ecs ecs)
        {{
");

            // Declare members
            foreach (var targetType in targetTypes)
            {
                var componentName = targetType.Name.Replace("Component", "").Replace("omponent", "");
                var upperComponentName = Char.ToUpperInvariant(componentName[0]) + componentName.Substring(1);

                stringBuilder.Append($@"
            {upperComponentName}Id = ecs.RegisterComponentKind(""{upperComponentName}"");
");
            }
            // End method
            stringBuilder.Append($@"
        }}
");


            // End class
            stringBuilder.Append($@"
    }}
}}
");
            return stringBuilder.ToString();
        }

        private string GenerateEcsExtensions(ITypeSymbol targetType)
        {
            var componentName = targetType.Name.Replace("Component", "").Replace("omponent", "");
            var lowerComponentName = Char.ToLowerInvariant(componentName[0]) + componentName.Substring(1);
            var upperComponentName = Char.ToUpperInvariant(componentName[0]) + componentName.Substring(1);

            return $@"
using System;
using EntityComponentSystem;
using {targetType.ContainingNamespace};

namespace EcsExtensions
{{
    public static class {upperComponentName}Extensions
    {{
        public static {componentName} Get{upperComponentName}(this Ecs ecs, int entityId)
        {{
            return ecs.GetComponent(EcsExtensions.{upperComponentName}Id, entityId) as {componentName};
        }}
        public static bool Has{upperComponentName}(this Ecs ecs, int entityId)
        {{
            return ecs.HasComponent(EcsExtensions.{upperComponentName}Id, entityId);
        }}
        public static void Set{upperComponentName}(this Ecs ecs, int entityId, {componentName} {lowerComponentName})
        {{
            ecs.SetComponent(EcsExtensions.{upperComponentName}Id, entityId, {lowerComponentName});
        }}
        public static void Delete{upperComponentName}(this Ecs ecs, int entityId)
        {{
            ecs.DeleteComponent(EcsExtensions.{upperComponentName}Id, entityId);
        }}
    }}
}}
";
        }
    }
}