using ImageMagick;

namespace GameOff
{
    public abstract class Texture : IDisposable
    {
        public MagickImage Image;
        private static MagickImage? _default;

        public abstract void Update();
        public abstract void Use();
        public abstract void Dispose();

        protected static MagickImage GetDefault()
        {
            if (_default == null)
            {
                MagickImage magenta = new MagickImage(new MagickColor(255, 0, 255), 32, 32);
                MagickGeometry geometry = new MagickGeometry(32, 32);
                _default = new MagickImage(new MagickColor(0, 0, 0), 64, 64);
                _default.CopyPixels(magenta, geometry, 0, 0);
                _default.CopyPixels(magenta, geometry, 32, 32);
                magenta.Dispose();
            }

            return _default;
        }
    }
}
