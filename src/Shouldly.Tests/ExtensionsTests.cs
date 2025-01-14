namespace Shouldly.Tests;

public class ExtensionsTests
{
    [Fact]
    public void Should_Cast_To_Object()
    {
        Should.NotThrow(
            () => 1.As<object>());
    }

    [Fact]
    public void Should_Cast_To_Type()
    {
        object person = new MyThing();
        Should.NotThrow(
            () => person.As<MyThing>().ShouldNotBeNull());
    }

    [Fact]
    public void Should_Return_Null()
    {
        object person = new MyThing();
        Should.NotThrow(
            () => person.As<MyDecoratedThing>().ShouldBeNull());
    }
}
