namespace Breakout.Engine
{
    public class PhysicsActorComponent : ActorComponent
    {
        public Point Position { get; set; }

        public Point Size { get; set; }

        public PhysicsActorComponent(Point position, Point size)
        {
            Position = position;
            Size = size;
        }
    }
}
