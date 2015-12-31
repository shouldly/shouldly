#if !PORTABLE
namespace Shouldly.Configuration
{
    public class ShouldMatchConfiguration
    {
        public ShouldMatchConfiguration()
        {
            TestMethodFinder = new FirstNonShouldlyMethodFinder();
            FileExtension = "txt";
        }

        public StringCompareShould StringCompareOptions { get; set; }
        public string FilenameDescriminator { get; set; }
        public bool PreventDiff { get; set; }
        /// <summary>
        /// File extension without the .
        /// </summary>
        public string FileExtension { get; set; }

        public ITestMethodFinder TestMethodFinder { get; set; }
    }
}
#endif