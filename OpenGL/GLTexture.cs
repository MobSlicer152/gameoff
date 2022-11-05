using Silk.NET.OpenGLES;
using ImageMagick;

namespace GameOff.OpenGL
{
    public class GLTexture : Texture
    {
        private GL _gl;
        private uint _handle;

        public GLTexture(GL gl, string path)
        {
            _gl = gl;

            _gl.GenTextures(1, out _handle);
            Use();
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Image = new MagickImage();
            try
            {
                Image.Read(path);
            }
            catch (Exception e)
            {
                Util.Log($"Failed to load image {path}: {e.Message}");
                Util.Log("Using default texture");
                Image = GetDefault();
            }
            Update();
        }

        public unsafe override void Update()
        {
            Image.TransformColorSpace(ColorProfile.SRGB);
            Use();
            _gl.TexImage2D<ushort>(TextureTarget.Texture2D, 0, (int)GLEnum.Srgb, (uint)Image.Width, (uint)Image.Height, 0, GLEnum.Srgb, GLEnum.UnsignedShort, Image.GetPixels().GetValues());
            _gl.GenerateMipmap(TextureTarget.Texture2D);
        }

        public override void Use()
        {
            _gl.BindTexture(GLEnum.Texture2D, _handle);
        }

        public override void Dispose()
        {
            _gl.DeleteTexture(_handle);
            Image.Dispose();
        }
    }
}
