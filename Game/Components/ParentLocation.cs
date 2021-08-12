using EntityComponentSystem;

namespace Sim.Components
{
    [Component]
    public class ParentLocation
    {
        public int EntityId;
        public ParentLocation(int entityId)
        {
            EntityId = entityId;
        }

        public override string ToString()
        {
            return EntityId.ToString();
        }
    }
}