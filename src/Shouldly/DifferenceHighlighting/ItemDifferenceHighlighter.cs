namespace Shouldly.DifferenceHighlighting;

static class ItemDifferenceHighlighter
{
    public const string HighlightCharacter = "*";

    public static string HighlightItem(string? item) =>
        HighlightCharacter + item + HighlightCharacter;
}