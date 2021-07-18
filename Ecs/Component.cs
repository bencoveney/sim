namespace Ecs
{
    public abstract class Component
    {
        protected static IdGenerator kindGenerator = new IdGenerator();
        public abstract uint Kind { get; }
    }
}