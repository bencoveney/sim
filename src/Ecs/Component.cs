using System.Collections.Generic;

namespace Sim.Ecs
{
    public class Component
    {
        public Component(uint id, int kind)
        {
            this.Id = id;
            this.Kind = kind;
        }

        public uint Id { get; private set; }
        public int Kind { get; set; }
        public Dictionary<int, IntValue> Ints { get; } = new Dictionary<int, IntValue>();
        public Dictionary<int, StringValue> Strings { get; } = new Dictionary<int, StringValue>();
        public Dictionary<int, EntityValue> Entities { get; } = new Dictionary<int, EntityValue>();
        public void AddInt(IntValue intValue)
        {
            this.Ints.Add(intValue.Kind, intValue);
        }
        public void AddString(StringValue stringValue)
        {
            this.Strings.Add(stringValue.Kind, stringValue);
        }
        public void AddEntity(EntityValue entityValue)
        {
            this.Entities.Add(entityValue.Kind, entityValue);
        }
    }
}