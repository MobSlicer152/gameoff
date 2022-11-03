using Silk.NET.OpenGLES;

namespace GameOff.OpenGL
{
    // Sort of copied from the Silk.NET OpenGL abstraction tutorial

    public class GLBufferObject<T> : IDisposable
        where T : unmanaged
    {
        private uint _handle;
        private BufferTargetARB _type;
        private GL _gl;

        public unsafe GLBufferObject(GL gl, Span<T> data, BufferTargetARB type)
        {
            _gl = gl;
            _type = type;

            _handle = _gl.GenBuffer();
            Bind();
            fixed (void* d = data)
            {
                _gl.BufferData(_type, (nuint)(data.Length * sizeof(T)), d, BufferUsageARB.StaticDraw);
            }
        }

        public void Bind()
        { 
            _gl.BindBuffer(_type, _handle);
        }

        public void Dispose()
        {
            _gl.DeleteBuffer(_handle);
        }
    }
}
