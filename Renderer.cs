using GameOff.OpenGL;

namespace GameOff
{
    public enum RendererType
    {
        None,
        GL
    }

    public abstract class Renderer
    {
        /// <summary>
        /// Initialize the render backend
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Set up to record commands
        /// </summary>
        public abstract bool BeginFrame();
        
        /// <summary>
        /// Submit commands
        /// </summary>
        public abstract void EndFrame();

        /// <summary>
        /// Clean up
        /// </summary>
        public abstract void Shutdown();

        public abstract Texture CreateTexture(string path, bool interpolate);
        public abstract Shader CreateShader(string vertexPath, string fragmentPath);
        public abstract Material CreateMaterial(Texture texture, Shader shader);
        public abstract Model CreateModel(float[] vertices, uint[] indices, VertexFormat format, Material material);
        
        public static RendererType GetRendererType(Renderer? renderer)
        {
            if (renderer == null)
                return RendererType.None;
            else if (renderer is GLRenderer)
                return RendererType.GL;
            else
                return RendererType.None;
        }
    }
}
