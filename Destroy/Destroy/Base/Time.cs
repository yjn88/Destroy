namespace Destroy
{
    /// <summary>
    /// 时间类 <see langword="static"/>
    /// </summary>
    [EnableOnRuntime]
    public static class Time
    {
        /// <summary>
        /// 游戏总运行时间(秒)
        /// </summary>
        public static float TotalTime { get; internal set; }

        /// <summary>
        /// 这一帧距离上一帧的时间(秒)
        /// </summary>
        public static float DeltaTime { get; internal set; }

        /// <summary>
        /// 当前帧率
        /// </summary>
        public static float FrameRate => 1 / DeltaTime;
    }
}