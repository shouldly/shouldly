using System.Collections;
using System.Reflection;
using NUnit.Core.Builders;
using NUnit.Framework;
using NUnit.TestData;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class NUnitTestCaseBuilderTests
	{
		private NUnitTestCaseBuilder _sut;

		[SetUp]
		public void Setup()
		{
			_sut = new NUnitTestCaseBuilder();
		}

		public IEnumerable TestsSource
		{
			get
			{
				return new TestCaseData[] {
                    new TestCaseData( Method("NonVoidTest"), RunState.NotRunnable )
                };
			}
		}

		public IEnumerable TestCasesSource
		{
			get
			{
				return new TestCaseData[] {
                    new TestCaseData( Method("VoidTestCaseWithExpectedResult"), RunState.NotRunnable )
                };
			}
		}

		[TestCaseSource("TestsSource")]
		public void Tests(MethodInfo method, RunState state)
		{
			Test built = _sut.BuildFrom(method);

			Assert.That(built, Is.InstanceOf(typeof(NUnitTestMethod)));
			Assert.That(built.RunState, Is.EqualTo(state));
		}

		[TestCaseSource("TestCasesSource")]
		public void TestCases(MethodInfo method, RunState state)
		{
			Test built = _sut.BuildFrom(method);

			NUnitTestMethod testMethod = built.Tests[0] as NUnitTestMethod;

			Assert.IsNotNull(testMethod);

			Assert.That(testMethod.RunState, Is.EqualTo(state));
		}

		public MethodInfo Method(string name)
		{
			return typeof (TestCaseBuilderFixture).GetMethod(name);
		}
	}
}

