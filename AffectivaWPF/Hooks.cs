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

            SendMessage(hWnd, WMCommand.WM_APPCOMMAND, hWnd, (IntPtr)AppCommand.APPCOMMAND_VOLUME_DOWN);
        }

        public static bool OpenApplication(string appExeName)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = appExeName;
            try
            {
                Process.Start(startInfo);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not open {0}: {1}", appExeName, e.Message);
                return false;
            }
        }

        public static bool TakeScreenshot(string folder, string filename)
        {
            try
            {
                Rectangle bounds = Screen.GetBounds(Point.Empty);
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                    }
                    filename = folder + "\\" +filename;
                    bitmap.Save(filename, ImageFormat.Jpeg);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not open {0}: {1}", url, e.Message);
                return false;
            }
        }

        public static void IncreaseVolume()
        {
            IntPtr hWnd = FindWindow("SpotifyMainWindow", "Spotify");

            if (hWnd == IntPtr.Zero)
                return;

            uint pID;
            GetWindowThreadProcessId(hWnd, out pID);
            SendMessage(hWnd, WMCommand.WM_APPCOMMAND, hWnd, (IntPtr)AppCommand.APPCOMMAND_VOLUME_UP);
        }

        public static void IncreaseSystemVolume(IntPtr handle)
        {
            SendMessage(handle, WMCommand.WM_APPCOMMAND, handle, (IntPtr)AppCommand.APPCOMMAND_VOLUME_UP);
        }

        public static void DecreaseSystemVolume(IntPtr handle)
        {
            SendMessage(handle, WMCommand.WM_APPCOMMAND, handle, (IntPtr)AppCommand.APPCOMMAND_VOLUME_DOWN);
        }

        public static void MuteSystemVolume(IntPtr handle)
        {
            SendMessage(handle, WMCommand.WM_APPCOMMAND, handle, (IntPtr)AppCommand.APPCOMMAND_VOLUME_MUTE);
        }

        public static void SystemMediaStop(IntPtr handle)
        {
            SendMessage(handle, WMCommand.WM_APPCOMMAND, handle, (IntPtr)AppCommand.APPCOMMAND_MEDIA_STOP);
        }

        public static void SystemMediaPause(IntPtr handle)
        {
            SendMessage(handle, WMCommand.WM_APPCOMMAND, handle, (IntPtr)AppCommand.APPCOMMAND_MEDIA_PAUSE);
        }

        public static void SystemMediaPlay(IntPtr handle)
        {
            SendMessage(handle, WMCommand.WM_APPCOMMAND, handle, (IntPtr)AppCommand.APPCOMMAND_MEDIA_PLAY);
        }

        public static void SystemMediaPlayPause(IntPtr handle)
        {
            SendMessage(handle, WMCommand.WM_APPCOMMAND, handle, (IntPtr)AppCommand.APPCOMMAND_MEDIA_PLAY_PAUSE);
        }

        public static void KeyboardPress(IntPtr handle, string key)
        {
            Keyboard.SendKeyDown(key);  
        }

        public static void BrightnessUp(IntPtr handle)
        {
            BrightnessControl.BrightnessUp(handle);
        }

        public static void BrightnessDown(IntPtr handle)
        {
            BrightnessControl.BrightnessDown(handle);
        }

        public static void hideAllWindows()
        {
            IntPtr OutResult;
            IntPtr lHwnd = FindWindow("Shell_TrayWnd", null);
            SendMessageTimeout(lHwnd, WMCommand.WM_COMMAND, (IntPtr)WMCommand.MIN_ALL, IntPtr.Zero, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 2000, out OutResult);
        }
    }

}