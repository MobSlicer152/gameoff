using static SDL2.SDL;

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
