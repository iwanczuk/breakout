namespace Breakout.Engine
{
    using System;
    using System.Collections.Generic;

    public abstract class Game
    {
        public enum Color
        {
            BLACK,
            RED,
            WHITE,
            GRAY
        }

        public static readonly int BOARD_WIDTH = 600;
        public static readonly int BOARD_HEIGHT = 600;

        public static readonly int PADDLE_WIDTH = 160;
        public static readonly int PADDLE_HEIGHT = 30;

        public static readonly int BALL_WIDTH = 20;
        public static readonly int BALL_HEIGHT = 20;

        public static readonly int BLOCK_WIDTH = 30;
        public static readonly int BLOCK_HEIGHT = 30;

        public Actor Board { get; private set; }
        public Actor Ball { get; private set; }
        public Actor Paddle { get; private set; }
        public List<Actor> Blocks { get; private set; }

        public bool IsLeftDown { get; set; }

        public bool IsRightDown { get; set; }

        public int Run()
        {
            try
            {
                Startup();

                Reset();

                try
                {
                    uint fps = 1000 / 30;
                    uint accumulator = 0, deltaTime = 0, newTime = 0;
                    uint lastTime = GetTicks();

                    while (IsRunning())
                    {
                        newTime = GetTicks();
                        deltaTime = newTime - lastTime;

                        if (deltaTime > 0)
                        {
                            lastTime = newTime;

                            accumulator += deltaTime;

                            if (accumulator > 1000)
                            {
                                accumulator = 1000;
                            }

                            if (accumulator >= fps)
                            {
                                while (accumulator >= fps)
                                {
                                    Update();

                                    accumulator -= fps;
                                }

                                Render();
                            }
                        }

                        Delay(fps - accumulator);
                    }
                }
                catch (Exception)
                {
                    return 1;
                }
                finally
                {
                    Shutdown();
                }
            }
            catch (Exception)
            {
                return 1;
            }

            return 0;
        }

        public virtual void Reset()
        {
            Board = ActorFactory.CreateBoard
            (
                this,
                new Point(BOARD_WIDTH / 2, BOARD_HEIGHT / 2),
                new Point(BOARD_WIDTH, BOARD_HEIGHT)
            );

            Ball = ActorFactory.CreateBall
            (
                this,
                new Point(BOARD_WIDTH / 2, BOARD_HEIGHT / 2),
                new Point(BALL_WIDTH, BALL_HEIGHT)
            );

            Paddle = ActorFactory.CreatePaddle
            (
                this,
                new Point(BOARD_WIDTH / 2, BOARD_HEIGHT - PADDLE_HEIGHT / 2),
                new Point(PADDLE_WIDTH, PADDLE_HEIGHT)
            );

            Blocks = new List<Actor>();

            for (int column = 0; column < BOARD_WIDTH / BLOCK_WIDTH; column++)
            {
                for (int row = 0; row < BOARD_HEIGHT / BLOCK_HEIGHT / 3; row++)
                {
                    Blocks.Add
                    (
                        ActorFactory.CreateBlock
                        (
                            this,
                            new Point(BLOCK_WIDTH * column + BLOCK_WIDTH / 2, BLOCK_HEIGHT * row + BLOCK_HEIGHT / 2),
                            new Point(BLOCK_WIDTH, BLOCK_HEIGHT)
                        )
                    );
                }
            }
        }

        public bool InCollision(Actor actor1, Actor actor2)
        {
            PhysicsActorComponent actor1Physics = actor1.GetComponent<PhysicsActorComponent>();
            PhysicsActorComponent actor2Physics = actor2.GetComponent<PhysicsActorComponent>();

            if (actor1Physics.Position.X - actor1Physics.Size.W / 2 > actor2Physics.Position.X + actor2Physics.Size.W / 2)
            {
                return false;
            }

            if (actor1Physics.Position.X + actor1Physics.Size.W / 2 < actor2Physics.Position.X - actor2Physics.Size.W / 2)
            {
                return false;
            }

            if (actor1Physics.Position.Y - actor1Physics.Size.H / 2 > actor2Physics.Position.Y + actor2Physics.Size.H / 2)
            {
                return false;
            }

            if (actor1Physics.Position.Y + actor1Physics.Size.H / 2 < actor2Physics.Position.Y - actor2Physics.Size.H / 2)
            {
                return false;
            }

            return true;
        }

        protected virtual void Update()
        {
            Board.Update();
            Ball.Update();
            Paddle.Update();

            foreach (Actor block in Blocks)
            {
                block.Update();
            }

            if (!Ball.GetComponent<BallActorComponent>().IsAlive)
            {
                Reset();
            }

            bool blocksLeft = false;

            foreach (Actor block in Blocks)
            {
                if (block.GetComponent<BlockActorComponent>().IsAlive)
                {
                    blocksLeft = true;

                    break;
                }
            }

            if (!blocksLeft)
            {
                Reset();
            }
        }

        protected virtual void Render()
        {
            Draw(Board, Color.BLACK);

            foreach (Actor block in Blocks)
            {
                if (!block.GetComponent<BlockActorComponent>().IsAlive)
                {
                    continue;
                }

                Draw(block, Color.GRAY);
            }

            Draw(Paddle, Color.WHITE);
            Draw(Ball, Color.RED);
        }

        protected virtual void Draw(Actor actor, Color color)
        {
            PhysicsActorComponent physics = actor.GetComponent<PhysicsActorComponent>();

            DrawSquare
            (
                physics.Position.X - physics.Size.X / 2,
                physics.Position.Y - physics.Size.Y / 2,
                physics.Size.X,
                physics.Size.Y,
                color
            );
        }

        protected abstract void DrawSquare(int x, int y, int w, int h, Color color);

        protected abstract uint GetTicks();

        protected abstract void Delay(uint ms);

        protected abstract void Startup();

        protected abstract void Shutdown();

        protected abstract bool IsRunning();
    }
}
