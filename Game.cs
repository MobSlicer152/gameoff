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
        public const int WIDTH = 576;
        public const int HEIGHT = 576;

        public bool Running;

        private Renderer _renderer;

        public void Run()
        {
            Model model;
            Material material;

            float[] vertices =
            {
                -0.5f,  0.5f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
                 0.5f,  0.5f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f,
                 0.5f, -0.5f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f,
                -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f, 0.0f
            };

            uint[] indices =
            {
                0, 1, 3,
                1, 2, 3
            };

            Util.Log("Initializing game");

            // TODO: hardcoding L bad
            _renderer = GLRenderer.Instance;
            _renderer.Initialize();

            material = _renderer.CreateMaterial(
                _renderer.CreateTexture("Textures/diamond_ore.png", false),
                _renderer.CreateShader("OpenGL/Shaders/colouruv.vert", "OpenGL/Shaders/colouruv.frag")
            );
            model = _renderer.CreateModel(vertices, indices, VertexFormat.PositionColourUV, material);

            Util.Log("Entering game loop");
            Running = true;
            while (Running)
            {
                Running = _renderer.BeginFrame();
                if (!Running)
                    break;

                model.Draw();

                _renderer.EndFrame();
            }

            Util.Log("Shutting down game");

            model.Dispose();
            _renderer.Shutdown();
        }
    }
}
