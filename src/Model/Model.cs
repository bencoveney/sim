using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sim.Model
{
  enum EntityTypes
  {
    Person
  }

  enum ComponentTypes
  {
    Person_Name,
    Person_Age
  }

  enum ValueTypes
  {
    Person_Name_FirstName,
    Person_Name_Surname,
    Person_Age_Days
  }

  public class SimContext : DbContext
  {
    private static bool _created = false;
    private static string _databasePath;
    public DbSet<Entity> Entities { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<IntValue> IntValues { get; set; }
    public DbSet<StringValue> StringValues { get; set; }
    public DbSet<FloatValue> FloatValues { get; set; }
    public DbSet<BoolValue> BoolValues { get; set; }
    public SimContext(string databasePath) : base()
    {
      if (!_created)
      {
        _databasePath = databasePath;
        _created = true;
        Database.EnsureDeleted();
        Database.EnsureCreated();
      }
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={_databasePath}");
  }

  public enum EntityName
  {
    None = 0,
    Person = 1
  }

  public class Entity
  {
    public int EntityId { get; set; }
    public EntityName Name { get; set; }
    public Entity Parent { get; set; }

    public List<Component> Components { get; } = new List<Component>();
  }

  public enum ComponentName
  {
    None = 0,
    Name = 1,
    Birth = 2,
    Death = 3
  }

  public class Component
  {
    public int ComponentId { get; set; }
    public ComponentName Name { get; set; }
    public List<IntValue> IntValues { get; } = new List<IntValue>();
    public List<StringValue> StringValues { get; } = new List<StringValue>();
    public List<FloatValue> FloatValues { get; } = new List<FloatValue>();
    public List<BoolValue> BoolValues { get; } = new List<BoolValue>();
  }

  public enum IntValueName
  {
    None = 0,
    Tick = 1
  }

  public class IntValue
  {
    public int IntValueId { get; set; }
    public IntValueName Name { get; set; }
    public int Value { get; set; }
  }

  public enum StringValueName
  {
    None = 0,
    FirstName = 1,
    Surname = 2
  }

  public class StringValue
  {
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
    public int BoolValueId { get; set; }
    public BoolValueName Name { get; set; }
    public bool Value { get; set; }
  }
}