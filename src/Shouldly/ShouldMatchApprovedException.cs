namespace Shouldly
{
    public class ShouldMatchApprovedExceptionContext
    {
        public string Message { private get; set; }
        
        public string Received { private get; set; }
        
        public string Approved { private get; set; }

        public string GenerateMessage()
        {
            var message = @"To approve the changes run this command:";

            if (ShouldlyEnvironmentContext.IsWindows())
            {
                message += $@"
copy /Y ""{Received}"" ""{Approved}""";
            }
            else
            {
                message += $@"
cp ""{Received}"" ""{Approved}""";
            }
           
            message += $@"
----------------------------

{Message}";

            return message;
        }
    } 
    
    public class ShouldMatchApprovedException : ShouldAssertException
    {
        public ShouldMatchApprovedException(ShouldMatchApprovedExceptionContext context) : base(context.GenerateMessage())
        {
        }
    }
}