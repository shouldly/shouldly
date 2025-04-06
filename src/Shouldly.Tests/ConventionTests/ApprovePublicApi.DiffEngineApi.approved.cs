namespace Shouldly
{
    public class DiffEngineDiffViewer : Shouldly.IDiffViewer
    {
        public static Shouldly.DiffEngineDiffViewer Instance { get; }
        public void Launch(string receivedFile, string approvedFile) { }
    }
    public static class ShouldMatchConfigurationBuilderExtensions
    {
        public static Shouldly.ShouldMatchConfigurationBuilder ConfigureDiffEngine(this Shouldly.ShouldMatchConfigurationBuilder builder) { }
        public static Shouldly.ShouldMatchConfigurationBuilder Diff(this Shouldly.ShouldMatchConfigurationBuilder builder) { }
    }
}