using NUnit.Framework;

namespace NUnit.TestData
{
	public class TestCaseBuilderFixture
	{
		[Test]
		public int NonVoidTest()
		{
			return 0;
		}

		[TestCase(5, Result = 1)]
		public void VoidTestCaseWithExpectedResult(int x)
		{
		}
	}
}
