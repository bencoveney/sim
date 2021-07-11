namespace Sim.World
{
    public enum StringKind
    {
        None = 0,
        FirstName = 1,
        Surname = 2,
        LocationName = 3,
    }

    static class StringKindExtensions
    {
        public static int ToInt(this StringKind stringKind)
        {
            return (int)stringKind;
        }

        public static StringKind ToStringKind(this int stringKind)
        {
            return (StringKind)stringKind;
        }
    }
}