using System.Runtime.InteropServices;

namespace LenovoBacklightImproved
{
    internal struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }
    internal static class SystemIdle
    {
        private static bool initialized = false;
        private static LASTINPUTINFO lastInPut;

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        private static void initialize()
        {
            lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);

            initialized = true;
        }
        public static uint getIdleTime()
        {
            if (!initialized)
            {
                initialize();
            }

            GetLastInputInfo(ref lastInPut);
            return (uint)Environment.TickCount - lastInPut.dwTime;
        }

        public static void makeNotIdle()
        {
            const int MOUSEEVENTF_MOVE = 0x0001;

            mouse_event(MOUSEEVENTF_MOVE, 0, 0, 0, 0);
        }
    }
}
