using GameOff.OpenGL;

namespace GameOff
{
    public enum RendererType
    {
        None,
        GL
    }

    public interface IRenderer
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

        public static RendererType GetRendererType(IRenderer? renderer)
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
