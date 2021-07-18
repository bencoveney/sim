namespace Sim.Ecs
{
    public class IdGenerator
    {
        private uint value = 0;
        public uint getNext()
        {
            return this.value++;
        }
    }
}