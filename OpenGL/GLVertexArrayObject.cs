using Silk.NET.OpenGLES;

namespace GameOff.OpenGL
{
    public class GLVertexArrayObject<TV, TI> : IDisposable
        where TV : unmanaged
        where TI : unmanaged
    {
        private uint _handle;
        private GL _gl;

        public GLVertexArrayObject(GL gl, GLBufferObject<TV> vbo, GLBufferObject<TI> ebo)
        {
            _gl = gl;

            _handle = _gl.GenVertexArray();
            Bind();
            vbo.Bind();
            ebo.Bind();
        }

        public unsafe void VertexAttribPointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offset)
        {
            _gl.VertexAttribPointer(index, count, type, false, vertexSize * (uint)sizeof(TV), (void*)(offset * sizeof(TV)));
            _gl.EnableVertexAttribArray(index);
        }

        public void Bind()
        {
            _gl.BindVertexArray(_handle);
        }

        public void Dispose()
        {
            _gl.DeleteVertexArray(_handle);
        }
    }
}
