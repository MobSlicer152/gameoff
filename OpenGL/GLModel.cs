using Silk.NET.OpenGLES;

namespace GameOff.OpenGL
{
    public sealed class GLModel : Model
    {
        private GL _gl;
        private Material _material;
        private GLBufferObject<float> _vbo;
        private GLBufferObject<uint> _ebo;
        private GLVertexArrayObject<float, uint> _vao;
        private uint _vertexCount;
        private uint _indexCount;
        private bool _textured;

        public GLModel(GL gl, float[] vertices, uint[] indices, VertexFormat format, GLMaterial material)
        {
            _gl = gl;
            _material = material;
            
            _vbo = new GLBufferObject<float>(_gl, vertices, BufferTargetARB.ArrayBuffer);
            _ebo = new GLBufferObject<uint>(_gl, indices, BufferTargetARB.ElementArrayBuffer);
            _vao = new GLVertexArrayObject<float, uint>(_gl, _vbo, _ebo);

            _vertexCount = (uint)vertices.Length;
            _indexCount = (uint)indices.Length;

            _vao.Bind();
            if (format == VertexFormat.PositionColour)
            {
                _vao.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, 7, 0);
                _vao.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, 7, 3);
                _textured = false;
            }
            else if (format == VertexFormat.PositionUV)
            {
                _vao.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, 6, 0);
                _vao.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, 6, 3);
                _textured = true;
            }
        }

        public override void Draw()
        {
            if (_textured)
                _material.Texture.Use();
            _material.Shader.Use();
            _vao.Bind();

            unsafe
            {
                _gl.DrawElements(PrimitiveType.Triangles, _indexCount, DrawElementsType.UnsignedInt, null);
            }
        }

        public override void DrawWireframe(float lineThickness)
        {
            _material.Shader.Use();
            _vao.Bind();
            _gl.LineWidth(lineThickness);

            unsafe
            {
                _gl.DrawElements(PrimitiveType.LineLoop, _indexCount, DrawElementsType.UnsignedInt, null);
            }
        }

        public override void Dispose()
        {
            _vao.Dispose();
            _ebo.Dispose();
            _vbo.Dispose();
        }
    }
}
