using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public static class DocExampleWriter
    {
        static readonly ConcurrentDictionary<string, List<MethodDeclarationSyntax>> FileMethodsLookup = 
            new ConcurrentDictionary<string, List<MethodDeclarationSyntax>>();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Document(Action shouldMethod, ITestOutputHelper testOutputHelper)
        {
            var stackTrace = new StackTrace(true);
            var caller = stackTrace.GetFrame(1);
            var callerFileName = caller.GetFileName();
            var callerMethod = caller.GetMethod();

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
            var body = string.Join(string.Empty, enumerable);
            var exceptionText = Should.Throw<ShouldAssertException>(shouldMethod).Message;

            testOutputHelper.WriteLine("Docs body:");
            testOutputHelper.WriteLine("");
            testOutputHelper.WriteLine(body);
            testOutputHelper.WriteLine("");
            testOutputHelper.WriteLine("");
            testOutputHelper.WriteLine("Exception text:");
            testOutputHelper.WriteLine("");
            testOutputHelper.WriteLine(exceptionText);


            var approvedFileFolder = $"CodeExamples/{callerMethod.DeclaringType.Name}";
            try
            {
                body.ShouldMatchApproved(c => c
                    .WithDescriminator("codeSample")
                    .UseCallerLocation()
                    .SubFolder(approvedFileFolder));
            }
            finally
            {
                exceptionText.ShouldMatchApproved(c =>
                {
                    c
                        .WithDescriminator("exceptionText")
                        .UseCallerLocation()
                        .SubFolder(approvedFileFolder);
                });
            }
        }
    }
}