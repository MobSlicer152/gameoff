using Silk.NET.OpenGLES;
using ImageMagick;

namespace GameOff.OpenGL
{
    public class GLTexture : Texture
    {
        private GL _gl;
        private uint _handle;

        public GLTexture(GL gl, string path, bool interpolate)
        {
            _gl = gl;

            _gl.GenTextures(1, out _handle);

            Use();
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            if (interpolate)
            {
                _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
                _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            }
            else
            {
                _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.NearestMipmapNearest);
                _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            }

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
            Image.Flip();
            Span<byte> raw = new Span<byte>(Image.GetPixels().ToByteArray(PixelMapping.RGBA));
            Use();
            _gl.TexImage2D<byte>(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)Image.Width, (uint)Image.Height, 0, GLEnum.Rgba, GLEnum.UnsignedByte, raw);
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
