using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;

namespace AffdexMe
{
    class BrightnessControl
    {
        private const int MONITOR_DEFAULTTONEAREST = 2;

        private const int PHYSICAL_MONITOR_DESCRIPTION_SIZE = 128;

        private const int MC_CAPS_BRIGHTNESS = 0x2;
        private const int MC_CAPS_CONTRAST = 0x4;

        [DllImport("user32.dll", SetLastError = true)]
        private extern static bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll", SetLastError = false)]
        private extern static IntPtr MonitorFromPoint(POINT pt, uint dwFlags);

        [DllImport("dxva2.dll", SetLastError = true)]
        private extern static bool GetMonitorBrightness(IntPtr hMonitor, out uint pdwMinimumBrightness, out uint pdwCurrentBrightness, out uint pdwMaximumBrightness);

        [DllImport("dxva2.dll", SetLastError = true)]
        private extern static bool SetMonitorBrightness(IntPtr hMonitor, uint dwNewBrightness);

        [DllImport("dxva2.dll", SetLastError = true)]
        private extern static bool GetPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, uint dwPhysicalMonitorArraySize, [Out] PHYSICAL_MONITOR[] pPhysicalMonitorArray);

        [DllImport("dxva2.dll", SetLastError = true)]
        private extern static bool GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, out uint pdwNumberOfPhysicalMonitors);
        [DllImport("user32.dll")]
        static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);


        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            int x;
            int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PHYSICAL_MONITOR
        {
            public IntPtr hPhysicalMonitor;

            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U2, SizeConst = PHYSICAL_MONITOR_DESCRIPTION_SIZE)]
            public char[] szPhysicalMonitorDescription;
        }

        const int ERROR_GEN_FAILURE = 0x1F;
        private static double currentMonitorBrightness = -1;

        public static PHYSICAL_MONITOR[] GetPhysicalMonitors(IntPtr handle)
        {
            IntPtr monitorHandle = MonitorFromWindow(handle, 0);
            uint dwNumberOfPhysicalMonitors;
            GetNumberOfPhysicalMonitorsFromHMONITOR(monitorHandle, out dwNumberOfPhysicalMonitors);

            PHYSICAL_MONITOR[] physicalMonitorArray = new PHYSICAL_MONITOR[dwNumberOfPhysicalMonitors];
            GetPhysicalMonitorsFromHMONITOR(monitorHandle, dwNumberOfPhysicalMonitors, physicalMonitorArray);
            return physicalMonitorArray;
        }

        public static double GetMonitorBrightness(PHYSICAL_MONITOR physicalMonitor)
        {
            uint dwMinimumBrightness, dwCurrentBrightness, dwMaximumBrightness;
            if (!GetMonitorBrightness(physicalMonitor.hPhysicalMonitor, out dwMinimumBrightness, out dwCurrentBrightness, out dwMaximumBrightness))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return (double)(dwCurrentBrightness - dwMinimumBrightness) / (double)(dwMaximumBrightness - dwMinimumBrightness);
        }

        public static void SetMonitorBrightness(PHYSICAL_MONITOR physicalMonitor, double brightness)
        {
            uint dwMinimumBrightness, dwCurrentBrightness, dwMaximumBrightness;
            if (!GetMonitorBrightness(physicalMonitor.hPhysicalMonitor, out dwMinimumBrightness, out dwCurrentBrightness, out dwMaximumBrightness))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            if (!SetMonitorBrightness(physicalMonitor.hPhysicalMonitor, (uint)(dwMinimumBrightness + (dwMaximumBrightness - dwMinimumBrightness) * brightness)))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        public static void BrightnessUp(IntPtr handle)
        {
            try
            {
                PHYSICAL_MONITOR[] physicalMonitors = GetPhysicalMonitors(handle);

                foreach (PHYSICAL_MONITOR physicalMonitor in physicalMonitors)
                {
                    currentMonitorBrightness = GetMonitorBrightness(physicalMonitor) * 100;
                    try
                    {
                        SetMonitorBrightness(physicalMonitor, currentMonitorBrightness + 1);
                    }
                    catch (Win32Exception e_)
                    {
                        // LG Flatron W2443T sometimes causes ERROR_GEN_FAILURE when rapidly changing brightness or contrast
                        if (e_.NativeErrorCode == ERROR_GEN_FAILURE)
                        {
                            break;
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR Brightness UP: " + e.Message);
            }
        }

        public static void BrightnessDown(IntPtr handle)
        {
            try
            {
                PHYSICAL_MONITOR[] physicalMonitors = GetPhysicalMonitors(handle);
                int i = 0;
                foreach (PHYSICAL_MONITOR physicalMonitor in physicalMonitors)
                {
                    Console.WriteLine("Monitor " +(i++));
                    currentMonitorBrightness = GetMonitorBrightness(physicalMonitor) * 100;
                    SetMonitorBrightness(physicalMonitor, currentMonitorBrightness - 1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR Brightness DOWN: " + e.Message);
            }
        }
    }
}
