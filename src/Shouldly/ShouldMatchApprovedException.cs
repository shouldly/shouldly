namespace Shouldly;

/// <summary>
/// Exception thrown when an approval test fails
/// </summary>
public class ShouldMatchApprovedException : ShouldAssertException
{
    /// <summary>
    /// Initializes a new instance of the ShouldMatchApprovedException class
    /// </summary>
    /// <param name="message">The error message</param>
    /// <param name="receivedFile">The path to the received file</param>
    /// <param name="approvedFile">The path to the approved file</param>
    /// <param name="viewerNotPresent">Whether a diff viewer is not present</param>
    public ShouldMatchApprovedException(string? message, string? receivedFile, string? approvedFile, bool viewerNotPresent) : base(
        GenerateMessage(message, receivedFile, approvedFile, viewerNotPresent))
    {
    }

    private static string GenerateMessage(string? message, string? receivedFile, string? approvedFile, bool viewerNotPresent)
    {
        var msg = "To approve the changes run this command:";

        if (ShouldlyEnvironmentContext.IsWindows())
        {
            msg += $"""

                    copy /Y "{receivedFile}" "{approvedFile}"
                    """;
        }
        else
        {
            msg += $"""

                    cp "{receivedFile}" "{approvedFile}"
                    """;
        }

        if (viewerNotPresent)
        {
            msg += """


                No diff viewer configured; if you want to automatically view the relevant diff,
                please install Shouldly.DiffEngine and configure it via
                ShouldMatchConfiguration.ShouldMatchApprovedDefaults.ConfigureDiffEngine();
                """;
        }

        msg += $"""

                ----------------------------

                {message}
                """;

        return msg;
    }
}