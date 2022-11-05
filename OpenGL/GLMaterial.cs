using Silk.NET.OpenGLES;

namespace GameOff.OpenGL
{
    public class GLMaterial : Material
    {
        private GL _gl;

        public GLMaterial(GL gl, GLTexture texture, GLShader shader)
        {
            _gl = gl;
            Texture = texture;
            Shader = shader;
        }

        public override void Dispose()
        {
            Texture.Dispose();
            Shader.Dispose();
        }
    }
}
