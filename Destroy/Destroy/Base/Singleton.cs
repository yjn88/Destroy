namespace Destroy
{
    /// <summary>
    /// 继承该类获得单例 (没有为多线程考虑)
    /// </summary>
    public class Singleton<T> where T : new()
    {
        private static T instance;

        /// <summary>
        /// 单例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }
    }
}