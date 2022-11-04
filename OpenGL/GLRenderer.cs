using System.Numerics;
using Silk.NET.Windowing;
using Silk.NET.OpenGLES;

namespace GameOff.OpenGL
{
    public sealed class GLRenderer : RendererBase
    {
        private static readonly Lazy<GLRenderer> _lazy = new Lazy<GLRenderer>(() => new GLRenderer());
        public static GLRenderer Instance { get { return _lazy.Value; } }
        private GLRenderer() { }

        private GL _gl;

        public override WindowOptions APIGetWindowOptions()
        {
            WindowOptions options = WindowOptions.Default;
            options.API = new GraphicsAPI(ContextAPI.OpenGLES, new APIVersion(3, 2));
            return options;
        }

        public override void APIInitialize()
        {
            Util.Log("Initializing OpenGL ES renderer");

            _window.MakeCurrent();
            _gl = GL.GetApi(_window);
            _gl.Enable(GLEnum.DepthTest);
            _gl.DepthFunc(GLEnum.Less);

#if DEBUG
            GLDebug.Register(_gl);
#endif

            Util.Log($"{_gl.GetStringS(GLEnum.Vendor)} {_gl.GetStringS(GLEnum.Version)}");
            Util.Log($"Render device is {_gl.GetStringS(GLEnum.Renderer)}");

            Util.Log("Renderer initialization succeeded");
        }

        public override void APIBeginFrame()
        {
            _gl.Viewport(0, 0, (uint)_window.Size.X, (uint)_window.Size.Y);
            _gl.ClearColor(0, 0, 0, 0);
            _gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public override void APIEndFrame()
        {
            _window.SwapBuffers();
        }

        public override void APIShutdown()
        {
            Util.Log("Shutting down OpenGL ES renderer");
        }
    }
}
