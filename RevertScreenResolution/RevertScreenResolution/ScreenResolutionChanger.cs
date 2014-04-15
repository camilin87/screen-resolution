using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RevertScreenResolution
{
    public class ScreenResolutionChanger
    {
        internal static readonly Devmode InvalidDevmode = new Devmode { dmDeviceName = "INVALID" };

        public bool SetResolution(ScreenSize screenSize)
        {
            return SetResolutionInternal(screenSize.Width, screenSize.Height);
        }

        private bool SetResolutionInternal(uint width, uint height)
        {
            var displaySettings = GetCurrentDisplaySettings();
            if (InvalidDevmode.Equals(displaySettings)) return false;

            displaySettings.dmPelsWidth = (int) width;
            displaySettings.dmPelsHeight = (int) height;
            var changeResult = ChangeDisplaySettings(ref displaySettings, CDS_UPDATEREGISTRY);

            return changeResult == DISP_CHANGE_SUCCESSFUL;
        }

        public virtual Devmode GetCurrentDisplaySettings()
        {
            var result = new Devmode();
            result.dmDeviceName = new String(new char[32]);
            result.dmSize = (short)Marshal.SizeOf(result);
            result.dmDeviceName = Screen.PrimaryScreen.DeviceName;

            if (EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref result) == 0)
            {
                result = InvalidDevmode;
            }
            return result;
        }

        public ScreenSize GetMaximumSupportedScreenResolution()
        {
            throw new NotImplementedException();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Devmode
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;

            public short dmOrientation;
            public short dmPaperSize;
            public short dmPaperLength;
            public short dmPaperWidth;

            public short dmScale;
            public short dmCopies;
            public short dmDefaultSource;
            public short dmPrintQuality;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;
            public short dmLogPixels;
            public short dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;

            public int dmDisplayFlags;
            public int dmDisplayFrequency;

            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;

            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        [DllImport("user32.dll")]
        internal static extern int EnumDisplaySettings(string deviceName, int modeNum, ref Devmode devMode);

        [DllImport("user32.dll")]
        internal static extern int ChangeDisplaySettings(ref Devmode devMode, int flags);

        private const int ENUM_CURRENT_SETTINGS = -1;
        private const int CDS_UPDATEREGISTRY = 0x01;
        private const int DISP_CHANGE_SUCCESSFUL = 0;
    }
}
