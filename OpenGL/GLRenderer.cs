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
        // TODO: this is temporary and should be replaced
        private GLShader _defaultShader;

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

            _defaultShader = new GLShader(_gl, "OpenGL/Shaders/colour.vert", "OpenGL/Shaders/colour.frag");

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

        public override Texture APICreateTexture(string path, bool interpolate)
        {
            return new GLTexture(_gl, path, interpolate);
        }

        public override Shader APICreateShader(string vertexPath, string fragmentPath)
        {
            return new GLShader(_gl, vertexPath, fragmentPath);
        }

        public override Material APICreateMaterial(Texture texture, Shader shader)
        {
            if (!(texture is GLTexture))
                throw new InvalidDataException("Cannot create OpenGL material with non-OpenGl texture");
            if (!(shader is Shader))
                throw new InvalidDataException("Cannot create OpenGL material with non-OpenGL shader");

            return new GLMaterial(_gl, (GLTexture)texture, (GLShader)shader);
        }

        public override Model APICreateModel(float[] vertices, uint[] indices, VertexFormat format, Material material)
        {
            if (!(material is GLMaterial))
                throw new InvalidDataException("Cannot create OpenGL model with non-OpenGL material");
            return new GLModel(_gl, vertices, indices, format, (GLMaterial)material);
        }
    }
}
