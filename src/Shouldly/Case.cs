namespace Shouldly
{
    public enum Case
    {
        Sensitive,
        Insensitive
    }
    internal static class CaseExtensions
    {
        public static ShouldBeStringOptions ToOptions(this Case caseSensitivity)
        {
            return caseSensitivity == Case.Insensitive 
                ? ShouldBeStringOptions.IgnoreCase 
                : ShouldBeStringOptions.None;
        }
    }
}