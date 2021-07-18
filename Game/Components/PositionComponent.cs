using Ecs;

namespace Sim.Components
{
    public class PositionComponent : Component
    {
        private static uint kind = Component.kindGenerator.getNext();
        public override uint Kind { get { return kind; } }

        public uint EntityId;
        public PositionComponent(uint entityId)
        {
            EntityId = entityId;
        }

        public override string ToString()
        {
            return EntityId.ToString();
        }
    }
}