using EntityComponentSystem;

namespace Sim.Components
{
    [Component]
    public class LocationName
    {
        public string Name;
        public LocationName(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}