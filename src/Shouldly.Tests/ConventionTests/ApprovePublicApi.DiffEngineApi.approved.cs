namespace Shouldly
{
    public class DiffEngine : Shouldly.IDiffEngine
    {
        public static Shouldly.DiffEngine Instance { get; }
        public void Launch(string receivedFile, string approvedFile) { }
    }
    public static class ShouldMatchConfigurationExtensions
    {
        public static Shouldly.ShouldMatchConfiguration ConfigureDiffEngine(this Shouldly.ShouldMatchConfiguration configuration) { }
    }
}