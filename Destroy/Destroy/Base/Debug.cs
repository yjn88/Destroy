namespace Destroy
{
    /// <summary>
    /// 用于调试程序 <see langword="static"/>
    /// </summary>
    public static class Debug
    {
        /// <summary>
        /// 输出调试信息
        /// </summary>
        /// <param name="obj">对象</param>
        public static void Log(object obj)
        {
#if DEBUG
            //向VisualStudio的Output中输出msg
            System.Diagnostics.Debug.WriteLine(obj.ToString());
#endif
        }
    }
}