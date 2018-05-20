namespace Breakout.Engine
{
    static class ActorFactory
    {
        public static Actor CreateBoard(Game game, Point position, Point size)
        {
            Actor actor = new Actor(game);

            actor.AddComponent
            (
                () => new PhysicsActorComponent(position, size)
            );

            return actor;
        }

        public static Actor CreatePaddle(Game game, Point position, Point size)
        {
            Actor actor = new Actor(game);

            actor.AddComponent
            (
                () => new PhysicsActorComponent(position, size)
            );

            actor.AddComponent
            (
                () => new PaddleActorComponent()
            );

            return actor;
        }

        public static Actor CreateBall(Game game, Point position, Point size)
        {
            Actor actor = new Actor(game);

            actor.AddComponent
            (
                () => new PhysicsActorComponent(position, size)
            );

            actor.AddComponent
            (
                () => new BallActorComponent(new Point(-9, -12))
            );

            return actor;
        }

        public static Actor CreateBlock(Game game, Point position, Point size)
        {
            Actor actor = new Actor(game);

            actor.AddComponent
            (
                () => new PhysicsActorComponent(position, size)
            );

            actor.AddComponent
            (
                () => new BlockActorComponent()
            );

            return actor;
        }
    }
}
