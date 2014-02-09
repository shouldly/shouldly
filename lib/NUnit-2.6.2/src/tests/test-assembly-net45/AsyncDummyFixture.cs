using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace test_assembly_net45
{
	public class AsyncDummyFixture
	{
		[Test]
		public async void AsyncVoidTest()
		{
            await Task.Yield();
		}

		[Test]
		public async Task AsyncTaskTest()
		{
			await Task.Yield();
		}

		[Test]
		public async Task<int> AsyncTaskTTest()
		{
			return await Task.FromResult(1);
		}

		[TestCase]
		public async void AsyncVoidTestCaseWithoutResultCheck()
		{
			await Task.Run(() => 1);
		}

		[TestCase]
		public async Task AsyncTaskTestCaseWithoutResultCheck()
		{
			await Task.Run(() => 1);
		}

		[TestCase]
		public async Task<int> AsyncTaskTTestCaseWithoutResultCheck()
		{
			return await Task.Run(() => 1);
		}

		[TestCase(Result = 1)]
		public async void AsyncVoidTestCaseWithResultCheck()
		{
			await Task.Run(() => 1);
		}

		[TestCase(Result = 1)]
		public async Task AsyncTaskTestCaseWithResultCheck()
		{
			await Task.Run(() => 1);
		}

		[TestCase(Result = 1)]
		public async Task<int> AsyncTaskTTestCaseWithResultCheck()
		{
			return await Task.Run(() => 1);
		}

		[TestCase(ExpectedException = typeof(Exception))]
		public async Task<int> AsyncTaskTTestCaseExpectedExceptionWithoutResultCheck()
		{
			return await Throw();
		}

		private async Task<int> Throw()
		{
			return await Task.Run(() =>
			{
				throw new InvalidOperationException();
				return 1;
			});
		}

		[Test]
		public Task<int> NonAsyncTaskWithResult()
		{
			return Task.FromResult(1);
		}

		[Test]
		public Task NonAsyncTask()
		{
			return Task.Delay(0);
		}
	}
}