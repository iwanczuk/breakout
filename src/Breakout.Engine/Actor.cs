namespace Breakout.Engine
{
    using System;
    using System.Collections.Generic;

    sealed public class Actor
    {
        public Game Game { get; }

        private readonly Dictionary<Type, ActorComponent> components = new Dictionary<Type, ActorComponent>();

        public Actor(Game game)
        {
            Game = game;
        }

        public void AddComponent<TActorComponent>(Func<TActorComponent> fatory) where TActorComponent : ActorComponent
        {
            ActorComponent component;

            component = fatory.Invoke();

            component.Init(this);

            components[typeof(TActorComponent)] = component;
        }

        public TActorComponent GetComponent<TActorComponent>() where TActorComponent : ActorComponent
        {
            return (TActorComponent) components[typeof(TActorComponent)];
        }

        public void Update()
        {
            foreach (KeyValuePair<Type, ActorComponent> pair in components)
            {
                pair.Value.Update();
            }
        }
    }
}
