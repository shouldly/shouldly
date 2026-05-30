using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class DocExampleWriter
{
    // Matches an absolute path (Windows "X:\..." or Unix "/...") up to a "src" path segment,
    // capturing the remainder. Used to normalise machine-specific paths in the docs.
    private static readonly Regex scrubberRegex = new(@"(?:[A-Za-z]:\\|/)[^\s""]*?[\\/]src[\\/]([^\s""]*)", RegexOptions.Compiled);

    // Normalise OS-specific output so the docs render identically on Windows and Unix: the approve
    // command (cp vs copy /Y) and the absolute path (forward vs back slashes, drive vs root).
    private static readonly Func<string, string> scrubber = v =>
        scrubberRegex.Replace(
            v.Replace("cp \"", "copy /Y \""),
            m => @"C:\PathToCode\shouldly\src\" + m.Groups[1].Value.Replace('/', '\\'));

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