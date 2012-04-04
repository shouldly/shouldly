namespace Shouldly
{
    internal static class ShouldlyStringExtensions
    {
        internal static string Clip(this string stringToClip, int maximumStringLength)
        {
            if (stringToClip.Length > maximumStringLength)
            {
                stringToClip = stringToClip.Substring(0, maximumStringLength);
            }
            return stringToClip;
        }
    }
}
