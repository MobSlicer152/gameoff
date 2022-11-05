using Silk.NET.Windowing;
using Silk.NET.Maths;

namespace GameOff
{
    public abstract class RendererBase : Renderer
    {

        protected IWindow _window;
        protected bool _resized;
        protected bool _unfocused;

        public abstract WindowOptions APIGetWindowOptions();
        public abstract void APIInitialize();
        public abstract void APIBeginFrame();
        public abstract void APIEndFrame();
        public abstract void APIShutdown();
        public abstract Texture APICreateTexture(string path);
        public abstract Shader APICreateShader(string vertexPath, string fragmentPath);
        public abstract Material APICreateMaterial(Texture texture, Shader shader);
        public abstract Model APICreateModel(float[] vertices, uint[] indices, VertexFormat format, Material data);

        public override void Initialize()
        {
            WindowOptions options = APIGetWindowOptions();
            options.Title = Game.NAME;
            options.Size = new Vector2D<int>(Game.WIDTH, Game.HEIGHT);
            _window = Window.Create(options);
            _window.Initialize();

            _window.Resize += OnResize;
            _window.FocusChanged += OnFocusChange;

            APIInitialize();
        }

        public override bool BeginFrame()
        {
            _window.DoEvents();

            APIBeginFrame();

            return !_window.IsClosing;
        }

        public override void EndFrame()
        {
            APIEndFrame();
        }

        public override void Shutdown()
        {
            APIShutdown();
            _window.Close();
        }

        public override Texture CreateTexture(string path)
        {
            return APICreateTexture(path);
        }

        public override Shader CreateShader(string vertexPath, string fragmentPath)
        {
            return APICreateShader(vertexPath, fragmentPath);
        }

        public override Material CreateMaterial(Texture texture, Shader shader)
        {
            return APICreateMaterial(texture, shader);
        }

        public override Model CreateModel(float[] vertices, uint[] indices, VertexFormat format, Material material)
        {
            return APICreateModel(vertices, indices, format, material);
        }

        protected void OnResize(Vector2D<int> newSize)
        {
            Util.Log($"Render window resized to {newSize.X}x{newSize.Y}");
            _resized = true;
        }

        protected void OnFocusChange(bool newFocus)
        {
            string focusString = newFocus ? "gained" : "lost";
            Util.Log($"Render window focus {focusString}");
            _unfocused = !newFocus;
        }
    }
}
