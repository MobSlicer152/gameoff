using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOff
{
	public abstract class Material : IDisposable
	{
		public Texture Texture;
		public Shader Shader;

		public abstract void Dispose();
	}
}
