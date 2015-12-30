// TODO Try and get this working with PCL
#if !PORTABLE
using System;
using System.Diagnostics;
using System.IO;
using Shouldly.Internals;

namespace Shouldly
{
    [ShouldlyMethods]
    public static class ShouldMatchApprovedTestExtensions
    {
#if net35 || net40
        public static void ShouldMatchApproved(this string actual)
        {
            actual.ShouldMatchApproved(0);
        }
        public static void ShouldMatchApproved(this string actual, StringCompareShould stringCompareOptions)
        {
            var codeGetter = new ActualCodeTextGetter();
            var memberName = codeGetter.GetCodeText(actual, new StackTrace(true)).Trim('"');
            ShouldMatchApproved(actual, memberName, codeGetter.FileName, codeGetter.LineNumber, stringCompareOptions);
        }

        static void ShouldMatchApproved(
            this string actual,
            string memberName,
            string sourceFilePath,
            int sourceLineNumber, 
            StringCompareShould stringCompareOptions)
#else
        public static void ShouldMatchApproved(this string actual,
            StringCompareShould stringCompareOptions,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
#endif
        {
            var directoryName = Path.GetDirectoryName(sourceFilePath);
            var approvedFile = Path.Combine(directoryName, $"{memberName}.approved.txt");
            var receivedFile = Path.Combine(directoryName, $"{memberName}.received.txt");
            File.WriteAllText(receivedFile, actual);

            if (ShouldlyConfiguration.DiffTools.ShouldOpenDiffTool())
                ShouldlyConfiguration.DiffTools.GetDiffTool().Open(receivedFile, approvedFile);

            File.Exists(approvedFile).ShouldBe(true, approvedFile);
            var approvedFileContents = File.ReadAllText(approvedFile);
            var receivedFileContents = File.ReadAllText(receivedFile);

            approvedFileContents.ShouldBe(receivedFileContents, stringCompareOptions);
            File.Delete(receivedFile);
        }
    }
}
#endif
