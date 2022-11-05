namespace GameOff
{
    public abstract class Shader : IDisposable
    {
        public abstract void Use();
        public abstract void Dispose();
    }
}
