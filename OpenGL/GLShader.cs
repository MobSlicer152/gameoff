using Silk.NET.OpenAL;
using Silk.NET.OpenGLES;
using System.Reflection.Metadata;

namespace GameOff.OpenGL
{
    public class GLShader : IDisposable
    {
        GL _gl;
        private uint _handle;

        public GLShader(GL gl, string vertexPath, string fragmentPath)
        {
            _gl = gl;

            uint vert = LoadShader(ShaderType.VertexShader, vertexPath);
            uint frag = LoadShader(ShaderType.FragmentShader, fragmentPath);

            _handle = _gl.CreateProgram();
            _gl.AttachShader(_handle, vert);
            _gl.AttachShader(_handle, frag);
            _gl.LinkProgram(_handle);
            _gl.GetProgram(_handle, GLEnum.LinkStatus, out var status);
            if (status == 0)
                throw new Exception($"Shader program from {vertexPath} and {fragmentPath} failed to link with status {status}");

            _gl.DetachShader(_handle, frag);
            _gl.DetachShader(_handle, vert);
            _gl.DeleteShader(vert);
            _gl.DeleteShader(frag);

        }

        public void Use()
        {
            _gl.UseProgram(_handle);
        }

        public void Dispose()
        {
            _gl.DeleteProgram(_handle);
        }

        private unsafe uint LoadShader(ShaderType shaderType, string path) 
        {
            uint shader = _gl.CreateShader(shaderType);
            int status = 0;
            string source;

            source = File.ReadAllText(path);

            _gl.ShaderSource(shader, source);
            _gl.CompileShader(shader);

            _gl.GetShader(shader, GLEnum.CompileStatus, out status);
            if (status == 0)
            {
                Util.Log($"Compilation of shader {path} failed:\n{_gl.GetShaderInfoLog(shader)}");
                _gl.DeleteShader(shader);
                return 0;
            }

            return shader;
        }

        public int GetUniform(string name)
        {
            return _gl.GetUniformLocation(_handle, name);
        }
    }
}
