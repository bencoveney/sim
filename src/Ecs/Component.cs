using System.Collections.Generic;

namespace Sim.Ecs
{
  public class Component
  {
    public Component(uint id, int kind)
    {
      this.Id = id;
      this.Kind = kind;
    }

    public uint Id { get; private set; }
    public int Kind { get; set; }
    public Dictionary<IntValueName, IntValue> Ints { get; } = new Dictionary<IntValueName, IntValue>();
    public Dictionary<StringValueName, StringValue> Strings { get; } = new Dictionary<StringValueName, StringValue>();
    public Dictionary<FloatValueName, FloatValue> Floats { get; } = new Dictionary<FloatValueName, FloatValue>();
    public Dictionary<BoolValueName, BoolValue> Bools { get; } = new Dictionary<BoolValueName, BoolValue>();
    public Dictionary<EntityValueName, EntityValue> Entities { get; } = new Dictionary<EntityValueName, EntityValue>();
    public void AddInt(IntValue intValue)
    {
      this.Ints.Add(intValue.Name, intValue);
      intValue.ComponentId = this.Id;
    }
    public void AddString(StringValue stringValue)
    {
      this.Strings.Add(stringValue.Name, stringValue);
      stringValue.ComponentId = this.Id;
    }
    public void AddFloat(FloatValue floatValue)
    {
      this.Floats.Add(floatValue.Name, floatValue);
      floatValue.ComponentId = this.Id;
    }
    public void AddBool(BoolValue boolValue)
    {
      this.Bools.Add(boolValue.Name, boolValue);
      boolValue.ComponentId = this.Id;
    }
    public void AddEntity(EntityValue entityValue)
    {
      this.Entities.Add(entityValue.Name, entityValue);
      entityValue.ComponentId = this.Id;
    }
  }
}