namespace Breakout.Engine
{
    public class PaddleActorComponent : ActorComponent
    {
        public override void Update()
        {
            PhysicsActorComponent boardPhysics = Actor.Game.Board.GetComponent<PhysicsActorComponent>();
            PhysicsActorComponent actorPhysics = Actor.GetComponent<PhysicsActorComponent>();

            if (Actor.Game.IsLeftDown)
            {
                actorPhysics.Position.X -= 20;
            }

            if (actorPhysics.Position.X - actorPhysics.Size.W / 2 < 0)
            {
                actorPhysics.Position.X = actorPhysics.Size.W / 2;
            }

            if (Actor.Game.IsRightDown)
            {
                actorPhysics.Position.X += 20;
            }

            if (actorPhysics.Position.X + actorPhysics.Size.W / 2 > boardPhysics.Size.W)
            {
                actorPhysics.Position.X = boardPhysics.Size.W - actorPhysics.Size.W / 2;
            }
        }
    }
}
