namespace Sim.World
{
  public enum EntityKind
  {
    None = 0,
    Person = 1,
    Location = 2
  }

  static class EntityKindExtensions
  {
    public static int ToInt(this EntityKind entityKind)
    {
      return (int)entityKind;
    }

    public static EntityKind ToEntityKind(this int entityKind)
    {
      return (EntityKind)entityKind;
    }
  }
}