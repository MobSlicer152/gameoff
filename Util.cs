using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

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
                string className = info.ReflectedType.Name;
                debugOnly = $"{info.ReflectedType.FullName}." +
                            $"{info.Name.Replace(".ctor", className).
                                Replace(".cctor", className).
                                Replace("Finalize", "~" + className)}/";
            }
            catch (Exception) { }
#endif
            Trace.WriteLine($"[{DateTime.Now}] ({Thread.CurrentThread.Name}/{debugOnly}{sourcePath}:{sourceLine}): {obj}");
        }

        public static unsafe string? StringFromNative(sbyte* source, int length)
        {
            return StringFromNative(source, length, Encoding.ASCII);
        }

        public static unsafe string? StringFromNative(sbyte* source, int length, Encoding encoding)
        {
            return new string(source, 0, length, encoding);
        }
    }
}
