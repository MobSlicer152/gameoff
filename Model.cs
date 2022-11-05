using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOff
{
    public abstract class Model : IDisposable
    {
        public abstract void Draw();
        public abstract void DrawWireframe(float lineThickness);
        public abstract void Dispose();
    }
}
