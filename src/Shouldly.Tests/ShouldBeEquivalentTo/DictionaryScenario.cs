using System.Collections.Generic;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeEquivalentTo
{
    public class DictionaryScenario
    {
        [Fact]
        public void ShouldPassWhenComparingIdenticalNestedDictionaries()
        {
            CreateNestedDict().ShouldBeEquivalentTo(CreateNestedDict());
        }

        [Fact]
        public void ShouldFailWhenComparingNonIdenticalNestedDictionariesWithSameKey()
        {
            var expected = CreateNestedDict();
            expected["outer"]["inner"] = "otherValue";
            var actual = CreateNestedDict();
            Verify.ShouldFail(() => actual.ShouldBeEquivalentTo(expected),
                errorWithSource: @"Comparing object equivalence, at path:
Verify.ShouldFail(actual [System.Collections.Generic.Dictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Collections.Generic.Dictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]]
    Key [outer] [System.Collections.Generic.Dictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]]
        Key [inner] [System.String]

    Expected value to be
""otherValue""
    but was
""value""",
                errorWithoutSource: @"Comparing object equivalence, at path:
<root> [System.Collections.Generic.Dictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Collections.Generic.Dictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]]
    Key [outer] [System.Collections.Generic.Dictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]]
        Key [inner] [System.String]

    Expected value to be
""otherValue""
    but was
""value""");
        }

        [Fact]
        public void ShouldFailWhenComparingNestedDictionariesOfDifferentSizes()
        {
            var expected = CreateNestedDict();
            expected["outer"]["anotherInner"] = "value";
            var actual = CreateNestedDict();
            Verify.ShouldFail(() => actual.ShouldBeEquivalentTo(expected),
                errorWithSource:
                @"Comparing object equivalence, at path:
Verify.ShouldFail(actual [System.Collections.Generic.Dictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Collections.Generic.Dictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]]
    Key [outer] [System.Collections.Generic.Dictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]]
        Count

    Expected value to be
2
    but was
1",
                errorWithoutSource: @"Comparing object equivalence, at path:
<root> [System.Collections.Generic.Dictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Collections.Generic.Dictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]]
    Key [outer] [System.Collections.Generic.Dictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]]
        Count

    Expected value to be
2
    but was
1");

        }

        [Fact]
        public void ShouldPassWhenComparingIdenticalNonNestedDictionaries()
        {
            CreateNonNestedDict().ShouldBeEquivalentTo(CreateNonNestedDict());
        }

        [Fact]
        public void ShouldFailWhenComparingNonNestedDictionariesWithDisjointKeys()
        {
            var expected = new Dictionary<string, string> {{"key", "value"}};
            var actual = new Dictionary<string, string> {{"otherKey", "value"}};

            Verify.ShouldFail(() => actual.ShouldBeEquivalentTo(expected),
                errorWithSource: @"Comparing object equivalence, at path:
Verify.ShouldFail(actual [System.Collections.Generic.Dictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]]
    Key [key]

    Expected value to be
""value""
    but was
null",
                errorWithoutSource: @"Comparing object equivalence, at path:
<root> [System.Collections.Generic.Dictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]]
    Key [key]

    Expected value to be
""value""
    but was
null");
        }

        private static Dictionary<string, Dictionary<string, string>> CreateNestedDict() =>
            new() {{"outer", CreateNonNestedDict()}};

        private static Dictionary<string, string> CreateNonNestedDict() =>
            new() {{"inner", "value"}};
    }
}