using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class DocExampleWriter
{
    private static readonly Regex windowsPathRegex = new(@"\w:.+?\\src\\DocumentationExamples", RegexOptions.Compiled);
    private static readonly Regex unixPathRegex = new(@"/[^""\r\n]+?/src/DocumentationExamples", RegexOptions.Compiled);
    private static readonly Regex scrubbedPathRegex = new(@"C:\\PathToCode\\shouldly\\src\\DocumentationExamples[^""\s]*", RegexOptions.Compiled);
    private static readonly Regex unixCopyCommandRegex = new(@"^cp (?=""C:\\PathToCode)", RegexOptions.Compiled | RegexOptions.Multiline);

    // The approved files are generated with Windows paths and commands; rewrite unix
    // equivalents to that form so the tests pass on any OS and checkout location.
    private static readonly Func<string, string> scrubber = v =>
    {
        v = windowsPathRegex.Replace(v, @"C:\PathToCode\shouldly\src\DocumentationExamples");
        v = unixPathRegex.Replace(v, @"C:\PathToCode\shouldly\src\DocumentationExamples");
        v = scrubbedPathRegex.Replace(v, m => m.Value.Replace('/', '\\'));
        v = unixCopyCommandRegex.Replace(v, "copy /Y ");
        return v;
    };

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