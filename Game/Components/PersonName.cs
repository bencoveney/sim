using EntityComponentSystem;

namespace Sim.Components
{
    [Component]
    public class PersonName
    {
        public string FirstName;
        public string SecondName;
        public PersonName(string firstName, string secondName)
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