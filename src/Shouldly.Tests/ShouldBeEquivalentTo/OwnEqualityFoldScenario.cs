namespace Shouldly.Tests.ShouldBeEquivalentTo;

// ShouldBeEquivalentTo compares members. When a value's own equality agrees that two values differ, and they differ
// in several members, the failure reports them as one difference: the two values as their type writes them, and
// the members that differ. The outcome is the members' either way.
public class OwnEqualityFoldScenario
{
    [Fact]
    public void ShouldReportAValueThatDiffersInSeveralMembersAsOneDifference()
    {
        var subject = new { Distance = new Distance(1) };
        var expected = new { Distance = new Distance(2) };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
    }

    [Fact]
    public void ShouldReportASingleDifferingMemberByItsPath()
    {
        var exception = Should.Throw<ShouldAssertException>(() =>
            new GridCell(1, 1).ShouldBeEquivalentTo(new GridCell(1, 2)));

        exception.Message.ShouldContain("Column [System.Int32]");
        exception.Message.ShouldNotContain("they differ at");
    }

    [Fact]
    public void ShouldReportEachMemberOfAValueThatHasNoTextOfItsOwn()
    {
        var exception = Should.Throw<ShouldAssertException>(() =>
            new Amount(5, "GBP").ShouldBeEquivalentTo(new Amount(6, "USD")));

        exception.Message.ShouldContain("found 2 differences");
    }

    [Fact]
    public void ShouldReportEachMemberOfAValueThatReadsTheSameEitherWay()
    {
        // Both read as "1 m", so only the members show what differs.
        var exception = Should.Throw<ShouldAssertException>(() =>
            new Distance(1.001).ShouldBeEquivalentTo(new Distance(1.002)));

        exception.Message.ShouldContain("found 7 differences");
    }

    [Fact]
    public void ShouldReportEachMemberOfARecord()
    {
        var exception = Should.Throw<ShouldAssertException>(() =>
            new Person("Ann", 30).ShouldBeEquivalentTo(new Person("Bob", 40)));

        exception.Message.ShouldContain("found 2 differences");
    }

    [Fact]
    public void ShouldReportEachMemberWhenTheValuesAreOfDifferentTypes()
    {
        var exception = Should.Throw<ShouldAssertException>(() =>
            new DistanceLike { Metres = 1, Kilometres = 0.001 }.ShouldBeEquivalentTo(new Distance(2)));

        exception.Message.ShouldNotContain("they differ at");
    }

    /// <summary>A value whose public members are views of one number.</summary>
    public readonly struct Distance(double metres) : IEquatable<Distance>
    {
        private readonly double _metres = metres;

        public double Metres => _metres;
        public double Kilometres => _metres / 1000;
        public double Centimetres => _metres * 100;
        public double Millimetres => _metres * 1000;
        public double Feet => _metres / 0.3048;
        public double Inches => _metres / 0.0254;
        public double Miles => _metres / 1609.344;

        public bool Equals(Distance other) => _metres.Equals(other._metres);

        public override bool Equals(object? obj) => obj is Distance other && Equals(other);

        public override int GetHashCode() => _metres.GetHashCode();

        public override string ToString() => $"{_metres:0.##} m";
    }

    public class DistanceLike
    {
        public double Metres { get; set; }
        public double Kilometres { get; set; }
    }

    public readonly struct GridCell(int row, int column) : IEquatable<GridCell>
    {
        public int Row { get; } = row;
        public int Column { get; } = column;

        public bool Equals(GridCell other) => Row == other.Row && Column == other.Column;

        public override bool Equals(object? obj) => obj is GridCell other && Equals(other);

        public override int GetHashCode() => Row * 397 ^ Column;

        public override string ToString() => $"R{Row}C{Column}";
    }

    public readonly struct Amount(decimal value, string currency) : IEquatable<Amount>
    {
        public decimal Value { get; } = value;
        public string Currency { get; } = currency;

        public bool Equals(Amount other) => Value == other.Value && Currency == other.Currency;

        public override bool Equals(object? obj) => obj is Amount other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode() * 397 ^ Currency.GetHashCode();
    }

    public record Person(string Name, int Age);
}
