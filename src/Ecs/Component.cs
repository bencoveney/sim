using System.Collections.Generic;

namespace Sim.Ecs
{
  public enum ComponentName
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

  public class Component
  {
    public uint EntityId { get; set; }
    public int ComponentId { get; set; }
    public ComponentName Name { get; set; }
    public Dictionary<IntValueName, IntValue> Ints { get; } = new Dictionary<IntValueName, IntValue>();
    public Dictionary<StringValueName, StringValue> Strings { get; } = new Dictionary<StringValueName, StringValue>();
    public Dictionary<FloatValueName, FloatValue> Floats { get; } = new Dictionary<FloatValueName, FloatValue>();
    public Dictionary<BoolValueName, BoolValue> Bools { get; } = new Dictionary<BoolValueName, BoolValue>();
    public Dictionary<EntityValueName, EntityValue> Entities { get; } = new Dictionary<EntityValueName, EntityValue>();
    public void AddInt(IntValue intValue)
    {
      this.Ints.Add(intValue.Name, intValue);
      intValue.ComponentId = this.ComponentId;
    }
    public void AddString(StringValue stringValue)
    {
      this.Strings.Add(stringValue.Name, stringValue);
      stringValue.ComponentId = this.ComponentId;
    }
    public void AddFloat(FloatValue floatValue)
    {
      this.Floats.Add(floatValue.Name, floatValue);
      floatValue.ComponentId = this.ComponentId;
    }
    public void AddBool(BoolValue boolValue)
    {
      this.Bools.Add(boolValue.Name, boolValue);
      boolValue.ComponentId = this.ComponentId;
    }
    public void AddEntity(EntityValue entityValue)
    {
      this.Entities.Add(entityValue.Name, entityValue);
      entityValue.ComponentId = this.ComponentId;
    }
  }
}