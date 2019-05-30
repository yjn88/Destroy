namespace Destroy
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 用于实现对象池
    /// </summary>
    public interface ILazy
    {
        /// <summary>
        /// 设置激活
        /// </summary>
        /// <param name="active">是否激活</param>
        void SetActive(bool active);
    }

    /// <summary>
    /// 实例化委托
    /// </summary>
    /// <returns>Lazy对象</returns>
    public delegate ILazy Instantiate();

    /// <summary>
    /// 对象池
    /// </summary>
    public class ObjectPool
    {
        private Instantiate instantiate;

        private readonly List<ILazy> pool;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="instantiate">实例化委托</param>
        public ObjectPool(Instantiate instantiate)
        {
            this.instantiate = instantiate;
            pool = new List<ILazy>();
        }

        /// <summary>
        /// 预先创建
        /// </summary>
        /// <param name="count">数量</param>
        public void PreAllocate(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ILazy instance = instantiate();
                ReturnInstance(instance);
            }
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns>实例</returns>
        public ILazy GetInstance()
        {
            if (pool.Any())
            {
                ILazy instance = pool.First();
                pool.Remove(instance);
                instance.SetActive(true);
                return instance;
            }
            return instantiate();
        }

        /// <summary>
        /// 返回实例
        /// </summary>
        /// <param name="instance">实例</param>
        public void ReturnInstance(ILazy instance)
        {
            instance.SetActive(false);
            pool.Add(instance);
        }
    }
}