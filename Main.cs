using System.Diagnostics;

namespace GameOff
{
    class Program
    {
        public const string LOG_NAME = "gameoff.log";

        static void Main(string[] argv)
        {
            var consoleTracer = new ConsoleTraceListener();
            var fileTracer = new TextWriterTraceListener(File.OpenWrite(LOG_NAME));

            Trace.Listeners.Add(consoleTracer);
            Trace.Listeners.Add(fileTracer);

            Thread.CurrentThread.Name = "Main";

            Util.Log("Initializing game");

            SDLManager.Init();
            SDLManager.Shutdown();

            Util.Log("Exiting");

            Trace.Close();
        }
    }
}
