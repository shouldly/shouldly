using Shouldly.Equivalency;

namespace Shouldly.Tests.InternalTests;

/// <summary>
/// What <c>ShouldBeEquivalentTo</c> does must also be possible for a shape provider that uses no reflection and knows
/// only the types it was written for, as a source-generated one would. These tests run the engine over such a
/// provider, written by hand, and hold it to the reflection provider's results.
/// </summary>
public class ClosedShapeProviderTests
{
    private static readonly IEquivalencyShapeProvider Closed = new ClosedShapeProvider(
        new LeafShape<string>(),
        new LeafShape<double>(),
        new ComplexShape<Shape>(new Member<Shape, string?>("Label", shape => shape.Label)),
        new ComplexShape<Circle>(
            new Member<Circle, string?>("Label", circle => circle.Label),
            new Member<Circle, double>("Radius", circle => circle.Radius)),
        new ComplexShape<Drawing>(
            new Member<Drawing, Shape?>("Main", drawing => drawing.Main),
            new Member<Drawing, IEnumerable<Shape>?>("Shapes", drawing => drawing.Shapes),
            new Member<Drawing, IEnumerable<string>?>("Tags", drawing => drawing.Tags)),
        new SequenceShape<IEnumerable<Shape>, Shape>(),
        new SequenceShape<List<Shape>, Shape>(),
        new SequenceShape<string[], string>(),
        new SequenceShape<Node[], Node>(),
        new SetShape<HashSet<string>, string>(keepsAnOrder: false),
        new SetShape<SortedSet<string>, string>(keepsAnOrder: true));

    public static TheoryData<string> Providers => ["closed", "reflection"];

    [Theory]
    [MemberData(nameof(Providers))]
    public void ComparesAValueByTheMembersOfItsOwnType(string provider)
    {
        var actual = new Drawing { Main = new Circle { Label = "c", Radius = 1 } };

        Compare(provider, actual, new Drawing { Main = new Circle { Label = "c", Radius = 1 } });
        Should.Throw<ShouldAssertException>(() =>
                Compare(provider, actual, new Drawing { Main = new Circle { Label = "c", Radius = 500 } }))
            .Message.ShouldContain("Radius [System.Double]");
    }

    [Theory]
    [MemberData(nameof(Providers))]
    public void ComparesTheItemsOfABaseTypedCollectionByTheirOwnTypes(string provider)
    {
        var actual = new Drawing { Shapes = new List<Shape> { new Circle { Label = "c", Radius = 1 }, new() { Label = "s" } } };

        Compare(provider, actual, new Drawing { Shapes = new List<Shape> { new Circle { Label = "c", Radius = 1 }, new() { Label = "s" } } });
        Should.Throw<ShouldAssertException>(() =>
                Compare(provider, actual, new Drawing { Shapes = new List<Shape> { new Circle { Label = "c", Radius = 2 }, new() { Label = "s" } } }))
            .Message.ShouldContain("Radius [System.Double]");
    }

    // A LINQ iterator's type can't be known ahead of time. Held as an IEnumerable<Shape> it is no more than its
    // items, so the provider shapes it as the type it is declared as.
    [Theory]
    [MemberData(nameof(Providers))]
    public void ComparesACollectionItCannotKnowAsTheTypeItIsDeclaredAs(string provider)
    {
        var circles = new[] { new Circle { Label = "a", Radius = 1 }, new Circle { Label = "b", Radius = 2 } };
        var actual = new Drawing { Shapes = circles.Where(_ => true) };

        Compare(provider, actual, new Drawing { Shapes = circles.Select(circle => new Circle { Label = circle.Label, Radius = circle.Radius }) });
        Should.Throw<ShouldAssertException>(() =>
            Compare(provider, actual, new Drawing { Shapes = circles.Select(circle => new Circle { Label = circle.Label, Radius = 9 }) }));
    }

    // Whether a set keeps an order of its own is for its shape to say.
    [Theory]
    [MemberData(nameof(Providers))]
    public void ComparesASetWithAListInOrderOnlyWhenTheSetKeepsOne(string provider)
    {
        Compare(provider, new Drawing { Tags = new HashSet<string> { "b", "a" } }, new Drawing { Tags = new[] { "a", "b" } });
        Compare(provider, new Drawing { Tags = new[] { "b", "a" } }, new Drawing { Tags = new HashSet<string> { "a", "b" } });
        Compare(provider, new Drawing { Tags = new SortedSet<string> { "b", "a" } }, new Drawing { Tags = new[] { "a", "b" } });

        Should.Throw<ShouldAssertException>(() =>
            Compare(provider, new Drawing { Tags = new SortedSet<string> { "b", "a" } }, new Drawing { Tags = new[] { "b", "a" } }));
    }

    // The provider must not fall back on the declared type here: a Shape's members are fewer than a Square's, and
    // the comparison would pass with the squares' sides different.
    [Fact]
    public void ThrowsForAValueWhoseTypeHasNoShape()
    {
        var actual = new Drawing { Main = new Square { Label = "s", Side = 1 } };

        var exception = Should.Throw<NotSupportedException>(() =>
            Compare("closed", actual, new Drawing { Main = new Square { Label = "s", Side = 2 } }));

        exception.Message.ShouldContain("ClosedShapeProviderTests+Square");
        exception.Message.ShouldContain("Main");
    }

    // An equality rule is a delegate looked up by the values' types, so it needs no shape: not for the type it
    // covers (Node's, registered in ModuleInitializer), which the closed provider has never heard of.
    [Theory]
    [MemberData(nameof(Providers))]
    public void ComparesValuesAnEqualityRuleCoversWithoutAShape(string provider)
    {
        Compare(provider, new Leaf("a"), new Leaf("a"));
        Compare(provider, new Node[] { new Leaf("a"), new Branch(new Leaf("b")) }, new Node[] { new Leaf("a"), new Branch(new Leaf("b")) });

        Should.Throw<ShouldAssertException>(() => Compare(provider, new Leaf("a"), new Leaf("b")));
        Should.Throw<ShouldAssertException>(() =>
                Compare(provider, new Node[] { new Leaf("a"), new Branch(new Leaf("b")) }, new Node[] { new Leaf("a"), new Branch(new Leaf("c")) }))
            .Message.ShouldContain("the equality rule for Node says they are not equal");
    }

    private static void Compare(string provider, object actual, object expected) =>
        EquivalencyComparison.Assert(
            actual,
            expected,
            new(),
            customMessage: null,
            actualExpression: "actual",
            provider == "closed" ? Closed : ReflectionShapeProvider.Instance);

    private class Shape
    {
        public string? Label { get; set; }
    }

    private class Circle : Shape
    {
        public double Radius { get; set; }
    }

    private class Square : Shape
    {
        public double Side { get; set; }
    }

    private class Drawing
    {
        public Shape? Main { get; set; }
        public IEnumerable<Shape>? Shapes { get; set; }
        public IEnumerable<string>? Tags { get; set; }
    }

    /// <summary>A provider of the shapes it is given and no others, each written out as a generator would emit it.</summary>
    private sealed class ClosedShapeProvider(params EquivalencyTypeShape[] shapes) : IEquivalencyShapeProvider
    {
        private readonly Dictionary<Type, EquivalencyTypeShape> _shapes = shapes.ToDictionary(shape => shape.Type);

        public EquivalencyTypeShape? GetShape(Type type, Type? declaredType = null)
        {
            if (_shapes.TryGetValue(type, out var shape))
                return shape;

            // One of .NET's own collections, held as a collection type there is a shape for: its items are all
            // there is to it, and the declared type's shape reads them.
            return declaredType is not null &&
                   type.Namespace?.StartsWith("System", StringComparison.Ordinal) == true &&
                   _shapes.TryGetValue(declaredType, out var declaredShape) &&
                   declaredShape.Kind == EquivalencyNodeKind.Sequence
                ? declaredShape
                : null;
        }
    }

    private abstract class ShapeOf<T>(EquivalencyNodeKind kind) : EquivalencyTypeShape
    {
        public override Type Type => typeof(T);
        public override EquivalencyNodeKind Kind => kind;
        public override bool KeepsAnOrder => false;
        public override IReadOnlyList<EquivalencyMemberShape> Members => [];
        public override EquivalencyMemberShape? FindMember(string name) => null;
        public override Type? ElementType => null;
        public override Type? KeyType => null;
        public override Type? ValueType => null;
        public override IEnumerable<KeyValuePair<object?, object?>> GetEntries(object instance) => throw new NotSupportedException();
    }

    private sealed class LeafShape<T>() : ShapeOf<T>(EquivalencyNodeKind.Leaf);

    private sealed class SequenceShape<T, TElement>() : ShapeOf<T>(EquivalencyNodeKind.Sequence)
    {
        public override Type? ElementType => typeof(TElement);
    }

    private sealed class SetShape<T, TElement>(bool keepsAnOrder) : ShapeOf<T>(EquivalencyNodeKind.Set)
    {
        public override bool KeepsAnOrder => keepsAnOrder;
        public override Type? ElementType => typeof(TElement);
    }

    private sealed class ComplexShape<T>(params EquivalencyMemberShape[] members) : ShapeOf<T>(EquivalencyNodeKind.Complex)
    {
        public override IReadOnlyList<EquivalencyMemberShape> Members => members;
        public override EquivalencyMemberShape? FindMember(string name) => members.FirstOrDefault(member => member.Name == name);
    }

    private sealed class Member<T, TMember>(string name, Func<T, TMember> get) : EquivalencyMemberShape
    {
        public override string Name => name;
        public override Type DeclaredType => typeof(TMember);
        public override object? GetValue(object instance) => get((T)instance);
    }
}
