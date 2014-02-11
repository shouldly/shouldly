#if net40
using System;
using System.Threading.Tasks;

namespace Shouldly
{
    public static partial class Should
    {
        public static TException Throw<TException>(Func<Task> actual) where TException : Exception
        {
            try
            {
                actual().Wait();
            }
            catch (AggregateException e)
            {
                var innerException = e.InnerException;
                if (innerException is TException)
                    return (TException) innerException;

                throw new ChuckedAWobbly(new ShouldlyMessage(typeof(TException), innerException.GetType()).ToString());
            }
            catch (Exception e)
            {
                if (e is TException)
                    return (TException) e;

                throw new ChuckedAWobbly(new ShouldlyMessage(typeof(TException), e.GetType()).ToString());
            }

            throw new ChuckedAWobbly(new ShouldlyMessage(typeof(TException)).ToString());
        }

        public static void NotThrow(Func<Task> action)
        {
            try
            {
                action().Wait();
            }
            catch (AggregateException ex)
            {
                throw new ChuckedAWobbly(new ShouldlyMessage(ex.InnerException.GetType()).ToString());
            }
            catch (Exception ex)
            {
                throw new ChuckedAWobbly(new ShouldlyMessage(ex.GetType()).ToString());
            }
        }

        public static T NotThrow<T>(Func<Task<T>> action)
        {
            try
            {
                return action().Result;
            }
            catch (AggregateException ex)
            {
                throw new ChuckedAWobbly(new ShouldlyMessage(ex.InnerException.GetType()).ToString());
            }
            catch (Exception ex)
            {
                throw new ChuckedAWobbly(new ShouldlyMessage(ex.GetType()).ToString());
            }
        }
    }
}
#endif