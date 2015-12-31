#if !PORTABLE
using System.Text.RegularExpressions;
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

        [Fact]
        public void Simple()
        {
            "Bar".ShouldMatchApproved(c => c.WithDescriminator(targetDescriminator));
        }

        [Fact]
        public void MissingApprovedFile()
        {
            Verify.ShouldFail(() =>
"Bar".ShouldMatchApproved(c => c.WithDescriminator(targetDescriminator).NoDiff()),

errorWithSource:
$@"Approval file ...\MissingApprovedFile.{targetDescriminator}.approved.txt
    does not exist",

errorWithoutSource:
$@"Approval file ...\MissingApprovedFile.{targetDescriminator}.approved.txt
    does not exist",

messageScrubber:
s => Regex.Replace(s, @"Approval file .+\\", "Approval file ...\\"));
        }

        [Fact]
        public void DifferencesUseShouldlyMessages()
        {
            var str = "Foo";
            Verify.ShouldFail(() =>
str.ShouldMatchApproved(c => c.WithDescriminator(targetDescriminator)),

errorWithSource:
@"str
    should match approved
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
@"""Foo""
    should match approved
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
s => Regex.Replace(s, @"Approval file .+\\", "Approval file ...\\"));
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
