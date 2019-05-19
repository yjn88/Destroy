namespace Destroy.Kernel
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 内核 <see langword="static"/>
    /// </summary>
    public static class KERNEL
    {
        private const string DESTROY_KERNEL = "DestroyKernel.dll";

        #region Window

        /// <summary>
        /// 获取控制台窗口句柄
        /// </summary>
        /// <param name="hConsoleWindow">控制台窗口句柄</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_WINDOW_HANDLE(out IntPtr hConsoleWindow);

        /// <summary>
        /// 获取控制台窗体在桌面上的坐标
        /// </summary>
        /// <param name="x">相对于左边的值</param>
        /// <param name="y">相对于上边的值</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_WINDOW_POS(out int x, out int y);

        /// <summary>
        /// 设置控制台窗体在桌面上的坐标
        /// </summary>
        /// <param name="x">相对于左边的值</param>
        /// <param name="y">相对于上边的值</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SET_WINDOW_POS(int x, int y);

        /// <summary>
        /// 获取控制台窗体的大小
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_WINDOW_SIZE(out int width, out int height);

        /// <summary>
        /// 获取桌面的大小
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_DESKTOP_SIZE(out int width, out int height);

        /// <summary>
        /// 获取控制台窗口的透明度
        /// </summary>
        /// <param name="alpha">透明度[0 - 255]</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_WINDOW_ALPHA(out byte alpha);

        /// <summary>
        /// 设置控制台窗口的透明度
        /// </summary>
        /// <param name="alpha">透明度[0 - 255]</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SET_WINDOW_ALPHA(byte alpha);

        /// <summary>
        /// 获取控制台是否具有焦点
        /// </summary>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_WINDOW_IN_FOCUS();

        /// <summary>
        /// 设置窗口菜单栏
        /// </summary>
        /// <param name="visible">是否显示</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SET_WINDOW_MENU(bool visible);

        /// <summary>
        /// 获取鼠标位置
        /// </summary>
        /// <param name="x">鼠标横向坐标</param>
        /// <param name="y">鼠标纵向坐标</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_CURSOR_POS(out int x, out int y);

        /// <summary>
        /// 设置鼠标位置
        /// </summary>
        /// <param name="x">鼠标横向坐标</param>
        /// <param name="y">鼠标纵向坐标</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SET_CURSOR_POS(int x, int y);

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="text">消息框文本</param>
        /// <param name="caption">消息框标题</param>
        /// <param name="type">消息框类型</param>
        /// <param name="choose">用户选择</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL, CharSet = CharSet.Unicode)]
        public static extern bool MESSAGE_BOX(string text, string caption, uint type, out int choose);

        /// <summary>
        /// 最大化窗口
        /// </summary>
        /// <param name="maximize">是否最大化(如果窗口此时已经最大化并且该参数为false则会还原窗口尺寸)</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool MAXIMIZE_WINDOW(bool maximize);

        #endregion

        #region Console

        /// <summary>
        /// 获取控制台句柄
        /// </summary>
        /// <param name="handle">句柄</param>
        /// <param name="stdHandle">句柄类型</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_CONSOLE_HANDLE(out IntPtr handle, uint stdHandle);

        /// <summary>
        /// 创建控制台屏幕缓冲区
        /// </summary>
        /// <param name="hConsoleScreen">控制台屏幕缓冲区</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool CREAT_CONSOLE_BUFFER(out IntPtr hConsoleScreen);

        /// <summary>
        /// 设置控制台活跃屏幕缓冲区
        /// </summary>
        /// <param name="hConsoleScreen">控制台屏幕缓冲区</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SET_CONSOLE_ACTIVE_BUFFER(IntPtr hConsoleScreen);

        /// <summary>
        /// 获取控制台模式
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="hConsoleInput">控制台标准输入句柄</param>
        /// <param name="cim">控制台输入模式</param>
        /// <param name="com">控制台输出模式</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_CONSOLE_MODE(IntPtr hConsoleOutput, IntPtr hConsoleInput, out ConsoleInputMode cim, out ConsoleOutputMode com);

        /// <summary>
        /// 设置控制台模式
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="hConsoleInput">控制台标准输入句柄</param>
        /// <param name="cim">控制台输入模式</param>
        /// <param name="com">控制台输出模式</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SET_CONSOLE_MODE(IntPtr hConsoleOutput, IntPtr hConsoleInput, ConsoleInputMode cim, ConsoleOutputMode com);

        /// <summary>
        /// 获取控制台字体
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="bold">是否使用粗体</param>
        /// <param name="fontWidth">字体宽度</param>
        /// <param name="fontHeight">字体高度</param>
        /// <param name="fontName">字体名字</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL, CharSet = CharSet.Unicode)]
        public static extern bool GET_CONSOLE_FONT(IntPtr hConsoleOutput, out bool bold, out short fontWidth, out short fontHeight, ref byte fontName);

        /// <summary>
        /// 设置控制台字体
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="bold">是否使用粗体</param>
        /// <param name="fontWidth">字体宽度</param>
        /// <param name="fontHeight">字体高度</param>
        /// <param name="fontName">字体名字</param>
        /// <param name="fontNameLength">字体名字字符串长度</param>
        /// <returns>是否成功</returns>
        [Unstable("If the API does not work, should set font manually.")]
        [DllImport(DESTROY_KERNEL, CharSet = CharSet.Unicode)]
        public static extern bool SET_CONSOLE_FONT(IntPtr hConsoleOutput, bool bold, short fontWidth, short fontHeight, string fontName, uint fontNameLength);

        /// <summary>
        /// 获取控制台屏幕缓冲区大小
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_CONSOLE_BUFFER_SIZE(IntPtr hConsoleOutput, out short width, out short height);

        /// <summary>
        /// 设置控制台屏幕缓冲区大小
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>是否成功</returns>
        [Unstable("Specific width/height can't less than the width/height of console screen buffer window")]
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SET_CONSOLE_BUFFER_SIZE(IntPtr hConsoleOutput, short width, short height);

        /// <summary>
        /// 获取控制台窗口大小
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_CONSOLE_WINDOW_SIZE(IntPtr hConsoleOutput, out short width, out short height);

        /// <summary>
        /// 设置控制台窗口大小
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>是否成功</returns>
        [Unstable("If the API does not work, should set the console screen buffer window size manually.")]
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SET_CONSOLE_WINDOW_SIZE(IntPtr hConsoleOutput, short width, short height);

        /// <summary>
        /// 获取控制台调色盘
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="index">颜色下标(0-15)</param>
        /// <param name="r">红色</param>
        /// <param name="g">绿色</param>
        /// <param name="b">蓝色</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_CONSOLE_PALETTE(IntPtr hConsoleOutput, uint index, out uint r, out uint g, out uint b);

        /// <summary>
        /// 设置控制台调色盘
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="index">颜色下标(0-15)</param>
        /// <param name="r">红色</param>
        /// <param name="g">绿色</param>
        /// <param name="b">蓝色</param>
        /// <returns>是否成功</returns>
        [Unstable("For unknown reasons, this method will resize console window size or console buffer size.")]
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SET_CONSOLE_PALETTE(IntPtr hConsoleOutput, uint index, uint r, uint g, uint b);

        /// <summary>
        /// 获取控制台标题
        /// </summary>
        /// <param name="title">控制台标题指针</param>
        /// <param name="length">控制台标题长度</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL, CharSet = CharSet.Unicode)]
        public static extern bool GET_CONSOLE_TITLE(ref byte title, out uint length);

        /// <summary>
        /// 设置控制台标题
        /// </summary>
        /// <param name="title">控制台标题</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL, CharSet = CharSet.Unicode)]
        public static extern bool SET_CONSOLE_TITLE(string title);

        /// <summary>
        /// 获取控制台输入代码页
        /// </summary>
        /// <param name="inputCP">输入代码页</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_CONSOLE_CP(out uint inputCP);

        /// <summary>
        /// 设置控制台输入代码页
        /// </summary>
        /// <param name="inputCP">输入代码页</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SET_CONSOLE_CP(uint inputCP);

        /// <summary>
        /// 获取控制台输出代码页
        /// </summary>
        /// <param name="outputCP">输出代码页</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_CONSOLE_OUTPUT_CP(out uint outputCP);

        /// <summary>
        /// 设置控制台输出代码页
        /// </summary>
        /// <param name="outputCP">输出代码页</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SET_CONSOLE_OUTPUT_CP(uint outputCP);

        /// <summary>
        /// 获取控制台特性
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_CONSOLE_ATTRIBUTE(IntPtr hConsoleOutput, out ushort foreColor, out ushort backColor);

        /// <summary>
        /// 设置控制台特性
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SET_CONSOLE_ATTRIBUTE(IntPtr hConsoleOutput, ushort foreColor, ushort backColor);

        /// <summary>
        /// 获取控制台光标信息
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="cursorVisible">光标可见性</param>
        /// <param name="cursorSize">光标尺寸(1-100)</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_CONSOLE_CURSOR_INFO(IntPtr hConsoleOutput, out bool cursorVisible, out uint cursorSize);

        /// <summary>
        /// 设置控制台光标信息
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="cursorVisible">光标可见性</param>
        /// <param name="cursorSize">光标尺寸(1-100)</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SET_CONSOLE_CURSOR_INFO(IntPtr hConsoleOutput, bool cursorVisible, uint cursorSize);

        /// <summary>
        /// 获取光标坐标
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="x">横向坐标</param>
        /// <param name="y">纵向坐标</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_CONSOLE_CURSOR_POS(IntPtr hConsoleOutput, out short x, out short y);

        /// <summary>
        /// 设置光标坐标
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="x">横向坐标</param>
        /// <param name="y">纵向坐标</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SET_CONSOLE_CURSOR_POS(IntPtr hConsoleOutput, short x, short y);

        /// <summary>
        /// 读取控制台
        /// </summary>
        /// <param name="hConsoleInput">控制台标准输入句柄</param>
        /// <param name="str">字符串指针</param>
        /// <param name="numberOfCharToRead">要读取的字符数</param>
        /// <param name="read">实际读取的字符数</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool READ_CONSOLE(IntPtr hConsoleInput, ref byte str, uint numberOfCharToRead, out uint read);

        /// <summary>
        /// 读取控制台输入(请开启控制台输入模式EnableWindowInput和EnableQuickEditMode)
        /// </summary>
        /// <param name="hConsoleInput">控制台标准输入句柄</param>
        /// <param name="mousePosX">相对于左边的值</param>
        /// <param name="mousePosY">相对于上边的值</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool READ_CONSOLE_INPUT(IntPtr hConsoleInput, out short mousePosX, out short mousePosY);

        /// <summary>
        /// 清空控制台输入
        /// </summary>
        /// <param name="hConsoleInput">控制台输入句柄</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool FLUSH_CONSOLE_INPUT(IntPtr hConsoleInput);

        /// <summary>
        /// 写入控制台
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="charBuffer">字符串</param>
        /// <param name="numberOfCharsToWrite">字符串长度</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL, CharSet = CharSet.Unicode)]
        public static extern bool WRITE_CONSOLE(IntPtr hConsoleOutput, string charBuffer, uint numberOfCharsToWrite);

        /// <summary>
        /// 写入控制台输出
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="charInfos">字符信息数组</param>
        /// <param name="x">横向坐标(英文字母宽度)</param>
        /// <param name="y">纵向坐标</param>
        /// <param name="width">宽度(英文字母宽度)</param>
        /// <param name="height">高度</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool WRITE_CONSOLE_OUTPUT(IntPtr hConsoleOutput, CharInfo[] charInfos, short x, short y, short width, short height);

        /// <summary>
        /// 写入控制台输出字符
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="charBuffer">字符串</param>
        /// <param name="numberOfCharsToWrite">字符串长度</param>
        /// <param name="x">横向坐标</param>
        /// <param name="y">纵向坐标</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL, CharSet = CharSet.Unicode)]
        public static extern bool WRITE_CONSOLE_OUTPUT_CHAR(IntPtr hConsoleOutput, string charBuffer, uint numberOfCharsToWrite, short x, short y);

        /// <summary>
        /// 打印屏幕特性
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="colors">颜色数组</param>
        /// <param name="numberOfColorsToWrite">颜色数组的长度</param>
        /// <param name="x">横向坐标</param>
        /// <param name="y">纵向坐标</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool WRITE_CONSOLE_OUTPUT_ATTRIBUTE(IntPtr hConsoleOutput, ushort[] colors, uint numberOfColorsToWrite, short x, short y);

        /// <summary>
        /// 填充屏幕字符
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="c">填充的字符</param>
        /// <param name="width">填充的宽度</param>
        /// <param name="x">横向坐标</param>
        /// <param name="y">纵向坐标</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL, CharSet = CharSet.Unicode)]
        public static extern bool FILL_CONSOLE_OUTPUT_CHAR(IntPtr hConsoleOutput, char c, uint width, short x, short y);

        /// <summary>
        /// 填充屏幕特性
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="color">填充的颜色</param>
        /// <param name="width">填充的宽度</param>
        /// <param name="x">横向坐标</param>
        /// <param name="y">纵向坐标</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool FILL_CONSOLE_OUTPUT_ATTRIBUTE(IntPtr hConsoleOutput, ushort color, uint width, short x, short y);

        /// <summary>
        /// 获取控制台窗口可以设置的最大尺寸
        /// </summary>
        /// <param name="hConsoleOutput">控制台标准输出句柄</param>
        /// <param name="width">最大宽度</param>
        /// <param name="height">最大高度</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_LARGEST_CONSOLE_WINDOW_SIZE(IntPtr hConsoleOutput, out short width, out short height);

        /// <summary>
        /// 设置控制台Ctrl+C信号处理
        /// </summary>
        /// <param name="disable">禁用</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SET_CONSOLE_CTRL_HANDLER(bool disable);

        #endregion

        #region Other

        /// <summary>
        /// 获取按键输入
        /// </summary>
        /// <param name="key">按键</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool GET_KEY(int key);

        /// <summary>
        /// 开始计时
        /// </summary>
        /// <param name="freq">频率</param>
        /// <param name="start">开始时间</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool START_TIMING(out long freq, out long start);

        /// <summary>
        /// 结束计时
        /// </summary>
        /// <param name="freq">频率</param>
        /// <param name="start">开始时间</param>
        /// <param name="timeCost">花费时间(单位:ms)</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool END_TIMING(long freq, long start, out long timeCost);

        /// <summary>
        /// 睡眠指定毫秒
        /// </summary>
        /// <param name="milliSeconds">毫秒(ms)</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool SLEEP(uint milliSeconds);

        /// <summary>
        /// 关闭句柄
        /// </summary>
        /// <param name="handle">句柄</param>
        /// <returns>是否成功</returns>
        [DllImport(DESTROY_KERNEL)]
        public static extern bool CLOSE_HANDLE(IntPtr handle);

        #endregion
    }
}