namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class NonRetraversableEnumerable
{
    private static IEnumerable<int> CreateTestEnumerable()
    {
        var foo = new TestEnumerable(new[] {
            new[] { 1, 2, 3 },
            new[] { 4, 5, 6 },
            new[] { 7, 8, 9 } });

        return foo.ReadLine();
    }

    [Fact]
    public void ActualEnumerableShouldOnlyBeTraversedOnce()
    {
        Verify.ShouldFail(
            () => CreateTestEnumerable().ShouldBe(new[] { 3, 2, 1 }),
            errorWithSource:
            @"CreateTestEnumerable()
    should be
[3, 2, 1]
    but was
[1, 2, 3]
    difference
[*1*, 2, *3*]",
            errorWithoutSource:
            @"[1, 2, 3]
    should be
[3, 2, 1]
    but was not
    difference
[*1*, 2, *3*]");
    }

    [Fact]
    public void ActualEnumerableShouldOnlyBeTraversedOnceWhenIgnoringOrder()
    {
        Verify.ShouldFail(
            () => CreateTestEnumerable().ShouldBe(new[] { 2, 3, 4 }, ignoreOrder: true),
            errorWithSource:
            @"CreateTestEnumerable()
    should be (ignoring order)
[2, 3, 4]
    but
CreateTestEnumerable()
    is missing
[4]
    and
[2, 3, 4]
    is missing
[1]",
            errorWithoutSource:
            @"[1, 2, 3]
    should be (ignoring order)
[2, 3, 4]
    but
[1, 2, 3]
    is missing
[4]
    and
[2, 3, 4]
    is missing
[1]");
    }

    [Fact]
    public void ExpectedEnumerableShouldOnlyBeTraversedOnce()
    {
        Verify.ShouldFail(
            () => new[] { 3, 2, 1 }.ShouldBe(CreateTestEnumerable()),
            errorWithSource:
            @"new[] { 3, 2, 1 }
    should be
[1, 2, 3]
    but was
[3, 2, 1]
    difference
[*3*, 2, *1*]",
            errorWithoutSource:
            @"[3, 2, 1]
    should be
[1, 2, 3]
    but was not
    difference
[*3*, 2, *1*]");
    }

    [Fact]
    public void ExpectedEnumerableShouldOnlyBeTraversedOnceWhenIgnoringOrder()
    {
        Verify.ShouldFail(
            () => new[] { 2, 3, 4 }.ShouldBe(CreateTestEnumerable(), ignoreOrder: true),
            errorWithSource:
            @"new[] { 2, 3, 4 }
    should be (ignoring order)
[1, 2, 3]
    but
new[] { 2, 3, 4 }
    is missing
[1]
    and
[1, 2, 3]
    is missing
[4]",
            errorWithoutSource:
            @"[2, 3, 4]
    should be (ignoring order)
[1, 2, 3]
    but
[2, 3, 4]
    is missing
[1]
    and
[1, 2, 3]
    is missing
[4]");
    }

    [Fact]
    public void TestEnumerableIsNotRetraversable()
    {
        var fooEnum = CreateTestEnumerable();

        fooEnum.ShouldSatisfyAllConditions(
            () => fooEnum.ShouldBe(new[] { 1, 2, 3 }),
            () => fooEnum.First().ShouldBe(4),
            () => fooEnum.First().ShouldBe(5),
            () => fooEnum.First().ShouldBe(6),
            () => fooEnum.Any().ShouldBeFalse(),
            () => fooEnum.First().ShouldBe(7),
            () => fooEnum.ShouldBe(new[] { 8, 9 }));
    }

    private class TestEnumerable
    {
        private readonly int[][] _baseData;
        private int _curLine;
        private int _curCol;

        public TestEnumerable(int[][] baseData)
        {
            _baseData = baseData;
            _curLine = 0;
            _curCol = -1;
        }

        public IEnumerable<int> ReadLine()
        {
            if (_curLine < _baseData.Length)
            {
                _curCol++;
                while (_curCol < _baseData[_curLine].Length)
                {
                    yield return _baseData[_curLine][_curCol];
                    _curCol++;
                }

                _curLine++;
                _curCol = -1;
            }
        }
    }
}