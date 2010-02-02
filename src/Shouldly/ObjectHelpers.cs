namespace Shouldly
{
    public static class ObjectHelpers
    {
        public static T As<T>(this object o)
        {
            return (T)o;
        }
    }
}