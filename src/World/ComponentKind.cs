namespace Sim.World
{
  public enum ComponentKind
  {
    None = 0,
    PersonName = 1,
    Birth = 2,
    Death = 3,
    LocationName = 4,
    ParentLocation = 5,
    Position = 6,
    Home = 7,
  }

  static class ComponentKindExtensions
  {
    public static int ToInt(this ComponentKind componentKind)
    {
      return (int)componentKind;
    }

    public static ComponentKind ToComponentKind(this int componentKind)
    {
      return (ComponentKind)componentKind;
    }
  }
}