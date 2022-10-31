using System.Diagnostics;

using static SDL2.SDL;
using static SDL2.SDL.SDL_WindowFlags;
using static SDL2.SDL.SDL_EventType;
using static SDL2.SDL.SDL_WindowEventID;
using gameoff;

namespace GameOff
{
    class SDLManager
    {
        public static void Init()
        {
            Util.Log("Initializing SDL");
            SDL_Init(SDL_INIT_VIDEO);
        }

        public static void Shutdown()
        {
            Util.Log("Shutting down SDL");
            SDL_Quit();
        }
    }
}
