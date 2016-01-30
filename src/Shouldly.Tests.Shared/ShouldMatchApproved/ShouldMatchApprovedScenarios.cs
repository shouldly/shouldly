#if !PORTABLE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Shouldly.Configuration;
using Shouldly.Tests.Strings;
using Xunit;

#if net45
using System.Threading.Tasks;
#endif

namespace Shouldly.Tests.Shared.ShouldMatchApproved
{
    public class ShouldMatchApprovedScenarios
    {
#if net35
        string targetDescriminator = "net35";
#elif net40
        string targetDescriminator = "net40";
#elif net45
        string targetDescriminator = "net45";
#endif

        readonly Func<string, string> _scrubber = v => Regex.Replace(v, @"\w:.+?shouldly\\src", "C:\\PathToCode\\shouldly\\src");

        [Fact]
        public void Simple()
        {
            "Bar".ShouldMatchApproved(c => c.WithDescriminator(targetDescriminator));
        }

        [Fact]
        public void MissingApprovedFile()
        {
            var errorMsg = $@"To approve the changes run this command:
copy /Y ""C:\PathToCode\shouldly\src\Shouldly.Tests.Shared\ShouldMatchApproved\ShouldMatchApprovedScenarios.MissingApprovedFile.{targetDescriminator}.received.txt"" ""C:\PathToCode\shouldly\src\Shouldly.Tests.Shared\ShouldMatchApproved\ShouldMatchApprovedScenarios.MissingApprovedFile.{targetDescriminator}.approved.txt""
----------------------------

Approval file C:\PathToCode\shouldly\src\Shouldly.Tests.Shared\ShouldMatchApproved\ShouldMatchApprovedScenarios.MissingApprovedFile.{targetDescriminator}.approved.txt
    does not exist";
            Verify.ShouldFail(() =>
"Bar".ShouldMatchApproved(c => c.WithDescriminator(targetDescriminator).NoDiff()),

errorWithSource: errorMsg,
errorWithoutSource: errorMsg,
messageScrubber: _scrubber);
        }

        [Fact]
        public void DifferencesUseShouldlyMessages()
        {
            var str = "Foo";
            Verify.ShouldFail(() =>
str.ShouldMatchApproved(c => c.WithDescriminator(targetDescriminator).NoDiff()),

errorWithSource:
$@"To approve the changes run this command:
copy /Y ""C:\PathToCode\shouldly\src\Shouldly.Tests.Shared\ShouldMatchApproved\ShouldMatchApprovedScenarios.DifferencesUseShouldlyMessages.{targetDescriminator}.received.txt"" ""C:\PathToCode\shouldly\src\Shouldly.Tests.Shared\ShouldMatchApproved\ShouldMatchApprovedScenarios.DifferencesUseShouldlyMessages.{targetDescriminator}.approved.txt""
----------------------------

str
    should match approved with options: Ignoring line endings
""Bar""
    but was
""Foo""
    difference
Difference     |  |    |    |   
               | \|/  \|/  \|/  
Index          | 0    1    2    
Expected Value | B    a    r    
Actual Value   | F    o    o    
Expected Code  | 66   97   114  
Actual Code    | 70   111  111  ",

errorWithoutSource:
$@"To approve the changes run this command:
copy /Y ""C:\PathToCode\shouldly\src\Shouldly.Tests.Shared\ShouldMatchApproved\ShouldMatchApprovedScenarios.DifferencesUseShouldlyMessages.{targetDescriminator}.received.txt"" ""C:\PathToCode\shouldly\src\Shouldly.Tests.Shared\ShouldMatchApproved\ShouldMatchApprovedScenarios.DifferencesUseShouldlyMessages.{targetDescriminator}.approved.txt""
----------------------------

""Foo""
    should match approved with options: Ignoring line endings
""Bar""
    but was not
    difference
Difference     |  |    |    |   
               | \|/  \|/  \|/  
Index          | 0    1    2    
Expected Value | B    a    r    
Actual Value   | F    o    o    
Expected Code  | 66   97   114  
Actual Code    | 70   111  111  ",

messageScrubber:
_scrubber);
        }

        [Fact]
        public void NoDiffToolsFound()
        {
            var diffTools = ShouldlyConfiguration.DiffTools.GetType()
                .GetField("_diffTools", BindingFlags.Instance | BindingFlags.NonPublic);
            var diffToolsCollection = (List<DiffTool>)diffTools.GetValue(ShouldlyConfiguration.DiffTools);
            var currentDiffTools = new List<DiffTool>(diffToolsCollection);

            try
            {
                diffToolsCollection.Clear();
                var ex = Should.Throw<ShouldAssertException>(() => ShouldlyConfiguration.DiffTools.GetDiffTool());
                ex.Message.ShouldBe(@"Cannot find a difftool to use, please open an issue or a PR to add support for your difftool.

In the meantime use 'ShouldlyConfiguration.DiffTools.RegisterDiffTool()' to add your own");
            }
            finally
            {
                diffToolsCollection.AddRange(currentDiffTools);
            }
        }

        [Fact]
        public void IgnoresLineEndingsByDefault()
        {
            var stacktrace = new StackTrace(true);
            var sourceFileDir = Path.GetDirectoryName(stacktrace.GetFrame(0).GetFileName());
            var approved = Path.Combine(sourceFileDir, $"ShouldMatchApprovedScenarios.IgnoresLineEndingsByDefault.{targetDescriminator}.approved.txt");
            File.WriteAllText(approved, "Different\nStyle\nLine\nBreaks");

            try
            {
                "Different\r\nStyle\r\nLine\r\nBreaks".ShouldMatchApproved(c => c.WithDescriminator(targetDescriminator));
            }
            finally
            {
                File.Delete(approved);
            }
        }

        [Fact]
        public void ChangingInstanceConfigDoesntChangeGlobal()
        {
            Should.Throw<ShouldMatchApprovedException>(() => "".ShouldMatchApproved(c => c.NoDiff()));

            ShouldlyConfiguration.ShouldMatchApprovedDefaults.Build().PreventDiff.ShouldBe(false);
        }

#if net45
        [Fact]
        public async Task HandlesAsync()
        {
            await Task.Delay(200);

            "Foo".ShouldMatchApproved();
        }
#endif
    }
}
#endif
