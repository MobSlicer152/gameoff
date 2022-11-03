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

        float[] quadVertices =
        {
             0.5f,  0.5f, 0.0f,
             0.5f, -0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            -0.5f,  0.5f, 0.0f
        };
        uint[] quadIndices =
        {
             0, 1, 3,
             1, 2, 3
        };
        GLBufferObject<float> quadVBO;
        GLBufferObject<uint> quadEBO;
        GLVertexArrayObject<float, uint> quadVAO;
        GLShader shader;

        public override WindowOptions APIGetWindowOptions()
        {
            WindowOptions options = WindowOptions.Default;
            options.API = new GraphicsAPI(ContextAPI.OpenGLES, new APIVersion(3, 2));
            return options;
        }

        public override void APIInitialize()
        {
            _gl = GL.GetApi(_window);
            _gl.Enable(GLEnum.DepthTest);
            _gl.DepthFunc(GLEnum.Less);
            Util.Log($"{_gl.GetStringS(GLEnum.Vendor)} OpenGL {_gl.GetStringS(GLEnum.Version)}");
            Util.Log($"Render device is {_gl.GetStringS(GLEnum.Renderer)}");
            quadVBO = new GLBufferObject<float>(_gl, quadVertices, BufferTargetARB.ArrayBuffer);
            quadEBO = new GLBufferObject<uint>(_gl, quadIndices, BufferTargetARB.ArrayBuffer);
            quadVAO = new GLVertexArrayObject<float, uint>(_gl, quadVBO, quadEBO);
            shader = new GLShader(_gl, "shader.vert", "shader.frag");

            quadVAO.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, 7, 0);
            quadVAO.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, 7, 3);
        }

        public override void APIBeginFrame()
        {
            _window.MakeCurrent();
            _gl.ClearColor(0, 0, 0, 0);
            _gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            quadVAO.Bind();
            unsafe
            {
                _gl.DrawElements(PrimitiveType.Triangles, (uint)quadIndices.Length, DrawElementsType.UnsignedInt, null);
            }
        }

        public override void APIEndFrame()
        {
            _window.SwapBuffers();
        }

        public override void APIShutdown()
        {
            quadVBO.Dispose();
            quadEBO.Dispose();
            quadVAO.Dispose();
            shader.Dispose();
        }
    }
}
