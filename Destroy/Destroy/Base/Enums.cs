namespace Destroy
{
    using System;

    /// <summary>
    /// 控制台类型
    /// </summary>
    public enum ConsoleType
    {
        /// <summary>
        /// 默认(Consolas)
        /// </summary>
        Default,

        /// <summary>
        /// 中文字体(新宋体)
        /// 注意:某些特殊字符在中文字体下打印的效果不同于英文字体(Consolas)
        /// 的打印效果, 所以在打印这些特殊字符时请确保选择正确的控制台类型
        /// </summary>
        Chinese,

        /// <summary>
        /// 像素风格
        /// </summary>
        [Obsolete]
        Pixel,

        /// <summary>
        /// 高品质风格
        /// </summary>
        [Obsolete]
        HignQuality,
    }

    /// <summary>
    /// 鼠标按键
    /// </summary>
    public enum MouseButton
    {
        /// <summary>
        /// 左键
        /// </summary>
        Left = 1,

        /// <summary>
        /// 中键
        /// </summary>
        Right = 2,

        /// <summary>
        /// 右键
        /// </summary>
        Middle = 4,
    }

    /// <summary>
    /// 字符宽度
    /// </summary>
    public enum CharWidth
    {
        /// <summary>
        /// //跟'a'一样, 占用1个CHAR_INFO
        /// </summary>
        Single = 1,

        /// <summary>
        /// //跟'啊'一样, 占用2个CHAR_INFO
        /// </summary>
        Double = 2,
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
    /// 运行时激活
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EnableOnRuntime : Attribute
    {
    }
}