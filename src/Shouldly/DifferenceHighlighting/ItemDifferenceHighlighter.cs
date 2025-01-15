namespace Shouldly.DifferenceHighlighting;

class ItemDifferenceHighlighter
{
    public const string HighlightCharacter = "*";

    public string HighlightItem(string? item)
    {
        return HighlightCharacter + item + HighlightCharacter;
    }
}