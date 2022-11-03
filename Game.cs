using GameOff.OpenGL;

namespace GameOff
{
    public sealed class Game
    {
        private static readonly Lazy<Game> _lazy = new Lazy<Game>(() => new Game());
        public static Game Instance { get { return _lazy.Value; } }
        private Game() { }

        public const string NAME = "GameOff";

        // TODO: add to settings eventually
        public const int WIDTH = 1024;
        public const int HEIGHT = 576;

        public bool Running;

        private IRenderer _renderer;

        public void Run()
        {
            Util.Log("Initializing game");

            // TODO: hardcoding L bad
            _renderer = GLRenderer.Instance;
            _renderer.Initialize();

            Util.Log("Entering game loop");
            Running = true;
            while (Running)
            {
                Running = _renderer.BeginFrame();
                if (!Running)
                    break;
                _renderer.EndFrame();
            }

            Util.Log("Shutting down game");

            _renderer.Shutdown();
        }
    }
}
