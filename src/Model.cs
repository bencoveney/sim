using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sim
{
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

  public class Entity
  {
    public int EntityId { get; set; }
    public string Name { get; set; }
    public Entity Parent { get; set; }

    public List<Component> Components { get; } = new List<Component>();
  }

  public class Component
  {
    public int ComponentId { get; set; }
    public int Name { get; set; }
    public List<IntValue> IntValues { get; } = new List<IntValue>();
    public List<StringValue> StringValues { get; } = new List<StringValue>();
    public List<FloatValue> FloatValues { get; } = new List<FloatValue>();
    public List<BoolValue> BoolValues { get; } = new List<BoolValue>();
  }

  public class IntValue
  {
    public int IntValueId { get; set; }
    public int Name { get; set; }
    public int Value { get; set; }
  }

  public class StringValue
  {
    public int StringValueId { get; set; }
    public int Name { get; set; }
    public string Value { get; set; }
  }

  public class FloatValue
  {
    public int FloatValueId { get; set; }
    public int Name { get; set; }
    public float Value { get; set; }
  }

  public class BoolValue
  {
    public int BoolValueId { get; set; }
    public int Name { get; set; }
    public bool Value { get; set; }
  }
}