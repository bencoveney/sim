
using System.Collections.Generic;
using Sim.Model;

namespace Sim.Filters
{
  class Filter
  {
    private List<ComponentName> includes = new List<ComponentName>();
    private List<ComponentName> excludes = new List<ComponentName>();
    private List<Entity> cache = new List<Entity>();
    public Filter(IEnumerable<ComponentName> includes, IEnumerable<ComponentName> excludes)
    {
      foreach (ComponentName component in includes)
      {
        this.includes.Add(component);
      }
      foreach (ComponentName component in excludes)
      {
        this.excludes.Add(component);
      }
    }

    public Filter(IEnumerable<ComponentName> includes)
    {
      foreach (ComponentName component in includes)
      {
        this.includes.Add(component);
      }
    }

    public void OnComponentChanged(Entity entity)
    {
      if (
        includes.TrueForAll(include => entity.Components.ContainsKey(include)) &&
        excludes.TrueForAll(exclude => !entity.Components.ContainsKey(exclude))
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