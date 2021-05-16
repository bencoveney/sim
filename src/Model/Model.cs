using System.Collections.Generic;

namespace Sim.Model
{
  public enum EntityName
  {
    None = 0,
    Person = 1,
    Location = 2
  }

  public class Entity
  {
    public int EntityId { get; set; }
    public EntityName Name { get; set; }
    public Dictionary<ComponentName, Component> Components { get; } = new Dictionary<ComponentName, Component>();
    public void AddComponent(Component component)
    {
      this.Components.Add(component.Name, component);
      component.EntityId = this.EntityId;
      Updated.EntityUpdated(this);
    }
  }

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
    public int EntityId { get; set; }
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