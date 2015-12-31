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

namespace DocumentationExamples
{
    public static class DocExampleWriter
    {
        static ConcurrentDictionary<string, List<MethodDeclarationSyntax>> _fileMethodsLookup = 
            new ConcurrentDictionary<string, List<MethodDeclarationSyntax>>();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Document(Action shouldMethod)
        {
            var stackTrace = new StackTrace(true);
            var caller = stackTrace.GetFrame(1);
            var callerFileName = caller.GetFileName();
            var callerMethod = caller.GetMethod();

            var testMethod = _fileMethodsLookup.GetOrAdd(callerFileName, fn =>
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

            try
            {
                body.ShouldMatchApproved(c => c
                    .WithDescriminator("codeSample")
                    .UseCallerLocation()
                    .SubFolder("CodeExamples"));
            }
            finally
            {
                exceptionText.ShouldMatchApproved(c => c
                    .WithDescriminator("exceptionText")
                    .UseCallerLocation()
                    .SubFolder("CodeExamples"));
            }
        }
    }
}