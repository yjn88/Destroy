namespace Destroy.UI
{
    using System.Collections.Generic;

    /// <summary>
    /// 文本框(会将文字进行排版显示)
    /// </summary>
    public class TextBox : UIObject
    {
        /// <summary>
        /// 图形网格集合
        /// </summary>
        public List<GraphicGrid> GraphicGrids;

        /// <summary>
        /// 宽度限制
        /// </summary>
        public int WidthLimit;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="manager">UI管理器</param>
        /// <param name="widthLimit">宽度限制(超出该宽度自动换行)</param>
        /// <param name="position">坐标</param>
        /// <param name="str">字符串</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        /// <param name="depth">深度</param>
        public TextBox(UIManager manager, int widthLimit, Vector2 position,
            string str, Colour foreColor, Colour backColor, uint depth = 0)
        {
            List<GraphicGrid> graphicGrids = null;
            Graphics graphics = manager.Graphics;
            manager.AddUIObject(this);

            if (graphics.CharWidth == CharWidth.Single)
            {
                graphicGrids = graphics.CreatGridByString1(
                    position, str, foreColor, backColor, depth);
            }
            else if (graphics.CharWidth == CharWidth.Double)
            {
                graphicGrids = graphics.CreatGridByString(
                    position, str, foreColor, backColor, depth);
            }

            foreach (GraphicGrid item in graphicGrids)
            {
                int tempX = item.Position.X;
                int indentX = 0;
                while (tempX / (widthLimit + position.X) > 0)
                {
                    tempX -= widthLimit;
                    indentX += widthLimit;
                    item.Position.Y++;
                }
                item.Position.X -= indentX;
            }

            WidthLimit = widthLimit;
            GraphicGrids = graphicGrids;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public override void Update()
        {
        }
    }
}