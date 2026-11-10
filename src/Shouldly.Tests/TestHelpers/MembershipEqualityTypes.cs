namespace Shouldly.Tests.TestHelpers;

#pragma warning disable CA1067 // These types deliberately implement IEquatable<T>/IComparable<T> without overriding Equals(object)

/// <summary>
/// Implements <see cref="IEquatable{T}"/> without overriding <see cref="object.Equals(object)"/> or
/// <see cref="object.GetHashCode"/> (CA1067), so <c>ShouldBe</c> treats two instances with the same value as equal
/// while BCL default equality and hashing don't. See issue #1404.
/// </summary>
public sealed class EquatableOnly(int value) : IEquatable<EquatableOnly>
{
    public int Value { get; } = value;

    public bool Equals(EquatableOnly? other) => other is not null && other.Value == Value;

    public override string ToString() => $"EquatableOnly({Value})";
}

/// <summary>
/// Implements only <see cref="IComparable{T}"/>, so <c>ShouldBe</c> treats two instances with the same value as
/// equal while BCL default equality doesn't.
/// </summary>
public sealed class ComparableOnly(int value) : IComparable<ComparableOnly>
{
    public int Value { get; } = value;

    public int CompareTo(ComparableOnly? other) => other is null ? 1 : Value.CompareTo(other.Value);

    public override string ToString() => $"ComparableOnly({Value})";
}

/// <summary>
/// Whose <c>Equals</c> accepts anything when called on a <see cref="Lenient"/> and nothing when called on a
/// <see cref="Strict"/>, so a test can tell which value an assertion called <c>Equals</c> on.
/// </summary>
public abstract class Receiver
{
    public override int GetHashCode() => 0;
}

public sealed class Lenient : Receiver
{
    public override bool Equals(object? obj) => true;

    public override int GetHashCode() => 0;

    public override string ToString() => "Lenient";
}

public sealed class Strict : Receiver
{
    public override bool Equals(object? obj) => ReferenceEquals(this, obj);

    public override int GetHashCode() => 0;

    public override string ToString() => "Strict";
}
#pragma warning restore CA1067
