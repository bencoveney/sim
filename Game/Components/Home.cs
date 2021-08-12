using EntityComponentSystem;

namespace Sim.Components
{
    [Component]
    public class Home
    {
        public int EntityId;
        public Home(int entityId)
        {
            EntityId = entityId;
        }

        public override string ToString()
        {
            return EntityId.ToString();
        }
    }
}