namespace Sim.World
{
  public enum IntKind
  {
    None = 0,
    Tick = 1
  }

  static class IntKindExtensions
  {
    public static int ToInt(this IntKind intKind)
    {
      return (int)intKind;
    }

    public static IntKind ToIntKind(this int intKind)
    {
      return (IntKind)intKind;
    }
  }
}