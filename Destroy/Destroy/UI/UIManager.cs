namespace Destroy.UI
{
    using System.Collections.Generic;

    /// <summary>
    /// UI管理器
    /// </summary>
    public class UIManager
    {
        /// <summary>
        /// UI物体数量
        /// </summary>
        public int UIObjectCount => objects.Count;

        /// <summary>
        /// 图形对象
        /// </summary>
        public Graphics Graphics;

        private List<UIObject> objects;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="graphics">图形对象</param>
        public UIManager(Graphics graphics)
        {
            Graphics = graphics;
            objects = new List<UIObject>();
        }

        /// <summary>
        /// 添加UI物体
        /// </summary>
        /// <param name="UIObject">UI物体</param>
        public void AddUIObject(UIObject UIObject)
        {
            objects.Add(UIObject);
        }

        /// <summary>
        /// 移除UI物体
        /// </summary>
        /// <param name="UIObject"></param>
        public void RemoveUIObject(UIObject UIObject)
        {
            objects.Remove(UIObject);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            foreach (UIObject item in objects)
            {
                item.Update();
            }
        }
    }
}