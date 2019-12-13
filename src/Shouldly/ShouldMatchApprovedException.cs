namespace Shouldly
{
    public class ShouldMatchApprovedException : ShouldAssertException
    {
        public ShouldMatchApprovedException(string message, string receivedFile, string approvedFile) : base(
            GenerateMessage(message, receivedFile, approvedFile))
        {
        }

        private static string GenerateMessage(string message, string receivedFile, string approvedFile)
        {
            var msg = @"To approve the changes run this command:";

            if (ShouldlyEnvironmentContext.IsWindows())
            {
                msg += $@"
copy /Y ""{receivedFile}"" ""{approvedFile}""";
            }
            else
            {
                msg += $@"
cp ""{receivedFile}"" ""{approvedFile}""";
            }
           
            msg += $@"
----------------------------

{message}";

            return msg;
        }
    }
}