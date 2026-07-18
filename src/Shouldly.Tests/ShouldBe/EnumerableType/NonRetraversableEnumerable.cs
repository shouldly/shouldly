namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class NonRetraversableEnumerable
{
    private static IEnumerable<int> CreateTestEnumerable()
    {
        var foo = new TestEnumerable([
            [1, 2, 3],
            [4, 5, 6],
            [7, 8, 9]
        ]);

        return foo.ReadLine();
    }

    [Fact]
    public void ActualEnumerableShouldOnlyBeTraversedOnce()
    {
        Verify.ShouldFail(
            () => CreateTestEnumerable().ShouldBe([3, 2, 1]));
    }

    [Fact]
    public void ActualEnumerableShouldOnlyBeTraversedOnceWhenIgnoringOrder()
    {
        Verify.ShouldFail(
            () => CreateTestEnumerable().ShouldBe([2, 3, 4], ignoreOrder: true));
    }

    [Fact]
    public void ExpectedEnumerableShouldOnlyBeTraversedOnce()
    {
        Verify.ShouldFail(
            () => new[] { 3, 2, 1 }.ShouldBe(CreateTestEnumerable()));
    }

    [Fact]
    public void ExpectedEnumerableShouldOnlyBeTraversedOnceWhenIgnoringOrder()
    {
        Verify.ShouldFail(
            () => new[] { 2, 3, 4 }.ShouldBe(CreateTestEnumerable(), ignoreOrder: true));
    }

    [Fact]
    public void TestEnumerableIsNotRetraversable()
    {
        var fooEnum = CreateTestEnumerable();

        fooEnum.ShouldSatisfy(
        [
            e => e.ShouldBe([1, 2, 3]),
            e => e.First().ShouldBe(4),
            e => e.First().ShouldBe(5),
            e => e.First().ShouldBe(6),
            e => e.Any().ShouldBeFalse(),
            e => e.First().ShouldBe(7),
            e => e.ShouldBe([8, 9])
        ]);
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