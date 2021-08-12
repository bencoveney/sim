using EntityComponentSystem;

namespace Sim.Components
{
    [Component]
    public class Position
    {
        public int EntityId;
        public Position(int entityId)
        {
            EntityId = entityId;
        }

        public override string ToString()
        {
            return EntityId.ToString();
        }
    }
}