#if NET_3_5 || NET_4_0 || NET_4_5
using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using NUnit.Core;
using NUnit.Core.Builders;
using NUnit.Framework;
using test_assembly_net45;

namespace nunit.core.tests.net45
{
	[TestFixture]
	public class NUnitAsyncTestMethodTests
	{
		private NUnitTestCaseBuilder _builder;

		[SetUp]
		public void Setup()
		{
			_builder = new NUnitTestCaseBuilder();
		}

		public IEnumerable TestCases
		{
			get
			{
				yield return new object[] { Method(f => f.VoidTestSuccess()), ResultState.Success, 1 };
				yield return new object[] { Method(f => f.VoidTestFailure()), ResultState.Failure, 1 };
				yield return new object[] { Method(f => f.VoidTestError()), ResultState.Error, 0 };
				yield return new object[] { Method(f => f.VoidTestExpectedException()), ResultState.Success, 0 };

				yield return new object[] { Method(f => f.TaskTestSuccess()), ResultState.Success, 1 };
				yield return new object[] { Method(f => f.TaskTestFailure()), ResultState.Failure, 1 };
				yield return new object[] { Method(f => f.TaskTestError()), ResultState.Error, 0 };
				yield return new object[] { Method(f => f.TaskTestExpectedException()), ResultState.Success, 0 };

				yield return new object[] { Method(f => f.TaskTTestCaseWithResultCheckSuccess()), ResultState.Success, 0 };
				yield return new object[] { Method(f => f.TaskTTestCaseWithResultCheckFailure()), ResultState.Failure, 0 };
				yield return new object[] { Method(f => f.TaskTTestCaseWithResultCheckError()), ResultState.Failure, 0 };
				yield return new object[] { Method(f => f.TaskTTestCaseWithResultCheckSuccessReturningNull()), ResultState.Success, 0 };
				yield return new object[] { Method(f => f.TaskTTestCaseWithoutResultCheckExpectedExceptionSuccess()), ResultState.Success, 0 };

				yield return new object[] { Method(f => f.NestedVoidTestSuccess()), ResultState.Success, 1 };
				yield return new object[] { Method(f => f.NestedVoidTestFailure()), ResultState.Failure, 1 };
				yield return new object[] { Method(f => f.NestedVoidTestError()), ResultState.Error, 0 };

				yield return new object[] { Method(f => f.NestedTaskTestSuccess()), ResultState.Success, 1 };
				yield return new object[] { Method(f => f.NestedTaskTestFailure()), ResultState.Failure, 1 };
				yield return new object[] { Method(f => f.NestedTaskTestError()), ResultState.Error, 0 };

				yield return new object[] { Method(f => f.VoidTestMultipleSuccess()), ResultState.Success, 1 };
				yield return new object[] { Method(f => f.VoidTestMultipleFailure()), ResultState.Failure, 1 };
				yield return new object[] { Method(f => f.VoidTestMultipleError()), ResultState.Error, 0 };

				yield return new object[] { Method(f => f.TaskTestMultipleSuccess()), ResultState.Success, 1 };
				yield return new object[] { Method(f => f.TaskTestMultipleFailure()), ResultState.Failure, 1 };
				yield return new object[] { Method(f => f.TaskTestMultipleError()), ResultState.Error, 0 };

				yield return new object[] { Method(f => f.VoidCheckTestContextAcrossTasks()), ResultState.Success, 2 };
				yield return new object[] { Method(f => f.VoidCheckTestContextWithinTestBody()), ResultState.Success, 2 };
				yield return new object[] { Method(f => f.TaskCheckTestContextAcrossTasks()), ResultState.Success, 2 };
				yield return new object[] { Method(f => f.TaskCheckTestContextWithinTestBody()), ResultState.Success, 2 };

				yield return new object[] { Method(f => f.VoidAsyncVoidChildCompletingEarlierThanTest()), ResultState.Success, 0 };
				yield return new object[] { Method(f => f.VoidAsyncVoidChildThrowingImmediately()), ResultState.Success, 0 };
			}
		}

		[Test]
		[TestCaseSource("TestCases")]
		public void RunTests(MethodInfo testMethod, ResultState resultState, int assertionCount)
		{
			var method = _builder.BuildFrom(testMethod);

			var result = method.Run(new NullListener(), TestFilter.Empty);

			Assert.That(result.Executed, Is.True, "Was not executed");
			Assert.That(result.ResultState, Is.EqualTo(resultState), "Wrong result state");
			Assert.That(result.AssertCount, Is.EqualTo(assertionCount), "Wrong assertion count");
		}

		[Test]
		public void SynchronizationContextSwitching()
		{
			var context = new CustomSynchronizationContext();

			SynchronizationContext.SetSynchronizationContext(context);

			var method = _builder.BuildFrom(Method(f => f.VoidAssertSynchronizationContext()));

			var result = method.Run(new NullListener(), TestFilter.Empty);

			Assert.AreSame(context, SynchronizationContext.Current);
			Assert.That(result.Executed, Is.True, "Was not executed");
			Assert.That(result.ResultState, Is.EqualTo(ResultState.Success), "Wrong result state");
			Assert.That(result.AssertCount, Is.EqualTo(1), "Wrong assertion count");
		}

		private static MethodInfo Method(Expression<Action<AsyncRealFixture>> action)
		{
			return ((MethodCallExpression)action.Body).Method;
		}

		public class CustomSynchronizationContext : SynchronizationContext
		{
		}
	}
}
#endif