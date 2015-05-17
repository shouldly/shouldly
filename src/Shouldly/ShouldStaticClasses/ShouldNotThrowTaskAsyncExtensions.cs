using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;


namespace Shouldly
{   
    public static partial class Should
    {
      
        public static Task NotThrowAsync([InstantHandle] Func<Task> action, TimeSpan timeoutAfter,
            Func<string> customMessage)
        {
            return NotThrowAsync(action(),timeoutAfter,customMessage);
        }

        public static Task NotThrowAsync([InstantHandle] Task task, TimeSpan timeoutAfter,
            Func<string> customMessage)
        {
            try
            {
                return task.TimeoutAfter(timeoutAfter);
            }
            catch (TimeoutException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException(new ExpectedShouldlyMessage(ex.GetType(), customMessage).ToString());
            }
            
        }

    }

}
