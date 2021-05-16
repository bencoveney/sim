namespace Sim.Ecs
{

  public enum IntValueName
  {
    None = 0,
    Tick = 1
  }

  public class IntValue
  {
    public int ComponentId { get; set; }
    public int IntValueId { get; set; }
    public IntValueName Name { get; set; }
    public int Value { get; set; }
  }

  public enum StringValueName
  {
    None = 0,
    FirstName = 1,
    Surname = 2,
    LocationName = 3,
  }

  public class StringValue
  {
    public int ComponentId { get; set; }
    public int StringValueId { get; set; }
    public StringValueName Name { get; set; }
    public string Value { get; set; }
  }

  public enum FloatValueName
  {
    None = 0
  }

  public class FloatValue
  {
    public int ComponentId { get; set; }
    public int FloatValueId { get; set; }
    public FloatValueName Name { get; set; }
    public float Value { get; set; }
  }

  public enum BoolValueName
  {
    None = 0
  }

  public class BoolValue
  {
    public int ComponentId { get; set; }
    public int BoolValueId { get; set; }
    public BoolValueName Name { get; set; }
    public bool Value { get; set; }
  }
  public enum EntityValueName
  {
    None = 0,
    Entity = 1
  }
  public class EntityValue
  {
    public int ComponentId { get; set; }
    public int EntityValueId { get; set; }
    public EntityValueName Name { get; set; }
    public Entity Value { get; set; }
  }
}