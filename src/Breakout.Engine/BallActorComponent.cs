namespace Breakout.Engine
{
    public class BallActorComponent : ActorComponent
    {
        public Point Speed { get; set; }

        public bool IsAlive { get; set; } = true;

        public BallActorComponent(Point speed)
        {
            Speed = speed;
        }

        public override void Update()
        {
            PhysicsActorComponent boardPhysics = Actor.Game.Board.GetComponent<PhysicsActorComponent>();
            PhysicsActorComponent paddlePhysics = Actor.Game.Paddle.GetComponent<PhysicsActorComponent>();
            PhysicsActorComponent actorPhysics = Actor.GetComponent<PhysicsActorComponent>();

            actorPhysics.Position.X += Speed.X;

            if (actorPhysics.Position.X - actorPhysics.Size.W / 2 < 0)
            {
                actorPhysics.Position.X = actorPhysics.Size.W / 2;

                Speed.X *= -1;
            }

            if (actorPhysics.Position.X + actorPhysics.Size.W / 2 > boardPhysics.Size.W)
            {
                actorPhysics.Position.X = boardPhysics.Size.W - actorPhysics.Size.W / 2;

                Speed.X *= -1;
            }

            if (Actor.Game.InCollision(Actor, Actor.Game.Paddle))
            {
                actorPhysics.Position.X -= Speed.X;

                Speed.X *= -1;
            }

            foreach (var block in Actor.Game.Blocks)
            {
                BlockActorComponent blockComponent = block.GetComponent<BlockActorComponent>();

                if (blockComponent.IsAlive && Actor.Game.InCollision(Actor, block))
                {
                    blockComponent.IsAlive = false;

                    actorPhysics.Position.X -= Speed.X;

                    Speed.X *= -1;
                }
            }

            actorPhysics.Position.Y += Speed.Y;

            if (actorPhysics.Position.Y - actorPhysics.Size.H / 2 < 0)
            {
                actorPhysics.Position.Y = actorPhysics.Size.H / 2;

                Speed.Y *= -1;
            }

            if (actorPhysics.Position.Y + actorPhysics.Size.H / 2 > boardPhysics.Size.H)
            {
                IsAlive = false;
            }

            if (Actor.Game.InCollision(Actor, Actor.Game.Paddle))
            {
                actorPhysics.Position.Y = paddlePhysics.Position.Y - paddlePhysics.Size.H / 2 - actorPhysics.Size.H / 2;

                Speed.X = (actorPhysics.Position.X - paddlePhysics.Position.X) / -5;
                Speed.Y *= -1;
            }

            foreach (var block in Actor.Game.Blocks)
            {
                BlockActorComponent blockComponent = block.GetComponent<BlockActorComponent>();

                if (blockComponent.IsAlive && Actor.Game.InCollision(Actor, block))
                {
                    blockComponent.IsAlive = false;

                    actorPhysics.Position.Y -= Speed.Y;

                    Speed.Y *= -1;
                }
            }
        }
    }
}
