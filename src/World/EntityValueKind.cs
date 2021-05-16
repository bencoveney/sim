namespace Sim.World
{
  public enum EntityValueKind
  {
    None = 0,
    Entity = 1,
  }

  static class EntityValueKindExtensions
  {
    public static int ToInt(this EntityValueKind entityValueKind)
    {
      return (int)entityValueKind;
    }

    public static EntityValueKind ToEntityValueKind(this int entityValueKind)
    {
      return (EntityValueKind)entityValueKind;
    }
  }
}