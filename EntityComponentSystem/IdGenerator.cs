namespace EntityComponentSystem
{
    public class IdGenerator
    {
        private int value = 0;
        public int GetNext()
        {
            return this.value++;
        }
    }
}