using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples;

public class ShouldBeNullNotNullExamples
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ShouldBeNullNotNullExamples(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ShouldBeNull()
    {
        DocExampleWriter.Document(
            () =>
            {
                var myRef = "Hello World";
                myRef.ShouldBeNull();
            },
            _testOutputHelper);
    }

    [Fact]
    public void NullableValueShouldBeNull()
    {
        DocExampleWriter.Document(
            () =>
            {
                int? nullableValue = 42;
                nullableValue.ShouldBeNull();
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldNotBeNull()
    {
        DocExampleWriter.Document(
            () =>
            {
                string? myRef = null;
                myRef.ShouldNotBeNull();
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldNotBeNullWithChaining()
    {
        DocExampleWriter.Document(
            () =>
            {
                var myRef = (string?)"1234";
                myRef.ShouldNotBeNull().Length.ShouldBe(5);
            },
            _testOutputHelper);
    }

    [Fact]
    public void NullableValueShouldNotBeNull()
    {
        DocExampleWriter.Document(
            () =>
            {
                int? myRef = null;
                myRef.ShouldNotBeNull();
            },
            _testOutputHelper);
    }

    [Fact]
    public void NullableValueShouldNotBeNullWithChaining()
    {
        DocExampleWriter.Document(
            () =>
            {
                SomeStruct? nullableValue = new SomeStruct { IntProperty = 41 };
                nullableValue.ShouldNotBeNull().IntProperty.ShouldBe(42);
            },
            _testOutputHelper);
    }

    public struct SomeStruct
    {
        public int IntProperty { get; set; }
    }
}