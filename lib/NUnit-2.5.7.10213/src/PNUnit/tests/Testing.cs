using System;
using System.Threading;

using NUnit.Framework;

using PNUnit.Framework;

namespace TestLibraries
{
	
	[TestFixture]
	public class Testing
	{
		[Test]
		public void EqualTo19()
		{
			Assert.AreEqual(19, Cmp.Add(15,4));
		}


		[Test]
		public void Server()
		{
			PNUnitServices.Get().InitBarrier("BARRIER");
			PNUnitServices.Get().WriteLine("Server started");

			Thread.Sleep(10000);

			PNUnitServices.Get().EnterBarrier("BARRIER");                
			Assert.IsTrue(false, "The test failed");        
		}

		[Test]
		public void Client()
		{   
			PNUnitServices.Get().WriteLine("The client should wait until the server starts");
			PNUnitServices.Get().InitBarrier("BARRIER");                
			
			PNUnitServices.Get().EnterBarrier("BARRIER");

			Console.WriteLine("Server should be started now");
			Assert.IsTrue(true, "The test failed");                

		}
	}
}
