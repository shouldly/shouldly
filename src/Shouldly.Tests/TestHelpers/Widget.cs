namespace Shouldly.Tests.TestHelpers;

public class Widget
{
    public bool Enabled { get; set; }
    public string? Name { get; set; }

    public override string ToString() =>
        $"Name({Name}) Enabled({Enabled})";
}