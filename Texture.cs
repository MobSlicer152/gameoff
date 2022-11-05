using ImageMagick;

namespace GameOff
{
    public abstract class Texture : IDisposable
    {
        public MagickImage Image;
        private static MagickImage? _default;
        private static readonly byte[] _defaultBytes =
        {
            0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF,
            0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF
        };

        public abstract void Update();
        public abstract void Use();
        public abstract void Dispose();

        protected static MagickImage GetDefault()
        {
            if (_default == null)
            {
                MagickReadSettings settings = new MagickReadSettings();
                settings.Width = 2;
                settings.Height = 2;
                settings.Format = MagickFormat.Rgba;
                _default = new MagickImage(_defaultBytes, settings);
                _default.Scale(64, 64);
            }

            return _default;
        }
    }
}
