namespace Shouldly
{
    public delegate string FilenameGenerator(Shouldly.TestMethodInfo testMethodInfo, string? discriminator, string fileType, string fileExtension);
    public class FindMethodUsingAttribute<T> : Shouldly.ITestMethodFinder
        where T : System.Attribute
    {
        public FindMethodUsingAttribute() { }
        public Shouldly.TestMethodInfo GetTestMethodInfo(System.Diagnostics.StackTrace stackTrace, int startAt = 0) { }
    }
    public class FirstNonShouldlyMethodFinder : Shouldly.ITestMethodFinder
    {
        public FirstNonShouldlyMethodFinder() { }
        public int Offset { get; set; }
        public Shouldly.TestMethodInfo GetTestMethodInfo(System.Diagnostics.StackTrace stackTrace, int startAt = 0) { }
    }
    public interface ITestMethodFinder
    {
        Shouldly.TestMethodInfo GetTestMethodInfo(System.Diagnostics.StackTrace stackTrace, int startAt = 0);
    }
    public class ShouldMatchApprovedException : Shouldly.ShouldAssertException
    {
        public ShouldMatchApprovedException(string? message, string? receivedFile, string? approvedFile) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldMatchApprovedTestExtensions
    {
        public static void ShouldMatchApproved(this string actual, System.Action<Shouldly.ShouldMatchConfigurationBuilder>? configureOptions = null, string? customMessage = null) { }
    }
    public class ShouldMatchConfiguration
    {
        public ShouldMatchConfiguration() { }
        public ShouldMatchConfiguration(Shouldly.ShouldMatchConfiguration initialConfig) { }
        public string? ApprovalFileSubFolder { get; set; }
        public string FileExtension { get; set; }
        public string? FilenameDiscriminator { get; set; }
        public Shouldly.FilenameGenerator FilenameGenerator { get; set; }
        public bool PreventDiff { get; set; }
        public System.Func<string, string>? Scrubber { get; set; }
        public Shouldly.StringCompareShould StringCompareOptions { get; set; }
        public Shouldly.ITestMethodFinder TestMethodFinder { get; set; }
        public static Shouldly.ShouldMatchConfigurationBuilder ShouldMatchApprovedDefaults { get; }
    }
    public class ShouldMatchConfigurationBuilder
    {
        public ShouldMatchConfigurationBuilder(Shouldly.ShouldMatchConfiguration initialConfig) { }
        public Shouldly.ShouldMatchConfiguration Build() { }
        public Shouldly.ShouldMatchConfigurationBuilder Configure(System.Action<Shouldly.ShouldMatchConfiguration> configure) { }
        public Shouldly.ShouldMatchConfigurationBuilder DoNotIgnoreLineEndings() { }
        public Shouldly.ShouldMatchConfigurationBuilder LocateTestMethodUsingAttribute<T>()
            where T : System.Attribute { }
        public Shouldly.ShouldMatchConfigurationBuilder NoDiff() { }
        public Shouldly.ShouldMatchConfigurationBuilder SubFolder(string subfolder) { }
        public Shouldly.ShouldMatchConfigurationBuilder UseCallerLocation() { }
        public Shouldly.ShouldMatchConfigurationBuilder WithDiscriminator(string fileDiscriminator) { }
        public Shouldly.ShouldMatchConfigurationBuilder WithFileExtension(string fileExtension) { }
        public Shouldly.ShouldMatchConfigurationBuilder WithFilenameGenerator(Shouldly.FilenameGenerator filenameGenerator) { }
        public Shouldly.ShouldMatchConfigurationBuilder WithScrubber(System.Func<string, string> scrubber) { }
        public Shouldly.ShouldMatchConfigurationBuilder WithStringCompareOptions(Shouldly.StringCompareShould stringCompareOptions) { }
    }
    public class TestMethodInfo
    {
        public TestMethodInfo(System.Diagnostics.StackFrame callingFrame) { }
        public string? DeclaringTypeName { get; }
        public string? MethodName { get; }
        public string? SourceFileDirectory { get; }
    }
}