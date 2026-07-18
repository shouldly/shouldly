using System.Dynamic;

namespace Shouldly.Tests.DynamicShould;

/// <summary>
/// Covers DynamicObject subclasses that implement <see cref="IDynamicMetaObjectProvider"/>
/// without implementing <see cref="IDictionary{TKey,TValue}"/>. Before the fix HaveProperty
/// would unconditionally cast to <c>IDictionary&lt;string, object&gt;</c> and throw
/// InvalidCastException; now it asks the meta-object for its member names instead.
/// </summary>
public class HavePropertyDynamicObjectScenario
{
    private class CustomDynamic : DynamicObject
    {
        private readonly HashSet<string> _members;

        public CustomDynamic(params string[] members) => _members = [..members];

        public override IEnumerable<string> GetDynamicMemberNames() => _members;
    }

    [Fact]
    public void ShouldPass_when_DynamicObject_subclass_has_member()
    {
        dynamic testDynamicObject = new CustomDynamic("Foo", "Bar");
        Shouldly.DynamicShould.HaveProperty(() => testDynamicObject, "Foo");
    }

    [Fact]
    public void ShouldFail_when_DynamicObject_subclass_lacks_member()
    {
        dynamic testDynamicObject = new CustomDynamic("Bar");
        var ex = Should.Throw<ShouldAssertException>(() =>
            Shouldly.DynamicShould.HaveProperty(() => testDynamicObject, "Foo"));
        ex.Message.ShouldContain("Foo");
    }
}
