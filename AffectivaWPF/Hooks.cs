using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace AffdexMe
{
    public class WmCommand
    {
        public const int WM_APPCOMMAND = 0x319;
        public const int WM_COMMAND = 0x111;
        public const int MIN_ALL = 419;
        public const int MIN_ALL_UNDO = 416;
    }
    public class AppCommand
    {
        public const int APPCOMMAND_BROWSER_BACKWARD = 1;
        public const int APPCOMMAND_BROWSER_FORWARD = 2;
        public const int APPCOMMAND_BROWSER_REFRESH = 3;
        public const int APPCOMMAND_BROWSER_STOP = 4;
        public const int APPCOMMAND_BROWSER_SEARCH = 5;
        public const int APPCOMMAND_BROWSER_FAVORITES = 6;
        public const int APPCOMMAND_BROWSER_HOME = 7;
        public const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        public const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        public const int APPCOMMAND_VOLUME_UP = 0xA0000;
        public const int APPCOMMAND_MEDIA_NEXTTRACK = 11;
        public const int APPCOMMAND_MEDIA_PREVIOUSTRACK = 12;
        public const int APPCOMMAND_MEDIA_STOP = 13;
        public const int APPCOMMAND_MEDIA_PLAY_PAUSE = 14;
        public const int APPCOMMAND_LAUNCH_MAIL = 15;
        public const int APPCOMMAND_LAUNCH_MEDIA_SELECT = 16;
        public const int APPCOMMAND_LAUNCH_APP1 = 17;
        public const int APPCOMMAND_LAUNCH_APP2 = 18;
        public const int APPCOMMAND_BASS_DOWN = 19;
        public const int APPCOMMAND_BASS_BOOST = 20;
        public const int APPCOMMAND_BASS_UP = 21;
        public const int APPCOMMAND_TREBLE_DOWN = 22;
        public const int APPCOMMAND_TREBLE_UP = 23;
        public const int APPCOMMAND_MICROPHONE_VOLUME_MUTE = 24;
        public const int APPCOMMAND_MICROPHONE_VOLUME_DOWN = 25;
        public const int APPCOMMAND_MICROPHONE_VOLUME_UP = 26;
        public const int APPCOMMAND_HELP = 27;
        public const int APPCOMMAND_FIND = 28;
        public const int APPCOMMAND_NEW = 29;
        public const int APPCOMMAND_OPEN = 30;
        public const int APPCOMMAND_CLOSE = 31;
        public const int APPCOMMAND_SAVE = 32;
        public const int APPCOMMAND_PRINT = 33;
        public const int APPCOMMAND_UNDO = 34;
        public const int APPCOMMAND_REDO = 35;
        public const int APPCOMMAND_COPY = 36;
        public const int APPCOMMAND_CUT = 37;
        public const int APPCOMMAND_PASTE = 38;
        public const int APPCOMMAND_REPLY_TO_MAIL = 39;
        public const int APPCOMMAND_FORWARD_MAIL = 40;
        public const int APPCOMMAND_SEND_MAIL = 41;
        public const int APPCOMMAND_SPELL_CHECK = 42;
        public const int APPCOMMAND_DICTATE_OR_COMMAND_CONTROL_TOGGLE = 43;
        public const int APPCOMMAND_MIC_ON_OFF_TOGGLE = 44;
        public const int APPCOMMAND_CORRECTION_LIST = 45;
        public const int APPCOMMAND_MEDIA_PLAY = 46;
        public const int APPCOMMAND_MEDIA_PAUSE = 47;
        public const int APPCOMMAND_MEDIA_RECORD = 48;
        public const int APPCOMMAND_MEDIA_FAST_FORWARD = 49;
        public const int APPCOMMAND_MEDIA_REWIND = 50;
        public const int APPCOMMAND_MEDIA_CHANNEL_UP = 51;
        public const int APPCOMMAND_MEDIA_CHANNEL_DOWN = 52;
        public const int APPCOMMAND_DELETE = 53;
        public const int APPCOMMAND_DWM_FLIP3D = 54;
    }

    // Hook Types
    public enum HookType : int
    {
        WH_JOURNALRECORD = 0,
        WH_JOURNALPLAYBACK = 1,
        WH_KEYBOARD = 2,
        WH_GETMESSAGE = 3,
        WH_CALLWNDPROC = 4,
        WH_CBT = 5,
        WH_SYSMSGFILTER = 6,
        WH_MOUSE = 7,
        WH_HARDWARE = 8,
        WH_DEBUG = 9,
        WH_SHELL = 10,
        WH_FOREGROUNDIDLE = 11,
        WH_CALLWNDPROCRET = 12,
        WH_KEYBOARD_LL = 13,
        WH_MOUSE_LL = 14
    }

    [Flags]
    public enum SendMessageTimeoutFlags : uint
    {
        SMTO_NORMAL = 0x0,
        SMTO_BLOCK = 0x1,
        SMTO_ABORTIFHUNG = 0x2,
        SMTO_NOTIMEOUTIFNOTHUNG = 0x8
    }



    public class HookActions
    {
        public delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);
        [DllImport("user32.dll")]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate CallbackFunction, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);
        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName); //Find A Window
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam); //Send System Message
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout( IntPtr windowHandle, uint Msg, IntPtr wParam,IntPtr lParam,SendMessageTimeoutFlags flags,uint timeout, out IntPtr result);
        public static void DecreaseVolume()
        {
            System.Console.WriteLine("Increasing volume");
            IntPtr hWnd = FindWindow("SpotifyMainWindow", "Spotify");
            if (hWnd == IntPtr.Zero)
                return;

            uint pID;
            GetWindowThreadProcessId(hWnd, out pID);

            SendMessage(hWnd, WmCommand.WM_APPCOMMAND, hWnd, (IntPtr)AppCommand.APPCOMMAND_VOLUME_DOWN);
        }

        public static void OpenApplication(string appExeName)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = appExeName;
            try
            {
                Process.Start(startInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not open {0}: {1}", appExeName, e.Message);
            }
        }

        public static string TakeScreenshot(string folder)
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            string filename = "none";
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                filename = folder + @"\ScreenShot-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".jpg";
                bitmap.Save(filename, ImageFormat.Jpeg);
            }
            return filename;
        }

        public static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not open {0}: {1}", url, e.Message);
            }
        }

        public static void IncreaseVolume()
        {
            IntPtr hWnd = FindWindow("SpotifyMainWindow", "Spotify");

            if (hWnd == IntPtr.Zero)
                return;

            uint pID;
            GetWindowThreadProcessId(hWnd, out pID);
            SendMessage(hWnd, WmCommand.WM_APPCOMMAND, hWnd, (IntPtr)AppCommand.APPCOMMAND_VOLUME_UP);
        }

        public static void IncreaseSystemVolume(IntPtr handle)
        {
            SendMessage(handle, WmCommand.WM_APPCOMMAND, handle, (IntPtr)AppCommand.APPCOMMAND_VOLUME_UP);
        }

        public static void hideAllWindows()
        {
            IntPtr OutResult;
            IntPtr lHwnd = FindWindow("Shell_TrayWnd", null);
            SendMessageTimeout(lHwnd, WmCommand.WM_COMMAND, (IntPtr)WmCommand.MIN_ALL, IntPtr.Zero, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 2000, out OutResult);
            System.Threading.Thread.Sleep(2000);
            SendMessageTimeout(lHwnd, WmCommand.WM_COMMAND, (IntPtr)WmCommand.MIN_ALL_UNDO, IntPtr.Zero, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 2000, out OutResult);
        }
    }

}