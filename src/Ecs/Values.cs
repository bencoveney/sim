namespace Sim.Ecs
{
    public class IntValue
    {
        public IntValue(int kind, int value)
        {
            Kind = kind;
            Value = value;
        }
        public int Kind { get; private set; }
        public int Value { get; set; }
    }
    public class StringValue
    {
        public StringValue(int kind, string value)
        {
            Kind = kind;
            Value = value;
        }
        public int Kind { get; private set; }
        public string Value { get; set; }
    }
    public class EntityValue
    {
        public EntityValue(int kind, Entity value)
        {
            Kind = kind;
            Value = value;
        }
        public int Kind { get; private set; }
        public Entity Value { get; set; }
    }
}