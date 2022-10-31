using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameOff
{
    class Util
    {
        public static void Log(object? obj, [CallerFilePath] string sourcePath = "<unknown>",
            [CallerLineNumber] int sourceLine = -1)
        {
            string debugOnly = "";
#if DEBUG
            try
            {
                MethodBase info = new StackTrace().GetFrame(1).GetMethod();
                debugOnly = $"{info.ReflectedType.FullName}.{info.Name}/";
            }
            catch (Exception) { }
#endif
            Trace.WriteLine($"[{DateTime.Now}] ({Thread.CurrentThread.Name}/{debugOnly}{sourcePath}:{sourceLine}): {obj}");
        }
    }
}
