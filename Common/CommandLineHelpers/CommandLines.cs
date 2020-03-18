using System;
using System.Runtime.InteropServices;

namespace CommandLineHelpers
{
    public class CommandLines
    {
        private const int LF_FACESIZE = 32;

        private const int STD_OUTPUT_HANDLE = -11;

        private const int TMPF_TRUETYPE = 4;

        private static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        public static void StartWindow()
        {
            AllocConsole();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        public static void SetFont(string fontName = "Lucida Console", int fontSize = 700)
        {
            unsafe
            {
                IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);
                if (hnd != INVALID_HANDLE_VALUE)
                {
                    // Set console font to Lucida Console.
                    CONSOLE_FONT_INFO_EX newInfo = new CONSOLE_FONT_INFO_EX();
                    newInfo.cbSize = (uint)Marshal.SizeOf(newInfo);
                    newInfo.FontFamily = TMPF_TRUETYPE;
                    IntPtr ptr = new IntPtr(newInfo.FaceName);
                    Marshal.Copy(fontName.ToCharArray(), 0, ptr, fontName.Length);

                    // Get some settings from current font.

                    newInfo.dwFontSize = new COORD(newInfo.dwFontSize.X, newInfo.dwFontSize.Y);
                    newInfo.FontWeight = fontSize;
                    SetCurrentConsoleFontEx(hnd, false, ref newInfo);
                }
            }
        }

        public static void SetFontColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public static void SetWindowSize(int width, int height)
        {
            Console.SetWindowSize(width, height);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int dwType);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int SetConsoleFont(
            IntPtr hOut,
            uint dwFontNum
            );

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetCurrentConsoleFontEx(
            IntPtr consoleOutput,
            bool maximumWindow,
            ref CONSOLE_FONT_INFO_EX consoleCurrentFontEx);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal unsafe struct CONSOLE_FONT_INFO_EX
        {
            internal uint cbSize;
            internal uint nFont;
            internal COORD dwFontSize;
            internal int FontFamily;
            internal int FontWeight;
            internal fixed char FaceName[LF_FACESIZE];
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct COORD
        {
            internal short X;
            internal short Y;

            internal COORD(short x, short y)
            {
                X = x;
                Y = y;
            }
        }
    }
}