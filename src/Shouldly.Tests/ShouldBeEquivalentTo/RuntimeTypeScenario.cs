namespace Shouldly.Tests.ShouldBeEquivalentTo;

// A value is compared by the members of the type it is, whatever type the member or collection holding it declares:
// a Circle in a List<Shape> is compared by a circle's members, not by a shape's alone.
public class RuntimeTypeScenario
{
    [Fact]
    public void ShouldFailWhenADerivedMemberDiffersInABaseTypedMember()
    {
        var subject = new Drawing { Main = new Circle { Label = "c", Radius = 1 } };
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new Drawing { Main = new Circle { Label = "c", Radius = 500 } }, "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenADerivedMemberDiffersInABaseTypedList()
    {
        var subject = new List<Shape> { new Circle { Label = "c", Radius = 1 } };
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new List<Shape> { new Circle { Label = "c", Radius = 500 } }, "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenADerivedMemberDiffersInABaseTypedArrayOrDictionary()
    {
        Should.Throw<ShouldAssertException>(() =>
            new Shape[] { new Circle { Radius = 1 } }.ShouldBeEquivalentTo(new Shape[] { new Circle { Radius = 2 } }));

        Should.Throw<ShouldAssertException>(() =>
            new Dictionary<string, Shape> { ["a"] = new Circle { Radius = 1 } }
                .ShouldBeEquivalentTo(new Dictionary<string, Shape> { ["a"] = new Circle { Radius = 2 } }));
    }

    [Fact]
    public void ShouldFailWhenAMemberDiffersBehindAnInterface()
    {
        var subject = new List<ILabelled> { new Circle { Label = "c", Radius = 1 } };
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new List<ILabelled> { new Circle { Label = "c", Radius = 500 } }, "Some additional context"));
    }

    [Fact]
    public void ShouldPassWhenBaseTypedValuesMatch()
    {
        var subject = new Drawing
        {
            Main = new Circle { Label = "c", Radius = 1 },
            Shapes = [new Circle { Label = "c", Radius = 1 }, new Square { Label = "s", Side = 2 }]
        };

        subject.ShouldBeEquivalentTo(new Drawing
        {
            Main = new Circle { Label = "c", Radius = 1 },
            Shapes = [new Circle { Label = "c", Radius = 1 }, new Square { Label = "s", Side = 2 }]
        });
    }

    // A marker interface has no members, and the values held as one are compared by their own.
    [Fact]
    public void ShouldCompareValuesHeldAsAMarkerInterfaceByTheirOwnMembers()
    {
        var subject = new List<IEvent> { new Renamed("a") };

        subject.ShouldBeEquivalentTo(new List<IEvent> { new Renamed("a") });
        Should.Throw<ShouldAssertException>(() =>
            subject.ShouldBeEquivalentTo(new List<IEvent> { new Renamed("b") }));
    }

    [Fact]
    public void ShouldFailWhenTheActualValueIsADifferentDerivedType()
    {
        var subject = new Drawing { Main = new Square { Label = "x", Side = 1 } };
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new Drawing { Main = new Circle { Label = "x", Radius = 1 } }, "Some additional context"));
    }

    // The expectation's members are the ones compared, so an expected Shape asks nothing of a circle's radius.
    [Fact]
    public void ShouldPassWhenTheExpectedValueIsTheBaseTypeAndTheActualValueIsDerived()
    {
        var subject = new Drawing { Main = new Circle { Label = "c", Radius = 1 } };
        subject.ShouldBeEquivalentTo(new Drawing { Main = new Shape { Label = "c" } });
    }

    // What kind of collection a value is comes from its own type too.
    [Fact]
    public void ShouldCompareASetHeldAsAnEnumerableWithoutRegardToOrder()
    {
        var subject = new Tagged { Tags = new HashSet<string> { "a", "b", "c" } };
        subject.ShouldBeEquivalentTo(new Tagged { Tags = new HashSet<string> { "c", "b", "a" } });
    }

    [Fact]
    public void ShouldCompareTheMembersACollectionAddsWhenHeldAsItsBaseType()
    {
        var subject = new Tagged { Numbers = new PagedNumbers(10) { 1, 2 } };

        subject.ShouldBeEquivalentTo(new Tagged { Numbers = new PagedNumbers(10) { 1, 2 } });
        Should.Throw<ShouldAssertException>(() =>
            subject.ShouldBeEquivalentTo(new Tagged { Numbers = new PagedNumbers(99) { 1, 2 } }));
    }

    // A number held as an interface it implements is still a number: a leaf, compared by value.
    [Fact]
    public void ShouldCompareALeafHeldAsAnInterfaceByItsValue()
    {
        var subject = new Tagged { Rank = 5 };

        subject.ShouldBeEquivalentTo(new Tagged { Rank = 5 });
        Should.Throw<ShouldAssertException>(() =>
            subject.ShouldBeEquivalentTo(new Tagged { Rank = 6 }));
    }

    public interface ILabelled
    {
        string? Label { get; }
    }

    public class Shape : ILabelled
    {
        public string? Label { get; set; }
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }
    }

    public class Square : Shape
    {
        public double Side { get; set; }
    }

    public class Drawing
    {
        public Shape? Main { get; set; }
        public IReadOnlyList<Shape>? Shapes { get; set; }
    }

    public interface IEvent;

    public class Renamed(string name) : IEvent
    {
        public string Name { get; } = name;
    }

    public class Tagged
    {
        public IEnumerable<string>? Tags { get; set; }
        public List<int>? Numbers { get; set; }
        public IComparable? Rank { get; set; }
    }

    public class PagedNumbers(int totalCount) : List<int>
    {
        public int TotalCount { get; } = totalCount;
    }
}
