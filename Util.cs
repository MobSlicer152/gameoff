using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace gameoff
{
    class Util
    {
        public static void Log(object? obj, [CallerFilePath] string sourcePath = "<unknown>",
            [CallerLineNumber] int sourceLine = -1)
        {
#if DEBUG
            MethodBase info = new StackTrace().GetFrame(1).GetMethod();
            string debugOnly = $"{info.ReflectedType.FullName}.{info.Name}/";
#else
            string debugOnly = "";
#endif
            Trace.WriteLine($"[{DateTime.Now}] ({Thread.CurrentThread.Name}/{debugOnly}{sourcePath}:{sourceLine}): {obj}");
        }
    }
}
