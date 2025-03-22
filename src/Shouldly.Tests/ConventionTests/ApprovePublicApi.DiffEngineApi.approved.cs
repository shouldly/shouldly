namespace Shouldly
{
    public class DiffEngine : Shouldly.IDiffEngine
    {
        public static Shouldly.DiffEngine Instance { get; }
        public void Launch(string receivedFile, string approvedFile) { }
    }
    public static class ShouldMatchConfigurationBuilderExtensions
    {
        public static Shouldly.ShouldMatchConfigurationBuilder ConfigureDiffEngine(this Shouldly.ShouldMatchConfigurationBuilder builder) { }
        public static Shouldly.ShouldMatchConfigurationBuilder Diff(this Shouldly.ShouldMatchConfigurationBuilder builder) { }
    }
}