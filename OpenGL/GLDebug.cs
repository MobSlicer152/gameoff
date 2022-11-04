using System;
using System.Text;
using Silk.NET.OpenGLES;

namespace GameOff.OpenGL
{
    public class GLDebug
    {
        private static GL _gl;

        public static unsafe void Register(GL gl)
        {
            _gl = gl;

            _gl.DebugMessageCallback(LogCallback, null);
        }

        public static string DebugSourceToFriendlyString(GLEnum source)
        {
            string sourceName;

            switch (source)
            {
                case GLEnum.DebugSourceApi:
                    sourceName = "API";
                    break;
                case GLEnum.DebugSourceApplication:
                    sourceName = "application";
                    break;
                case GLEnum.DebugSourceOther:
                    sourceName = "other";
                    break;
                case GLEnum.DebugSourceShaderCompiler:
                    sourceName = "shader compiler";
                    break;
                case GLEnum.DebugSourceThirdParty:
                    sourceName = "third party";
                    break;
                case GLEnum.DebugSourceWindowSystem:
                    sourceName = "window system";
                    break;
                default:
                    sourceName = "unknown";
                    break;
            }

            return sourceName;
        }

        public static string DebugTypeToFriendlyString(GLEnum type)
        {
            string typeName;

            switch (type)
            {
                case GLEnum.DebugTypeDeprecatedBehavior:
                    typeName = "deprecated behaviour";
                    break;
                case GLEnum.DebugTypeError:
                    typeName = "error";
                    break;
                case GLEnum.DebugTypeMarker:
                    typeName = "marker";
                    break;
                case GLEnum.DebugTypeOther:
                    typeName = "other";
                    break;
                case GLEnum.DebugTypePerformance:
                    typeName = "performance";
                    break;
                case GLEnum.DebugTypePopGroup:
                    typeName = "pop group";
                    break;
                case GLEnum.DebugTypePortability:
                    typeName = "portability";
                    break;
                case GLEnum.DebugTypePushGroup:
                    typeName = "push group";
                    break;
                case GLEnum.DebugTypeUndefinedBehavior:
                    typeName = "undefined behaviour";
                    break;
                default:
                    typeName = "unknown";
                    break;
            }

            return typeName;
        }

        public static string DebugSeverityToFriendlyString(GLEnum severity)
        {
            string severityString;

            switch (severity)
            {
                case GLEnum.DebugSeverityHigh:
                    severityString = "high severity";
                    break;
                case GLEnum.DebugSeverityLow:
                    severityString = "low severity";
                    break;
                case GLEnum.DebugSeverityMedium:
                    severityString = "medium severity";
                    break;
                case GLEnum.DebugSeverityNotification:
                    severityString = "";
                    break;
                default:
                    severityString = "unknown severity";
                    break;
            }

            return severityString;
        }

        public static unsafe void LogCallback(GLEnum source, GLEnum type, int id, GLEnum severity, int length, nint message, nint userParam)
        {
            Util.Log($"OpenGL {DebugSourceToFriendlyString(source)} {DebugTypeToFriendlyString(type)} {DebugSeverityToFriendlyString(severity)} message: {Util.StringFromNative((sbyte*)message, length)}");
        }
    }
}
