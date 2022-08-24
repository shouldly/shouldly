using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Shouldly.Configuration;
using Xunit.Abstractions;

namespace DocumentationExamples;

public static class DocExampleWriter
{
    private static readonly Regex scrubberRegex = new(@"\w:.+?shouldly\\src", RegexOptions.Compiled);
    private static readonly Func<string, string> scrubber = v => scrubberRegex.Replace(v, "C:\\PathToCode\\shouldly\\src");

    private static readonly ConcurrentDictionary<string, List<MethodDeclarationSyntax>> FileMethodsLookup = new();

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Document(Action shouldMethod, ITestOutputHelper testOutputHelper, Action<ShouldMatchConfigurationBuilder>? additionConfig = null)
    {
        var stackTrace = new StackTrace(true);
        var caller = stackTrace.GetFrame(1)!;
        var callerFileName = caller.GetFileName()!;
        var callerMethod = caller.GetMethod()!;

        var testMethod = FileMethodsLookup.GetOrAdd(callerFileName, fn =>
        {
            var callerFile = File.ReadAllText(fn);
            var syntaxTree = CSharpSyntaxTree.ParseText(callerFile);
            return syntaxTree.GetRoot()
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>().ToList();
        }).Single(m => m.Identifier.ValueText == callerMethod.Name);

        var documentCall = testMethod.DescendantNodes()
            .OfType<InvocationExpressionSyntax>()
            .First();

        var shouldMethodCallSyntax = documentCall.ArgumentList.Arguments[0];
        var blockSyntax = shouldMethodCallSyntax.DescendantNodes()
            .OfType<BlockSyntax>()
            .First();
        var enumerable = blockSyntax
            .Statements
            .Select(s => s.WithoutLeadingTrivia().ToFullString());
        var body = string.Join(string.Empty, enumerable).Trim();
        var exceptionText = Should.Throw<Exception>(shouldMethod).Message;

        testOutputHelper.WriteLine("Docs body:");
        testOutputHelper.WriteLine("");
        testOutputHelper.WriteLine(body);
        testOutputHelper.WriteLine("");
        testOutputHelper.WriteLine("");
        testOutputHelper.WriteLine("Exception text:");
        testOutputHelper.WriteLine("");
        testOutputHelper.WriteLine(exceptionText);

        try
        {
            body.ShouldMatchApproved(configurationBuilder =>
            {
                configurationBuilder
                    .WithDiscriminator("codeSample")
                    .UseCallerLocation()
                    .SubFolder("CodeExamples")
                    .WithScrubber(scrubber).WithFileExtension(".cs");

                additionConfig?.Invoke(configurationBuilder);
            });
        }
        finally
        {
            exceptionText = $@"```
{exceptionText}
```
";
            exceptionText.ShouldMatchApproved(configurationBuilder =>
            {
                configurationBuilder
                    .WithDiscriminator("exceptionText")
                    .UseCallerLocation()
                    .SubFolder("CodeExamples")
                    .WithScrubber(scrubber);

                additionConfig?.Invoke(configurationBuilder);
            });
        }
    }
}