namespace Destroy.UI
{
    using System.Collections.Generic;

    /// <summary>
    /// 对话框(用于游戏中的对话显示)
    /// </summary>
    public class DialogBox : UIObject
    {
        /// <summary>
        /// 图形网格集合
        /// </summary>
        public List<GraphicGrid> GraphicGrids;

        private float interval;

        private float timer;

        private int count;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="graphics">图形对象</param>
        /// <param name="time">播放总时间</param>
        /// <param name="widthLimit">宽度限制(超出该宽度自动换行)</param>
        /// <param name="position">坐标</param>
        /// <param name="str">字符串</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        /// <param name="depth">深度</param>
        public DialogBox(Graphics graphics, float time, int widthLimit, Vector2 position,
            string str, Colour foreColor, Colour backColor, uint depth = 0)
        {
            TextBox textBox = new TextBox(graphics, widthLimit, position, str, foreColor, backColor, depth);

            foreach (GraphicGrid item in textBox.GraphicGrids)
            {
                item.Active = false;
            }

            GraphicGrids = textBox.GraphicGrids;
            interval = time / textBox.GraphicGrids.Count;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public override void Update()
        {
            timer += Time.DeltaTime;
            if (timer >= interval)
            {
                timer = 0;
                if (count < GraphicGrids.Count)
                {
                    GraphicGrids[count].Active = true;
                    count++;
                }
            }
        }
    }
}