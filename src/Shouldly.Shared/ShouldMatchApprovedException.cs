namespace Shouldly
{
    public class ShouldMatchApprovedException : ShouldAssertException
    {
        public ShouldMatchApprovedException(string message, string receivedFile, string approvedFile) :
            base($@"To approve the changes run this command:
copy /Y ""{receivedFile}"" ""{approvedFile}""
----------------------------

{message}")
        {
        }
    }
}