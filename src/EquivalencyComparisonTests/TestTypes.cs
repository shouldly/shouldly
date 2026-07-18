namespace EquivalencyComparisonTests;

public class Person
{
    public string? Name { get; set; }
    public int Age { get; set; }
}

/// <summary>Structurally identical to <see cref="Person"/> but a different type.</summary>
public class Customer
{
    public string? Name { get; set; }
    public int Age { get; set; }
}

public class PersonWithAddress
{
    public string? Name { get; set; }
    public Address? Address { get; set; }
}

public class Address
{
    public string? Street { get; set; }
    public string? City { get; set; }
}

public class FieldHolder
{
    public string? PublicField;
    private string? _privateField;

    public FieldHolder(string? publicField, string? privateField)
    {
        PublicField = publicField;
        _privateField = privateField;
    }
}

public class PrivatePropertyHolder
{
    public string? PublicValue { get; set; }
    private string? PrivateValue { get; set; }

    public PrivatePropertyHolder(string? publicValue, string? privateValue)
    {
        PublicValue = publicValue;
        PrivateValue = privateValue;
    }
}

public class IndexerHolder
{
    public string? Name { get; set; }

    public int this[int i] => i * 2;
}

/// <summary>Reference type with value semantics: Equals/GetHashCode use Id only.</summary>
public class EqualsById
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public override bool Equals(object? obj) => obj is EqualsById other && other.Id == Id;
    public override int GetHashCode() => Id;
}

/// <summary>Reference type whose Equals always reports inequality despite identical members.</summary>
public class EqualsNever
{
    public string? Name { get; set; }

    public override bool Equals(object? obj) => false;
    public override int GetHashCode() => 0;
}

public class TypeHolder
{
    public Type? Type { get; set; }
}

public class Node
{
    public string? Name { get; set; }
    public Node? Next { get; set; }
}

public class Animal
{
    public string? Name { get; set; }
}

public class Dog : Animal
{
    public string? Breed { get; set; }
}

public class PetOwner
{
    // Declared type is Animal; may hold a Dog at runtime.
    public Animal? Pet { get; set; }
}

public record PersonRecord(string Name, int Age);

public record struct PointRecordStruct(int X, int Y);

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

public enum Color
{
    Red = 1,
    Green = 2,
}

public enum Shade
{
    Crimson = 1,
    Emerald = 2,
}

public class OrderHolder
{
    public List<int> Values { get; set; } = [];
}
