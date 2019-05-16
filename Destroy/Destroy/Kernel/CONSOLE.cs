namespace Destroy.Kernel
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// 不稳定方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class Unstable : Attribute
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">消息</param>
        public Unstable(string message = "")
        {
            Message = message;
        }
    }

    /// <summary>
    /// 错误异常类
    /// </summary>
    public class Error : Exception
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">错误消息</param>
        public Error(string message = nameof(Error)) : base(message)
        {
        }

        /// <summary>
        /// 抛出错误
        /// </summary>
        /// <param name="message">错误消息</param>
        public static void Pop(string message = nameof(Error))
        {
            throw new Error(message);
        }

        /// <summary>
        /// 检查是否错误
        /// </summary>
        /// <param name="result">方法返回值</param>
        /// <param name="message">错误消息</param>
        public static void Check(bool result, string message = nameof(Error))
        {
            if (!result)
            {
                Pop(message);
            }
        }
    }

    /// <summary>
    /// 控制台标准句柄类型
    /// </summary>
    public enum HandleType
    {
        /// <summary>
        /// 输入
        /// </summary>
        Input = -10,

        /// <summary>
        /// 输出
        /// </summary>
        Output = -11,

        /// <summary>
        /// 错误
        /// </summary>
        Error = -12,
    }

    /// <summary>
    /// 调色盘类型
    /// </summary>
    public enum PaletteType
    {
        /// <summary>
        /// 遗产调色盘
        /// </summary>
        Legacy,

        /// <summary>
        /// 现代调色盘
        /// </summary>
        Modern
    }

    /// <summary>
    /// 消息框类型
    /// </summary>
    public enum MessageBoxType
    {
        /// <summary>
        /// 显示OK(默认值)
        /// </summary>
        OK = (int)0x00000000L,

        /// <summary>
        /// 显示OK与取消
        /// </summary>
        OKCancel = (int)0x00000001L,

        /// <summary>
        /// 显示重试与取消
        /// </summary>
        RetryCancel = (int)0x00000005L,

        /// <summary>
        /// 显示是与否
        /// </summary>
        YesNo = (int)0x00000004L,

        /// <summary>
        /// 显示是与否与取消
        /// </summary>
        YesNoCancel = (int)0x00000003L,
    }

    /// <summary>
    /// 消息框图标
    /// </summary>
    public enum MessageBoxIcon
    {
        /// <summary>
        /// 信息图标
        /// </summary>
        IconInformation = (int)0x00000040L,

        /// <summary>
        /// 警告图标
        /// </summary>
        IconWarning = (int)0x00000030L,

        /// <summary>
        /// 错误图标
        /// </summary>
        IconError = (int)0x00000010L,
    }

    /// <summary>
    /// 消息框默认按钮
    /// </summary>
    public enum MessageBoxDefault
    {
        /// <summary>
        /// 第一个按钮是默认按钮(默认值)
        /// </summary>
        DefaultButton1 = (int)0x00000000L,

        /// <summary>
        /// 第二个按钮是默认按钮
        /// </summary>
        DefaultButton2 = (int)0x00000100L,

        /// <summary>
        /// 第三个按钮是默认按钮
        /// </summary>
        DefaultButton3 = (int)0x00000200L,

        /// <summary>
        /// 第四个按钮是默认按钮
        /// </summary>
        DefaultButton4 = (int)0x00000300L,
    }

    /// <summary>
    /// 消息框选择
    /// </summary>
    public enum MessageBoxChoice
    {
        /// <summary>
        /// OK按钮被选中
        /// </summary>
        OK = 1,

        /// <summary>
        /// Cancel按钮被选中
        /// </summary>
        Cancel = 2,

        /// <summary>
        /// Retry按钮被选中
        /// </summary>
        Retry = 4,

        /// <summary>
        /// Yes按钮被选中
        /// </summary>
        Yes = 6,

        /// <summary>
        /// No按钮被选中
        /// </summary>
        No = 7,
    }

    /// <summary>
    /// 字符信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CharInfo
    {
        /// <summary>
        /// Unicode字符
        /// </summary>
        public char UnicodeChar;

        /// <summary>
        /// 前景色背景色
        /// </summary>
        public ushort Attributes;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unicodeChar">Unicode字符</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        public CharInfo(char unicodeChar, ConsoleColor foreColor, ConsoleColor backColor)
        {
            UnicodeChar = unicodeChar;
            Attributes = (ushort)((int)backColor * 16 + (int)foreColor);
        }

        /// <summary>
        /// 前景色
        /// </summary>
        public ConsoleColor ForeColor
        {
            get
            {
                ConsoleColor foreColor = (ConsoleColor)(Attributes & 0x000F);
                return foreColor;
            }
            set
            {
                Attributes = (ushort)((int)BackColor * 16 + (int)value);
            }
        }

        /// <summary>
        /// 背景色
        /// </summary>
        public ConsoleColor BackColor
        {
            get
            {
                ConsoleColor backColor = (ConsoleColor)((Attributes & 0x00F0) / 16);
                return backColor;
            }
            set
            {
                Attributes = (ushort)((int)value * 16 + (int)ForeColor);
            }
        }

        /// <summary>
        /// 获取前景色跟背景色
        /// </summary>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        public void GetColor(out ConsoleColor foreColor, out ConsoleColor backColor)
        {
            foreColor = (ConsoleColor)(Attributes & 0x000F);
            backColor = (ConsoleColor)((Attributes & 0x00F0) / 16);
        }

        /// <summary>
        /// 设置前景色跟背景色
        /// </summary>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        public void SetColor(ConsoleColor foreColor, ConsoleColor backColor)
        {
            Attributes = (ushort)((int)backColor * 16 + (int)foreColor);
        }
    }

    /// <summary>
    /// 控制台输入模式
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ConsoleInputMode
    {
        /// <summary>
        /// 默认为TRUE
        /// </summary>
        public bool EnableProcessedInput;

        /// <summary>
        /// 默认为TRUE
        /// </summary>
        public bool EnableLineInput;

        /// <summary>
        /// 默认为TRUE
        /// </summary>
        public bool EnableEchoInput;

        /// <summary>
        /// 默认为FALSE(推荐设置为TRUE)
        /// </summary>
        public bool EnableWindowInput;

        /// <summary>
        /// 默认为TRUE
        /// </summary>
        public bool EnableMouseInput;

        /// <summary>
        /// 默认为TRUE
        /// </summary>
        public bool EnableInsertMode;

        /// <summary>
        /// 默认为TRUE(推荐设置为FALSE)
        /// </summary>
        public bool EnableQuickEditMode;

        /// <summary>
        /// 默认为TRUE
        /// </summary>
        public bool EnableExtendedFlags;

        /// <summary>
        /// 默认为TRUE
        /// </summary>
        public bool EnableAutoPosition;

        /// <summary>
        /// 默认为FALSE
        /// </summary>
        public bool EnableVirtualTerminalInput;
    }

    /// <summary>
    /// 控制台输出模式
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ConsoleOutputMode
    {
        /// <summary>
        /// 默认为TRUE
        /// </summary>
        public bool EnableProcessedOutput;

        /// <summary>
        /// 默认为TRUE (如果不想输出字符后自动换行可以禁用此模式)
        /// </summary>
        public bool EnableWrapAtEndOfLineOutput;

        /// <summary>
        /// 默认为FALSE
        /// </summary>
        public bool EnableVirtualTerminalProcessing;

        /// <summary>
        /// 默认为FALSE
        /// </summary>
        public bool DisableNewLineAutoReturn;

        /// <summary>
        /// 默认为FALSE
        /// </summary>
        public bool EnableLVBGridWorldWide;
    }

    /// <summary>
    /// 控制台 <see langword="static"/>
    /// </summary>
    public static class CONSOLE
    {
        #region Package KERNEL

        private const int SIZE_OF_WCHAR = 2;

        private const int MAX_PATH = 260;

        private const int FACE_SIZE = 32;

        private static IntPtr inputHandle;

        private static IntPtr outputHandle;

        private static IntPtr errorHandle;

        /// <summary>
        /// 控制台输入句柄
        /// </summary>
        public static IntPtr InputHandle
        {
            get
            {
                if (inputHandle == IntPtr.Zero)
                {
                    inputHandle = GetConsoleHandle(HandleType.Input);
                }
                return inputHandle;
            }
        }

        /// <summary>
        /// 控制台输出句柄
        /// </summary>
        public static IntPtr OutputHandle
        {
            get
            {
                if (outputHandle == IntPtr.Zero)
                {
                    outputHandle = GetConsoleHandle(HandleType.Output);
                }
                return outputHandle;
            }
        }

        /// <summary>
        /// 控制台错误句柄
        /// </summary>
        public static IntPtr ErrorHandle
        {
            get
            {
                if (errorHandle == IntPtr.Zero)
                {
                    errorHandle = GetConsoleHandle(HandleType.Error);
                }
                return errorHandle;
            }
        }

        /// <summary>
        /// 使控制台窗口在桌面上居中显示
        /// </summary>
        public static void CenterConsoleWindowPosition()
        {
            Error.Check(KERNEL.GET_DESKTOP_SIZE(out int desktopWidth, out int desktopHeight));
            Error.Check(KERNEL.GET_WINDOW_SIZE(out int windowWidth, out int windowHeight));

            int x = (desktopWidth / 2) - (windowWidth / 2);
            int y = (desktopHeight / 2) - (windowHeight / 2);

            Error.Check(KERNEL.SET_WINDOW_POS(x, y));
        }

        /// <summary>
        /// 设置标准控制台调色盘
        /// </summary>
        /// <param name="consoleOutputHandle">控制台标准输出句柄</param>
        /// <param name="paletteType">调色盘类型</param>
        public static void SetStdConsolePalette(IntPtr consoleOutputHandle, PaletteType paletteType = PaletteType.Modern)
        {
            if (paletteType == PaletteType.Legacy)
            {
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 0, 0, 0, 0));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 1, 0, 0, 128));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 2, 0, 128, 0));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 3, 0, 128, 128));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 4, 128, 0, 0));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 5, 128, 0, 128));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 6, 128, 128, 0));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 7, 192, 192, 192));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 8, 128, 128, 128));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 9, 0, 0, 255));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 10, 0, 255, 0));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 11, 0, 255, 255));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 12, 255, 0, 0));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 13, 255, 0, 255));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 14, 255, 255, 0));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 15, 255, 255, 255));
            }
            if (paletteType == PaletteType.Modern)
            {
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 0, 12, 12, 12));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 1, 0, 55, 218));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 2, 19, 161, 14));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 3, 58, 150, 221));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 4, 197, 15, 31));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 5, 136, 23, 152));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 6, 193, 156, 0));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 7, 204, 204, 204));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 8, 118, 118, 118));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 9, 59, 120, 255));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 10, 22, 198, 12));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 11, 97, 214, 214));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 12, 231, 72, 86));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 13, 180, 0, 158));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 14, 249, 241, 165));
                Error.Check(KERNEL.SET_CONSOLE_PALETTE(consoleOutputHandle, 15, 242, 242, 242));
            }
        }

        /// <summary>
        /// 设置标准控制台模式
        /// </summary>
        /// <param name="consoleOutputHandle">控制台标准输出句柄</param>
        /// <param name="consoleInputHandle">控制台标准输入句柄</param>
        public static void SetStdConsoleMode(IntPtr consoleOutputHandle, IntPtr consoleInputHandle)
        {
            Error.Check(KERNEL.GET_CONSOLE_MODE
            (
                consoleOutputHandle,
                consoleInputHandle,
                out ConsoleInputMode consoleInputMode,
                out ConsoleOutputMode consoleOutputMode
            ));
            //允许窗口输入
            consoleInputMode.EnableWindowInput = true;
            //关闭快速编辑模式
            consoleInputMode.EnableQuickEditMode = false;
            Error.Check(KERNEL.SET_CONSOLE_MODE
            (
                consoleOutputHandle,
                consoleInputHandle,
                consoleInputMode,
                consoleOutputMode
             ));
        }

        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="text">消息框文本</param>
        /// <param name="caption">消息框标题</param>
        /// <param name="messageBoxType">消息框类型</param>
        /// <param name="messageBoxIcon">消息框图标</param>
        /// <param name="messageBoxDefault">消息框默认选项</param>
        /// <returns>用户选择</returns>
        public static MessageBoxChoice MessageBox(string text, string caption, MessageBoxType messageBoxType, MessageBoxIcon messageBoxIcon, MessageBoxDefault messageBoxDefault)
        {
            uint type = (uint)messageBoxType | (uint)messageBoxIcon | (uint)messageBoxDefault;
            Error.Check(KERNEL.MESSAGE_BOX(text, caption, type, out int choose));
            return (MessageBoxChoice)choose;
        }

        /// <summary>
        /// 获取按键输入
        /// </summary>
        /// <param name="consoleKey">控制台按键</param>
        /// <returns>是否按下</returns>
        public static bool GetKey(ConsoleKey consoleKey)
        {
            return KERNEL.GET_KEY((int)consoleKey);
        }

        /// <summary>
        /// 获取控制台句柄
        /// </summary>
        /// <param name="handleType">句柄类型</param>
        /// <returns>句柄</returns>
        public static IntPtr GetConsoleHandle(HandleType handleType)
        {
            unchecked
            {
                Error.Check(KERNEL.GET_CONSOLE_HANDLE(out IntPtr handle, (uint)handleType));
                return handle;
            }
        }

        /// <summary>
        /// 获取控制台字体
        /// </summary>
        /// <param name="consoleOutputHandle">控制台标准输出句柄</param>
        /// <param name="bold">是否使用粗体</param>
        /// <param name="fontWidth">字体宽度</param>
        /// <param name="fontHeight">字体高度</param>
        /// <param name="fontName">字体名字</param>
        public static void GetConsoleFont(IntPtr consoleOutputHandle, out bool bold, out short fontWidth, out short fontHeight, out string fontName)
        {
            byte[] bytes = new byte[FACE_SIZE * SIZE_OF_WCHAR];

            Error.Check(KERNEL.GET_CONSOLE_FONT(consoleOutputHandle, out bold,
                out fontWidth, out fontHeight, ref bytes[0]));

            string str = Encoding.Unicode.GetString(bytes);
            string[] strArray = str.Split('\0');
            fontName = strArray[0];
        }

        /// <summary>
        /// 设置控制台字体
        /// </summary>
        /// <param name="consoleOutputHandle">控制台标准输出句柄</param>
        /// <param name="bold">是否使用粗体</param>
        /// <param name="fontWidth">字体宽度</param>
        /// <param name="fontHeight">字体高度</param>
        /// <param name="fontName">字体名字</param>
        public static void SetConsoleFont(IntPtr consoleOutputHandle, bool bold, short fontWidth, short fontHeight, string fontName)
        {
            Error.Check(KERNEL.SET_CONSOLE_FONT(consoleOutputHandle, bold, fontWidth, fontHeight,
                fontName, (uint)fontName.Length));
        }

        /// <summary>
        /// 获取控制台标题
        /// </summary>
        /// <returns>控制台标题</returns>
        public static string GetConsoleTitle()
        {
            byte[] bytes = new byte[MAX_PATH * SIZE_OF_WCHAR];

            Error.Check(KERNEL.GET_CONSOLE_TITLE(ref bytes[0], out uint length));

            string title = Encoding.Unicode.GetString(bytes, 0, (int)length * SIZE_OF_WCHAR);

            return title;
        }

        /// <summary>
        /// 获取控制台特性
        /// </summary>
        /// <param name="consoleOutputHandle">控制台标准输出句柄</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        public static void GetConsoleAttribute(IntPtr consoleOutputHandle, out ConsoleColor foreColor, out ConsoleColor backColor)
        {
            Error.Check(KERNEL.GET_CONSOLE_ATTRIBUTE(consoleOutputHandle,
                out ushort f, out ushort b));
            foreColor = (ConsoleColor)f;
            backColor = (ConsoleColor)b;
        }

        /// <summary>
        /// 设置控制台特性
        /// </summary>
        /// <param name="consoleOutputHandle">控制台标准输出句柄</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        public static void SetConsoleAttribute(IntPtr consoleOutputHandle, ConsoleColor foreColor, ConsoleColor backColor)
        {
            Error.Check(KERNEL.SET_CONSOLE_ATTRIBUTE(consoleOutputHandle,
                (ushort)foreColor, (ushort)backColor));
        }

        /// <summary>
        /// 读取控制台
        /// </summary>
        /// <param name="consoleInputHandle">控制台标准输入句柄</param>
        /// <returns>字符串</returns>
        public static string ReadConsole(IntPtr consoleInputHandle)
        {
            byte[] bytes = new byte[MAX_PATH * SIZE_OF_WCHAR];

            Error.Check(KERNEL.READ_CONSOLE(consoleInputHandle, ref bytes[0], MAX_PATH, out uint read));

            string str = Encoding.Unicode.GetString(bytes, 0, (int)read * SIZE_OF_WCHAR);
            //删除换行符
            str = str.Substring(0, str.Length - Environment.NewLine.Length);
            return str;
        }

        /// <summary>
        /// 写入控制台
        /// </summary>
        /// <param name="consoleOutputHandle">控制台标准输出句柄</param>
        /// <param name="str">字符串</param>
        public static void WriteConsole(IntPtr consoleOutputHandle, string str)
        {
            Error.Check(KERNEL.WRITE_CONSOLE(consoleOutputHandle, str, (uint)str.Length));
        }

        /// <summary>
        /// 将指定对象的文本表示形式写入控制台屏幕缓冲区的指定位置
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="x">横向坐标</param>
        /// <param name="y">纵向坐标</param>
        public static void Print(object obj, short x, short y)
        {
            string str = obj.ToString();
            Error.Check(KERNEL.WRITE_CONSOLE_OUTPUT_CHAR(OutputHandle, str, (uint)str.Length, x, y));
        }

        /// <summary>
        /// 将指定的前景色与背景色写入控制台屏幕缓冲区
        /// </summary>
        /// <param name="foreColors">前景色数组</param>
        /// <param name="backColors">背景色数组</param>
        /// <param name="x">横向坐标</param>
        /// <param name="y">纵向坐标</param>
        public static void PrintColor(ConsoleColor[] foreColors, ConsoleColor[] backColors, short x, short y)
        {
            ushort[] colors = new ushort[foreColors.Length];

            for (int i = 0; i < colors.Length; i++)
            {
                int bgc = (int)backColors[i] * 16;
                int fgc = (int)foreColors[i];

                colors[i] = (ushort)(fgc + bgc);
            }

            Error.Check(KERNEL.WRITE_CONSOLE_OUTPUT_ATTRIBUTE(OutputHandle, colors,
                (uint)colors.Length, x, y));
        }

        /// <summary>
        /// 将指定的字符以指定宽度写入控制台屏幕缓冲区的指定位置
        /// </summary>
        /// <param name="c">字符</param>
        /// <param name="width">宽度</param>
        /// <param name="x">横向坐标</param>
        /// <param name="y">纵向坐标</param>
        public static void Fill(char c, uint width, short x, short y)
        {
            Error.Check(KERNEL.FILL_CONSOLE_OUTPUT_CHAR(OutputHandle, c, width, x, y));
        }

        /// <summary>
        /// 将指定的前景色与背景色以指定宽度写入控制台屏幕缓冲区
        /// </summary>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        /// <param name="width">宽度</param>
        /// <param name="x">横向坐标</param>
        /// <param name="y">纵向坐标</param>
        public static void FillColor(ConsoleColor foreColor, ConsoleColor backColor, uint width, short x, short y)
        {
            int bgc = (int)backColor * 16;
            int fgc = (int)foreColor;
            ushort color = (ushort)(fgc + bgc);

            Error.Check(KERNEL.FILL_CONSOLE_OUTPUT_ATTRIBUTE(OutputHandle, color, width, x, y));
        }

        #endregion

        #region Simple Console

        private static bool treatControlCAsInput = false;

        /// <summary>
        /// 控制台输入代码页
        /// </summary>
        public static Encoding InputEncoding
        {
            get
            {
                Error.Check(KERNEL.GET_CONSOLE_CP(out uint inputCP));
                return Encoding.GetEncoding((int)inputCP);
            }
            set
            {
                Error.Check(KERNEL.SET_CONSOLE_CP((uint)value.CodePage));
            }
        }

        /// <summary>
        /// 控制台输出代码页
        /// </summary>
        public static Encoding OutputEncoding
        {
            get
            {
                Error.Check(KERNEL.GET_CONSOLE_OUTPUT_CP(out uint inputCP));
                return Encoding.GetEncoding((int)inputCP);
            }
            set
            {
                Error.Check(KERNEL.SET_CONSOLE_OUTPUT_CP((uint)value.CodePage));
            }
        }

        /// <summary>
        /// 控制台前景色
        /// </summary>
        public static ConsoleColor ForegroundColor
        {
            get
            {
                GetConsoleAttribute(OutputHandle, out ConsoleColor foreColor, out ConsoleColor backColor);
                return foreColor;
            }
            set
            {
                SetConsoleAttribute(OutputHandle, value, BackgroundColor);
            }
        }

        /// <summary>
        /// 控制台背景色
        /// </summary>
        public static ConsoleColor BackgroundColor
        {
            get
            {
                GetConsoleAttribute(OutputHandle, out ConsoleColor foreColor, out ConsoleColor backColor);
                return backColor;
            }
            set
            {
                SetConsoleAttribute(OutputHandle, ForegroundColor, value);
            }
        }

        /// <summary>
        /// 获取控制台窗口可以设置的最大宽度
        /// </summary>
        public static int LargestWindowWidth
        {
            get
            {
                Error.Check(KERNEL.GET_LARGEST_CONSOLE_WINDOW_SIZE(OutputHandle, out short width, out short height));
                return width;
            }
        }

        /// <summary>
        /// 获取控制台窗口可以设置的最大高度
        /// </summary>
        public static int LargestWindowHeight
        {
            get
            {
                Error.Check(KERNEL.GET_LARGEST_CONSOLE_WINDOW_SIZE(OutputHandle, out short width, out short height));
                return height;
            }
        }

        /// <summary>
        /// 控制台屏幕缓冲区宽度
        /// </summary>
        public static int BufferWidth
        {
            get
            {
                Error.Check(KERNEL.GET_CONSOLE_BUFFER_SIZE(OutputHandle, out short width, out short height));
                return width;
            }
            set
            {
                Error.Check(KERNEL.SET_CONSOLE_BUFFER_SIZE(OutputHandle, (short)value, (short)BufferHeight));
            }
        }

        /// <summary>
        /// 控制台屏幕缓冲区高度
        /// </summary>
        public static int BufferHeight
        {
            get
            {
                Error.Check(KERNEL.GET_CONSOLE_BUFFER_SIZE(OutputHandle, out short width, out short height));
                return height;
            }
            set
            {
                Error.Check(KERNEL.SET_CONSOLE_BUFFER_SIZE(OutputHandle, (short)BufferWidth, (short)value));
            }
        }

        /// <summary>
        /// 控制台窗口宽度
        /// </summary>
        public static int WindowWidth
        {
            get
            {
                Error.Check(KERNEL.GET_CONSOLE_WINDOW_SIZE(OutputHandle, out short width, out short height));
                return width;
            }
            set
            {
                Error.Check(KERNEL.SET_CONSOLE_WINDOW_SIZE(OutputHandle, (short)value, (short)WindowHeight));
            }
        }

        /// <summary>
        /// 控制台窗口高度
        /// </summary>
        public static int WindowHeight
        {
            get
            {
                Error.Check(KERNEL.GET_CONSOLE_WINDOW_SIZE(OutputHandle, out short width, out short height));
                return height;
            }
            set
            {
                Error.Check(KERNEL.SET_CONSOLE_WINDOW_SIZE(OutputHandle, (short)WindowWidth, (short)value));
            }
        }

        /// <summary>
        /// 控制台光标横向位置
        /// </summary>
        public static int CursorLeft
        {
            get
            {
                Error.Check(KERNEL.GET_CONSOLE_CURSOR_POS(OutputHandle, out short x, out short y));
                return x;
            }
            set
            {
                Error.Check(KERNEL.SET_CONSOLE_CURSOR_POS(OutputHandle, (short)value, (short)CursorTop));
            }
        }

        /// <summary>
        /// 控制台光标纵向位置
        /// </summary>
        public static int CursorTop
        {
            get
            {
                Error.Check(KERNEL.GET_CONSOLE_CURSOR_POS(OutputHandle, out short x, out short y));
                return y;
            }
            set
            {
                Error.Check(KERNEL.SET_CONSOLE_CURSOR_POS(OutputHandle, (short)CursorLeft, (short)value));
            }
        }

        /// <summary>
        /// 控制台光标尺寸
        /// </summary>
        public static int CursorSize
        {
            get
            {
                Error.Check(KERNEL.GET_CONSOLE_CURSOR_INFO(OutputHandle, out bool visible, out uint size));
                return (int)size;
            }
            set
            {
                Error.Check(KERNEL.SET_CONSOLE_CURSOR_INFO(OutputHandle, CursorVisible, (uint)value));
            }
        }

        /// <summary>
        /// 控制台光标是否可见
        /// </summary>
        public static bool CursorVisible
        {
            get
            {
                Error.Check(KERNEL.GET_CONSOLE_CURSOR_INFO(OutputHandle, out bool visible, out uint size));
                return visible;
            }
            set
            {
                Error.Check(KERNEL.SET_CONSOLE_CURSOR_INFO(OutputHandle, value, (uint)CursorSize));
            }
        }

        /// <summary>
        /// 将Ctrl+C视为普通输入
        /// </summary>
        public static bool TreatControlCAsInput
        {
            get
            {
                return treatControlCAsInput;
            }
            set
            {
                treatControlCAsInput = value;
                KERNEL.SET_CONSOLE_CTRL_HANDLER(value);
            }
        }

        /// <summary>
        /// 获取或设置要显示在控制台标题栏中的标题
        /// </summary>
        public static string Title
        {
            get
            {
                return GetConsoleTitle();
            }
            set
            {
                Error.Check(KERNEL.SET_CONSOLE_TITLE(value));
            }
        }

        /// <summary>
        /// 设置控制台屏幕缓冲区窗口大小
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void SetWindowSize(int width, int height)
        {
            Error.Check(KERNEL.SET_CONSOLE_WINDOW_SIZE(OutputHandle, (short)width, (short)height));
        }

        /// <summary>
        /// 设置控制台屏幕缓冲区大小
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void SetBufferSize(int width, int height)
        {
            Error.Check(KERNEL.SET_CONSOLE_BUFFER_SIZE(OutputHandle, (short)width, (short)height));
        }

        /// <summary>
        /// 设置光标坐标
        /// </summary>
        /// <param name="left">横向坐标</param>
        /// <param name="top">纵向坐标</param>
        public static void SetCursorPosition(int left, int top)
        {
            Error.Check(KERNEL.SET_CONSOLE_CURSOR_POS(OutputHandle, (short)left, (short)top));
        }

        /// <summary>
        /// 获取用户输入的下一个控制台按键
        /// </summary>
        /// <returns>控制台按键</returns>
        public static ConsoleKey ReadKey()
        {
            while (true)
            {
                foreach (object item in Enum.GetValues(typeof(ConsoleKey)))
                {
                    ConsoleKey consoleKey = (ConsoleKey)item;
                    if (GetKey(consoleKey))
                    {
                        return consoleKey;
                    }
                }
            }
        }

        /// <summary>
        /// 从控制台标准输入流读取下一行字符(不包括换行符)
        /// </summary>
        /// <returns>字符串</returns>
        public static string ReadLine()
        {
            return ReadConsole(InputHandle);
        }

        /// <summary>
        /// 将指定对象的文本表示形式写入控制台屏幕缓冲区
        /// </summary>
        /// <param name="obj">对象</param>
        public static void Write(object obj)
        {
            string str = obj.ToString();
            WriteConsole(OutputHandle, str);
        }

        /// <summary>
        /// 将换行符写入控制台屏幕缓冲区
        /// </summary>
        public static void WriteLine()
        {
            string str = Environment.NewLine;
            WriteConsole(OutputHandle, str);
        }

        /// <summary>
        /// 将指定对象的文本表示形式加上换行符写入控制台屏幕缓冲区
        /// </summary>
        /// <param name="obj">对象</param>
        public static void WriteLine(object obj)
        {
            string str = obj.ToString();
            str += Environment.NewLine;
            WriteConsole(OutputHandle, str);
        }

        /// <summary>
        /// 清空默认屏幕屏幕缓冲区
        /// </summary>
        public static void Clear()
        {
            Error.Check(KERNEL.GET_CONSOLE_BUFFER_SIZE(OutputHandle, out short width, out short height));
            Error.Check(KERNEL.FILL_CONSOLE_OUTPUT_ATTRIBUTE(OutputHandle, 0x07, (uint)(width * height), 0, 0));
            Error.Check(KERNEL.FILL_CONSOLE_OUTPUT_CHAR(OutputHandle, ' ', (uint)(width * height), 0, 0));
        }

        /// <summary>
        /// 重置控制台颜色
        /// </summary>
        public static void ResetColor()
        {
            SetConsoleAttribute(OutputHandle, ConsoleColor.Gray, ConsoleColor.Black);
        }

        #endregion
    }
}