namespace Destroy
{
    using System;

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
}