using System.Collections.Generic;

namespace Sim.Ecs
{
  class Filter
  {
    private List<int> includes = new List<int>();
    private List<int> excludes = new List<int>();
    private List<Entity> cache = new List<Entity>();
    public Filter(IEnumerable<int> includes, IEnumerable<int> excludes)
    {
      foreach (int component in includes)
      {
        this.includes.Add(component);
      }
      foreach (int component in excludes)
      {
        this.excludes.Add(component);
      }
    }

    public Filter(IEnumerable<int> includes)
    {
      foreach (int component in includes)
      {
        this.includes.Add(component);
      }
    }

    public void OnComponentChanged(Entity entity)
    {
      if (
        includes.TrueForAll(include => entity.ComponentsByKind.ContainsKey(include)) &&
        excludes.TrueForAll(exclude => !entity.ComponentsByKind.ContainsKey(exclude))
      )
      {
        if (!cache.Contains(entity))
        {
          cache.Add(entity);
        }
      }
      else
      {
        if (cache.Contains(entity))
        {
          cache.Remove(entity);
        }
      }
    }
    public IEnumerable<Entity> GetEntities()
    {
      return this.cache;
    }
  }
}