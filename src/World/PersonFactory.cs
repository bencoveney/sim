using Sim.Ecs;
using Sim.Utils;

namespace Sim.World
{
    class PersonFactory
    {
        public static EntityBuilder CreateElder(EntityPool entityPool, int ageRange)
        {
            var builder = entityPool.CreateBuilder(EntityKind.Person.ToInt());
            CreateElderName(builder);
            CreateBirth(builder, ageRange);
            return builder;
        }

        public static EntityBuilder CreateBaby(EntityPool entityPool, Entity parent1, Entity parent2, int currentTick)
        {
            var builder = entityPool.CreateBuilder(EntityKind.Person.ToInt());
            CreateElderName(builder);
            CreateBirthAt(builder, currentTick);
            CreateHome(builder, parent1.ComponentsByKind[ComponentKind.Home.ToInt()].Entities[EntityValueKind.Entity.ToInt()].Value);
            CreatePosition(builder, parent1.ComponentsByKind[ComponentKind.Position.ToInt()].Entities[EntityValueKind.Entity.ToInt()].Value);
            return builder;
        }

        private static Component CreateElderName(EntityBuilder builder)
        {
            var component = builder.AddComponent(ComponentKind.PersonName.ToInt());
            component.AddString(new StringValue(StringKind.FirstName.ToInt(), Random.Name()));
            component.AddString(new StringValue(StringKind.Surname.ToInt(), Random.Name()));
            return component;
        }

        private static Component CreateBabyName(EntityBuilder builder, Entity parent1, Entity parent2)
        {
            var component = builder.AddComponent(ComponentKind.PersonName.ToInt());
            component.AddString(new StringValue(StringKind.FirstName.ToInt(), Random.Name()));
            component.AddString(new StringValue(StringKind.Surname.ToInt(), Random.Name()));
            return component;
        }

        private static Component CreateBirth(EntityBuilder builder, int ageRange)
        {
            var component = builder.AddComponent(ComponentKind.Birth.ToInt());
            component.AddInt(new IntValue(IntKind.Tick.ToInt(), Random.random.Next(ageRange)));
            return component;
        }

        private static Component CreateBirthAt(EntityBuilder builder, int tick)
        {
            var component = builder.AddComponent(ComponentKind.Birth.ToInt());
            component.AddInt(new IntValue(IntKind.Tick.ToInt(), tick));
            return component;
        }

        public static Component CreateDeath(EntityBuilder builder, int deathTick)
        {
            var component = builder.AddComponent(ComponentKind.Death.ToInt());
            component.AddInt(new IntValue(IntKind.Tick.ToInt(), deathTick));
            return component;
        }

        public static Component CreateHome(EntityBuilder builder, Entity position)
        {
            var component = builder.AddComponent(ComponentKind.Home.ToInt());
            component.AddEntity(new EntityValue(EntityValueKind.Entity.ToInt(), position));
            return component;
        }

        public static Component CreatePosition(EntityBuilder builder, Entity position)
        {
            var component = builder.AddComponent(ComponentKind.Position.ToInt());
            component.AddEntity(new EntityValue(EntityValueKind.Entity.ToInt(), position));
            return component;
        }
    }
}