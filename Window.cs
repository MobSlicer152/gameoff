using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static SDL2.SDL;
using static SDL2.SDL.SDL_EventType;
using static SDL2.SDL.SDL_WindowEventID;

namespace GameOff
{
    struct Window
    {
        public string Title { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public WindowFlags Flags { get; private set; }
        public uint ID { get; private set; }

        private IntPtr _handle;
        private bool _closeRequested;

        public Window(string title, int width, int height, WindowFlags flags)
        {
            Title = title;
            Width = width;
            Height = height;
            Flags = flags;

            Util.Log($"Creating {this}");

            _handle = SDL_CreateWindow(Title, SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED,
                Width, Height, (SDL_WindowFlags)Flags);
            if (_handle == IntPtr.Zero)
                throw new Exception($"Failed to create {this}: {SDL_GetError()}");

            ID = SDL_GetWindowID(_handle);

            Util.Log($"Window successfully created (handle {_handle}, ID {ID})");
        }

        public void Close()
        {
            Util.Log($"Destroying {this}");
            SDL_DestroyWindow(_handle);
        }

        public override string ToString()
        {
            return $"{Width}x{Height} window titled {Title}";
        }

        public enum WindowFlags : uint
        {
            Fullscreen = SDL_WindowFlags.SDL_WINDOW_FULLSCREEN,
            OpenGL = SDL_WindowFlags.SDL_WINDOW_OPENGL,
            Shown = SDL_WindowFlags.SDL_WINDOW_SHOWN,
            Hidden = SDL_WindowFlags.SDL_WINDOW_HIDDEN,
            Borderless = SDL_WindowFlags.SDL_WINDOW_BORDERLESS,
            Resizable = SDL_WindowFlags.SDL_WINDOW_RESIZABLE,
            Minimised = SDL_WindowFlags.SDL_WINDOW_MINIMIZED,
            Maximised = SDL_WindowFlags.SDL_WINDOW_MAXIMIZED,
            InputGrabbed = SDL_WindowFlags.SDL_WINDOW_INPUT_GRABBED,
            InputFocus = SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS,
            MouseFocus = SDL_WindowFlags.SDL_WINDOW_MOUSE_FOCUS,
            FullscreenDesktop = SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP,
            Foreign = SDL_WindowFlags.SDL_WINDOW_FOREIGN,
            HighDPI = SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI,
            MouseCapture = SDL_WindowFlags.SDL_WINDOW_MOUSE_CAPTURE,
            AlwaysOnTop = SDL_WindowFlags.SDL_WINDOW_ALWAYS_ON_TOP,
            SkipTaskbar = SDL_WindowFlags.SDL_WINDOW_SKIP_TASKBAR,
            Utility = SDL_WindowFlags.SDL_WINDOW_UTILITY,
            Tooltip = SDL_WindowFlags.SDL_WINDOW_TOOLTIP,
            PopupMenu = SDL_WindowFlags.SDL_WINDOW_POPUP_MENU,
            Vulkan = SDL_WindowFlags.SDL_WINDOW_VULKAN,
            Metal = SDL_WindowFlags.SDL_WINDOW_METAL,
        }
    }
}
