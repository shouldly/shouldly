// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Threading;

namespace NUnit.Framework.Constraints
{
    [TestFixture]
	public class AfterConstraintTest : ConstraintTestBase
	{
		private static bool value;

        //public AfterConstraintTest(int x) { }

		[SetUp]
		public void SetUp()
		{
			theConstraint = new DelayedConstraint(new EqualConstraint(true), 500);
			expectedDescription = "True after 500 millisecond delay";
			stringRepresentation = "<after 500 <equal True>>";

            value = false;
            //SetValueTrueAfterDelay(300);
		}

        internal object[] SuccessData = new object[] { true };
        internal object[] FailureData = new object[] { false, 0, null };
		internal string[] ActualValues = new string[] { "False", "0", "null" };

		internal object[] InvalidData = new object[] { InvalidDelegate };

        internal ActualValueDelegate[] SuccessDelegates = new ActualValueDelegate[] { DelegateReturningValue };
        internal ActualValueDelegate[] FailureDelegates = new ActualValueDelegate[] { DelegateReturningFalse, DelegateReturningZero };

        [Test, TestCaseSource("SuccessDelegates")]
        public void SucceedsWithGoodDelegates(ActualValueDelegate del)
        {
            SetValueTrueAfterDelay(300);
            Assert.That(theConstraint.Matches(del));
        }

        [Test,TestCaseSource("FailureDelegates")]
        public void FailsWithBadDelegates(ActualValueDelegate del)
        {
            Assert.IsFalse(theConstraint.Matches(del));
        }

        [Test]
        public void SimpleTest()
        {
            SetValueTrueAfterDelay(500);
            Assert.That(DelegateReturningValue, new DelayedConstraint(new EqualConstraint(true), 5000, 200));
        }

        [Test]
        public void SimpleTestUsingReference()
        {
            SetValueTrueAfterDelay(500);
            Assert.That(ref value, new DelayedConstraint(new EqualConstraint(true), 5000, 200));
        }

        [Test]
        public void ThatOverload_ZeroDelayIsAllowed()
        {
            Assert.That(DelegateReturningZero, new DelayedConstraint(new EqualConstraint(0), 0));
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ThatOverload_DoesNotAcceptNegativeDelayValues()
        {
            Assert.That(DelegateReturningZero, new DelayedConstraint(new EqualConstraint(0), -1));
        }

#if CLR_2_0 || CLR_4_0
        [Test]
        public void CanTestContentsOfList()
        {
            var worker = new System.ComponentModel.BackgroundWorker();
            var list = new System.Collections.Generic.List<int>();
            worker.RunWorkerCompleted += delegate { list.Add(1); };
            worker.DoWork += delegate { Thread.Sleep(1); };
            worker.RunWorkerAsync();
            Assert.That(list, Has.Count.EqualTo(1).After(5000, 100));
        }

        [Test]
        public void CanTestContentsOfRefList()
        {
            var worker = new System.ComponentModel.BackgroundWorker();
            var list = new System.Collections.Generic.List<int>();
            worker.RunWorkerCompleted += delegate { list.Add(1); };
            worker.DoWork += delegate { Thread.Sleep(1); };
            worker.RunWorkerAsync();
            Assert.That(ref list, Has.Count.EqualTo(1).After(5000, 100));
        }

        [Test]
        public void CanTestContentsOfDelegateReturningList()
        {
            var worker = new System.ComponentModel.BackgroundWorker();
            var list = new System.Collections.Generic.List<int>();
            worker.RunWorkerCompleted += delegate { list.Add(1); };
            worker.DoWork += delegate { Thread.Sleep(1); };
            worker.RunWorkerAsync();
            Assert.That(() => list, Has.Count.EqualTo(1).After(5000, 100));
        }

        [Test]
        public void CanTestInitiallyNullReference()
        {
            string statusString = null; // object starts off as null

            var worker = new System.ComponentModel.BackgroundWorker();
            worker.RunWorkerCompleted += delegate { statusString = "finished"; /* object non-null after work */ };
            worker.DoWork += delegate { Thread.Sleep(TimeSpan.FromSeconds(1)); /* simulate work */ };
            worker.RunWorkerAsync();

            Assert.That(ref statusString, Has.Length.GreaterThan(0).After(3000, 100));
        }

        [Test]
        public void CanTestInitiallyNullDelegate()
        {
            string statusString = null; // object starts off as null

            var worker = new System.ComponentModel.BackgroundWorker();
            worker.RunWorkerCompleted += delegate { statusString = "finished"; /* object non-null after work */ };
            worker.DoWork += delegate { Thread.Sleep(TimeSpan.FromSeconds(1)); /* simulate work */ };
            worker.RunWorkerAsync();

            Assert.That(() => statusString, Has.Length.GreaterThan(0).After(3000, 100));
        }
#endif

        private static int setValueTrueDelay;

		private void SetValueTrueAfterDelay(int delay)
		{
            setValueTrueDelay = delay;
            Thread thread = new Thread( SetValueTrueDelegate );
            thread.Start();
		}

		private static void MethodReturningVoid() { }
		private static TestDelegate InvalidDelegate = new TestDelegate(MethodReturningVoid);

		private static object MethodReturningValue() { return value; }
		private static ActualValueDelegate DelegateReturningValue = new ActualValueDelegate(MethodReturningValue);

		private static object MethodReturningFalse() { return false; }
		private static ActualValueDelegate DelegateReturningFalse = new ActualValueDelegate(MethodReturningFalse);

		private static object MethodReturningZero() { return 0; }
		private static ActualValueDelegate DelegateReturningZero = new ActualValueDelegate(MethodReturningZero);

        private static void MethodSetsValueTrue()
        {
            Thread.Sleep(setValueTrueDelay);
            value = true;
        }
		private ThreadStart SetValueTrueDelegate = new ThreadStart(MethodSetsValueTrue);
	}
}
