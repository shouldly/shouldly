using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace NUnit.Core
{
    public class NUnitAsyncTestMethod : NUnitTestMethod
    {
	    private const string TaskWaitMethod = "Wait";
	    private const string TaskResultProperty = "Result";
	    private const string SystemAggregateException = "System.AggregateException";
	    private const string InnerExceptionsProperty = "InnerExceptions";
	    private const BindingFlags TaskResultPropertyBindingFlags = BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public;

	    public NUnitAsyncTestMethod(MethodInfo method) : base(method)
        {
        }

        protected override object RunTestMethod(TestResult testResult)
        {
            if (method.ReturnType == typeof (void))
            {
                return RunVoidAsyncMethod(testResult);
            }
            
            return RunTaskAsyncMethod(testResult);
        }

	    private object RunVoidAsyncMethod(TestResult testResult)
        {
            var previousContext = SynchronizationContext.Current;
            var currentContext = new AsyncSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(currentContext);

            try
            {
                object result = base.RunTestMethod(testResult);

	            try
	            {
		            currentContext.WaitForPendingOperationsToComplete();
	            }
	            catch (Exception e)
	            {
					throw new NUnitException("Rethrown", e);		            
	            }

                return result;
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(previousContext);
            }
        }

	    private object RunTaskAsyncMethod(TestResult testResult)
	    {
		    try
		    {
			    object task = base.RunTestMethod(testResult);

			    Reflect.InvokeMethod(method.ReturnType.GetMethod(TaskWaitMethod, new Type[0]), task);
			    PropertyInfo resultProperty = Reflect.GetNamedProperty(method.ReturnType, TaskResultProperty, TaskResultPropertyBindingFlags);

			    return resultProperty != null ? resultProperty.GetValue(task, null) : task;
		    }
		    catch (NUnitException e)
		    {
			    if (e.InnerException != null && 
					e.InnerException.GetType().FullName.Equals(SystemAggregateException))
			    {
				    IList<Exception> inner = (IList<Exception>)e.InnerException.GetType()
						.GetProperty(InnerExceptionsProperty).GetValue(e.InnerException, null);

				    throw new NUnitException("Rethrown", inner[0]);
			    }

			    throw;
		    }
	    }
    }
}