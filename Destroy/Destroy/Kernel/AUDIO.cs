namespace Destroy.Kernel
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 音频 <see langword="static"/>
    /// </summary>
    public static class AUDIO
    {
        /// <summary>
        /// 打开音频
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>是否成功</returns>
        [Obsolete("BUG in C#, normal in C++")]
        [DllImport(KERNEL.DESTROY_KERNEL, CharSet = CharSet.Unicode)]
        public static extern bool OPEN(string path);

        /// <summary>
        /// 关闭音频
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>是否成功</returns>
        [DllImport(KERNEL.DESTROY_KERNEL, CharSet = CharSet.Unicode)]
        public static extern bool CLOSE(string path);

        /// <summary>
        /// 播放音频
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="repeat">重复播放</param>
        /// <returns>是否成功</returns>
        [DllImport(KERNEL.DESTROY_KERNEL, CharSet = CharSet.Unicode)]
        public static extern bool PLAY(string path, bool repeat);

        /// <summary>
        /// 暂停音频
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>是否成功</returns>
        [DllImport(KERNEL.DESTROY_KERNEL, CharSet = CharSet.Unicode)]
        public static extern bool PAUSE(string path);

        /// <summary>
        /// 继续播放音频
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>是否成功</returns>
        [DllImport(KERNEL.DESTROY_KERNEL, CharSet = CharSet.Unicode)]
        public static extern bool RESUME(string path);

        /// <summary>
        /// 设置音频音量
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="volume">音量(0-100)</param>
        /// <returns>是否成功</returns>
        [DllImport(KERNEL.DESTROY_KERNEL, CharSet = CharSet.Unicode)]
        public static extern bool SET_VOLUME(string path, ushort volume);
    }
}