using Silk.NET.Windowing;
using Silk.NET.Maths;

namespace GameOff
{
    public abstract class RendererBase : IRenderer
    {

        protected IWindow _window;
        protected bool _resized;
        protected bool _unfocused;

        public abstract WindowOptions APIGetWindowOptions();
        public abstract void APIInitialize();
        public abstract void APIBeginFrame();
        public abstract void APIEndFrame();
        public abstract void APIShutdown();

        public void Initialize()
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

        public bool BeginFrame()
        {
            _window.DoEvents();

            APIBeginFrame();

            return !_window.IsClosing;
        }

        public void EndFrame()
        {
            APIEndFrame();
        }

        public void Shutdown()
        {
            APIShutdown();
            _window.Close();
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
