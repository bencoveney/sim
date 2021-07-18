using Sim.Ecs;

namespace Sim.Components
{
    public class PersonNameComponent : Component
    {
        private static uint kind = Component.kindGenerator.getNext();
        public override uint Kind { get { return kind; } }

        public string FirstName;
        public string SecondName;
        public PersonNameComponent(string firstName, string secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
        }

        public override string ToString()
        {
            return $"{FirstName} {SecondName}";
        }
    }
}