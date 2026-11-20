using System.Runtime.InteropServices;

namespace BBEngine.Core
{
    public static class DisplayResolution
    {
        private const int ENUM_CURRENT_SETTINGS = -1;

        private const uint DM_PELSWIDTH = 0x80000;
        private const uint DM_PELSHEIGHT = 0x100000;

        private const uint CDS_FULLSCREEN = 0x00000004;
        private const uint CDS_TEST = 0x00000002;

        private static DEVMODE _original;
        private static bool _changed;

        [StructLayout(LayoutKind.Sequential)]
        private struct POINTL
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;

            public ushort dmSpecVersion;
            public ushort dmDriverVersion;
            public ushort dmSize;
            public ushort dmDriverExtra;

            public uint dmFields;

            public POINTL dmPosition;
            public uint dmDisplayOrientation;
            public uint dmDisplayFixedOutput;

            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;

            public ushort dmLogPixels;

            public uint dmBitsPerPel;
            public uint dmPelsWidth;
            public uint dmPelsHeight;
            public uint dmDisplayFlags;
            public uint dmDisplayFrequency;

            public uint dmICMMethod;
            public uint dmICMIntent;
            public uint dmMediaType;
            public uint dmDitherType;

            public uint dmReserved1;
            public uint dmReserved2;

            public uint dmPanningWidth;
            public uint dmPanningHeight;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern bool EnumDisplaySettings(
            string? deviceName,
            int modeNum,
            ref DEVMODE devMode
        );

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int ChangeDisplaySettings(
            ref DEVMODE devMode,
            uint flags
        );

        // 设置显示器分辨率
        public static bool SetResolution(int width, int height)
        {
            if (_changed)
                Restore();

            DEVMODE mode = new()
            {
                dmSize = (ushort)Marshal.SizeOf<DEVMODE>()
            };

            // 获取当前分辨率
            if (!EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref mode))
                return false;

            _original = mode;

            // 设置目标分辨率
            mode.dmPelsWidth = (uint)width;
            mode.dmPelsHeight = (uint)height;
            mode.dmFields = DM_PELSWIDTH | DM_PELSHEIGHT;

            // 测试分辨率是否支持
            if (ChangeDisplaySettings(ref mode, CDS_TEST) != 0)
                return false;

            // 正式应用（临时修改）
            if (ChangeDisplaySettings(ref mode, CDS_FULLSCREEN) != 0)
                return false;

            _changed = true;
            return true;
        }

        // 恢复原始分辨率
        public static bool Restore()
        {
            if (!_changed)
                return true;

            int result = ChangeDisplaySettings(ref _original, 0);

            if (result == 0)
                _changed = false;

            return result == 0;
        }
    }
}