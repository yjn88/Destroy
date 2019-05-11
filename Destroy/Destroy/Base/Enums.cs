namespace Destroy
{
    /// <summary>
    /// 控制台类型
    /// </summary>
    public enum ConsoleType
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 1,

        /// <summary>
        /// 像素风格
        /// </summary>
        Pixel = 2,

        /// <summary>
        /// 高品质风格
        /// </summary>
        HignQuality = 3,
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
}