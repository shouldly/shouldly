﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Shouldly.Configuration;
using Xunit;
using Xunit.Abstractions;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, DisableTestParallelization = true)]

namespace DocumentationExamples
{
    public static class DocExampleWriter
    {
        static readonly ConcurrentDictionary<string, List<MethodDeclarationSyntax>> FileMethodsLookup =
            new ConcurrentDictionary<string, List<MethodDeclarationSyntax>>();

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

            Func<string, string> scrubber = v => Regex.Replace(v, @"\w:.+?shouldly\\src", "C:\\PathToCode\\shouldly\\src");
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
}