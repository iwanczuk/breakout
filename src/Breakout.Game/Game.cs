namespace Breakout.Game
{
    using System;
    using static SDL2.SDL;

    internal sealed class Game : Breakout.Engine.Game
    {
        private IntPtr window;

        private IntPtr renderer;

        protected override void Render()
        {
            SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);

            SDL_RenderClear(renderer);

            base.Render();

            SDL_RenderPresent(renderer);
        }

        protected override void DrawSquare(int x, int y, int w, int h, Color color)
        {
            SDL_Rect rect;
            rect.x = x;
            rect.y = y;
            rect.w = w;
            rect.h = h;

            switch (color)
            {
                case Color.BLACK:
                    SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);
                    break;

                case Color.RED:
                    SDL_SetRenderDrawColor(renderer, 255, 0, 0, 255);
                    break;

                case Color.WHITE:
                    SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
                    break;

                case Color.GRAY:
                    SDL_SetRenderDrawColor(renderer, 128, 128, 128, 255);
                    break;
            }

            SDL_RenderFillRect(renderer, ref rect);
        }

        protected override uint GetTicks()
        {
            return SDL_GetTicks();
        }

        protected override void Delay(uint ms)
        {
            SDL_Delay(ms);
        }

        protected override void Startup()
        {
            SDL_Init(SDL_INIT_VIDEO);

            window = SDL_CreateWindow
            (
                "Breakout",
                SDL_WINDOWPOS_CENTERED,
                SDL_WINDOWPOS_CENTERED,
                BOARD_WIDTH, BOARD_HEIGHT,
                SDL_WindowFlags.SDL_WINDOW_SHOWN
            );

            renderer = SDL_CreateRenderer(window, 0, SDL_RendererFlags.SDL_RENDERER_SOFTWARE);
        }

        protected override void Shutdown()
        {
            SDL_DestroyRenderer(renderer);

            SDL_DestroyWindow(window);

            SDL_Quit();
        }

        protected override bool IsRunning()
        {
            while (SDL_PollEvent(out SDL_Event e) != 0)
            {
                switch (e.type)
                {
                    case SDL_EventType.SDL_QUIT:
                        return false;

                    case SDL_EventType.SDL_KEYUP:
                        switch (e.key.keysym.sym)
                        {
                            case SDL_Keycode.SDLK_LEFT:
                                IsLeftDown = false;
                                break;

                            case SDL_Keycode.SDLK_RIGHT:
                                IsRightDown = false;
                                break;
                        }
                        break;

                    case SDL_EventType.SDL_KEYDOWN:
                        switch (e.key.keysym.sym)
                        {
                            case SDL_Keycode.SDLK_LEFT:
                                IsLeftDown = true;
                                break;

                            case SDL_Keycode.SDLK_RIGHT:
                                IsRightDown = true;
                                break;
                        }
                        break;
                }
            }

            return true;
        }
    }
}
