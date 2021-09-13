namespace Sim.Render
{
    public class Handle
    {
        private readonly int value;

        public Handle(int Value)
        {
            value = Value;
        }

        public static implicit operator int(Handle handle) => handle.value;

        public override string ToString()
        {
            return $"Handle [{value}]";
        }
    }
}