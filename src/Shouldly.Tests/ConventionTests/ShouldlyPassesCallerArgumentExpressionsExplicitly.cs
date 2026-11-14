using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Operations;

namespace Shouldly.Tests.ConventionTests;

/// <summary>
/// Inside Shouldly, a call to a Shouldly method with a <see cref="CallerArgumentExpressionAttribute"/> parameter
/// passes that argument explicitly. Left out, the compiler fills it with the source text of Shouldly's own argument
/// (<c>actual</c>, <c>action</c>), and the failure names Shouldly's parameter instead of the user's code. The
/// trip-wire can't see this, because the expression isn't missing, just wrong; nor can the compiled IL, where both are
/// a string constant. So this reads Shouldly's source with Roslyn, once with each target's preprocessor symbols.
/// </summary>
public class ShouldlyPassesCallerArgumentExpressionsExplicitly
{
    public static TheoryData<string> Targets => ["netstandard2.0", "net8.0", "net9.0"];

    [Theory]
    [MemberData(nameof(Targets))]
    public void Every_caller_argument_expression_is_passed_explicitly(string target)
    {
        var compilation = CompileShouldly(target);
        compilation.GetDiagnostics(TestContext.Current.CancellationToken)
            .Where(d => d.Severity == DiagnosticSeverity.Error)
            .Select(d => d.ToString())
            .ShouldBeEmpty("Shouldly's source should compile here, or the check below would miss calls it can't bind");

        var passed = 0;
        var leftToTheCompiler = new List<string>();
        foreach (var argument in CallerArgumentExpressionArguments(compilation))
        {
            if (argument.ArgumentKind == ArgumentKind.Explicit)
            {
                passed++;
                continue;
            }

            var call = argument.Parent!.Syntax;
            var line = call.GetLocation().GetLineSpan();
            var file = Path.GetRelativePath(ShouldlySource(), line.Path).Replace('\\', '/');
            var method = argument.Parameter!.ContainingSymbol.ToDisplayString(SymbolDisplayFormat.CSharpShortErrorMessageFormat);
            leftToTheCompiler.Add($"{file}:{line.StartLinePosition.Line + 1} leaves {argument.Parameter.Name} of {method} to the compiler");
        }

        passed.ShouldBeGreaterThan(0, "the check should see Shouldly passing expressions on, or it isn't looking");
        leftToTheCompiler.ShouldBeEmpty();
    }

    [Fact]
    public void Finds_an_expression_left_to_the_compiler()
    {
        var compilation = CSharpCompilation.Create("Sample",
            [
                CSharpSyntaxTree.ParseText("""
                    using System.Runtime.CompilerServices;

                    static class Assertions
                    {
                        public static void ShouldBeNice(this int actual, [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) { }
                        public static void ShouldBeNicer(this int actual, [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
                            actual.ShouldBeNice();
                        public static void ShouldBeNicest(this int actual, [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
                            actual.ShouldBeNice(actualExpression);
                    }
                    """, new CSharpParseOptions(LanguageVersion.Latest), cancellationToken: TestContext.Current.CancellationToken),
            ],
            FrameworkReferences(),
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, nullableContextOptions: NullableContextOptions.Enable));

        CallerArgumentExpressionArguments(compilation)
            .Select(a => $"{a.ArgumentKind} at line {a.Parent!.Syntax.GetLocation().GetLineSpan().StartLinePosition.Line + 1}")
            .ShouldBe(["DefaultValue at line 7", "Explicit at line 9"]);
    }

    /// <summary>
    /// The arguments given to <see cref="CallerArgumentExpressionAttribute"/> parameters of methods and constructors
    /// declared in the compilation itself. Calls into the BCL (such as <c>Debug.Assert</c>, whose message is filled in
    /// from the condition) are left alone.
    /// </summary>
    static IEnumerable<IArgumentOperation> CallerArgumentExpressionArguments(Compilation compilation)
    {
        foreach (var tree in compilation.SyntaxTrees)
        {
            var model = compilation.GetSemanticModel(tree);
            foreach (var node in tree.GetRoot().DescendantNodes())
            {
                var arguments = model.GetOperation(node) switch
                {
                    IInvocationOperation invocation => invocation.Arguments,
                    IObjectCreationOperation creation => creation.Arguments,
                    _ => [],
                };

                foreach (var argument in arguments)
                {
                    if (argument.Parameter is { } parameter &&
                        SymbolEqualityComparer.Default.Equals(parameter.ContainingAssembly, compilation.Assembly) &&
                        parameter.GetAttributes().Any(a => a.AttributeClass?.Name == nameof(CallerArgumentExpressionAttribute)))
                        yield return argument;
                }
            }
        }
    }

    static CSharpCompilation CompileShouldly(string target)
    {
        var options = new CSharpParseOptions(LanguageVersion.CSharp14, preprocessorSymbols: PreprocessorSymbols(target));
        var trees = Directory.EnumerateFiles(ShouldlySource(), "*.cs", SearchOption.AllDirectories)
            .Where(path => !IsBuildOutput(Path.GetRelativePath(ShouldlySource(), path)))
            .Select(path => CSharpSyntaxTree.ParseText(File.ReadAllText(path), options, path))
            .Append(CSharpSyntaxTree.ParseText(ImplicitUsings, options, "ImplicitUsings.cs"));

        // The BCL of the running framework stands in for each target's. Shouldly compiles against it cleanly with
        // every target's symbols, and the assertion on compile errors above says so if that stops being true.
        return CSharpCompilation.Create("Shouldly", trees, FrameworkReferences(),
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, nullableContextOptions: NullableContextOptions.Enable));
    }

    // The running framework's own assemblies, leaving out the test's dependencies, Shouldly.dll among them.
    static IEnumerable<MetadataReference> FrameworkReferences()
    {
        var frameworkDirectory = Path.GetDirectoryName(typeof(object).Assembly.Location);
        return ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!)
            .Split(Path.PathSeparator)
            .Where(path => string.Equals(Path.GetDirectoryName(path), frameworkDirectory, StringComparison.OrdinalIgnoreCase))
            .Select(path => MetadataReference.CreateFromFile(path));
    }

    // The symbols the SDK defines for each of Shouldly's targets.
    static string[] PreprocessorSymbols(string target) => target switch
    {
        "netstandard2.0" => ["NETSTANDARD", "NETSTANDARD2_0", .. OrGreater("NETSTANDARD", "1_0", "1_1", "1_2", "1_3", "1_4", "1_5", "1_6", "2_0")],
        "net8.0" => ["NET", "NET8_0", "NETCOREAPP", .. OrGreater("NET", "5_0", "6_0", "7_0", "8_0"), .. OrGreater("NETCOREAPP", "1_0", "1_1", "2_0", "2_1", "2_2", "3_0", "3_1")],
        "net9.0" => ["NET", "NET9_0", "NETCOREAPP", .. OrGreater("NET", "5_0", "6_0", "7_0", "8_0", "9_0"), .. OrGreater("NETCOREAPP", "1_0", "1_1", "2_0", "2_1", "2_2", "3_0", "3_1")],
        _ => throw new ArgumentOutOfRangeException(nameof(target), target, null),
    };

    static IEnumerable<string> OrGreater(string prefix, params string[] versions) =>
        versions.Select(version => $"{prefix}{version}_OR_GREATER");

    // What <ImplicitUsings>enable</ImplicitUsings> adds to Shouldly.
    const string ImplicitUsings = """
        global using System;
        global using System.Collections.Generic;
        global using System.IO;
        global using System.Linq;
        global using System.Net.Http;
        global using System.Threading;
        global using System.Threading.Tasks;
        """;

    static bool IsBuildOutput(string path) =>
        path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar).Any(segment => segment is "obj" or "bin");

    static string ShouldlySource([CallerFilePath] string thisFile = "") =>
        Path.GetFullPath(Path.Combine(Path.GetDirectoryName(thisFile)!, "..", "..", "Shouldly"));
}
