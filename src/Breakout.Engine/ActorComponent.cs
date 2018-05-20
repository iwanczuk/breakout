namespace Breakout.Engine
{
    abstract public class ActorComponent
    {
        public Actor Actor { get; set; }

        public virtual void Init(Actor actor)
        {
            Actor = actor;
        }

        public virtual void Update()
        {

        }
    }
}
