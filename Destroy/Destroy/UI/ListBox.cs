namespace Destroy.UI
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// 列表(适用于双宽模式)
    /// </summary>
    public class ListBox : UIObject
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width;

        /// <summary>
        /// 高度
        /// </summary>
        public int Height;

        /// <summary>
        /// 行
        /// </summary>
        public List<GraphicContainer> Lines;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="graphics">图形对象(必须是双宽字符)</param>
        /// <param name="width">宽度(不能小于2)</param>
        /// <param name="height">高度(不能小于2)</param>
        /// <param name="position">坐标</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        /// <param name="depth">深度</param>
        public ListBox(Graphics graphics, int width, int height,
            Vector2 position, Colour foreColor, Colour backColor, uint depth = 0)
        {
            if (graphics.CharWidth != CharWidth.Double)
            {
                Error.Pop("只适用于双宽模式!");
            }
            if (width < 2 || height < 2)
            {
                Error.Pop("宽度或高度设置得太小!");
            }
            Width = width;
            Height = height;

            List<string> lines = new List<string>();
            StringBuilder builder = new StringBuilder();
            //构造第一行
            builder.Append("┌");
            for (int i = 0; i < width - 2; i++)
            {
                builder.Append("─");
            }
            builder.Append("┐");
            lines.Add(builder.ToString());
            //构造中间行
            builder.Clear();
            builder.Append("│");
            for (int i = 0; i < width - 2; i++)
            {
                builder.Append("  ");
            }
            builder.Append("│");
            string middleLine = builder.ToString();
            for (int i = 0; i < height - 2; i++)
            {
                lines.Add(middleLine);
            }
            //构造最后一行
            builder.Clear();
            builder.Append("└");
            for (int i = 0; i < width - 2; i++)
            {
                builder.Append("─");
            }
            builder.Append("┘");
            lines.Add(builder.ToString());

            List<GraphicGrid> graphicGrids =
                graphics.CreatGridByStrings(position, lines.ToArray(),
                foreColor, backColor, depth);

            List<GraphicGrid> border = new List<GraphicGrid>();
            List<GraphicGrid> inside = new List<GraphicGrid>();

            foreach (GraphicGrid item in graphicGrids)
            {
                //border
                if (item.Position.X == 0 || item.Position.X == width - 1 ||
                    item.Position.Y == 0 || item.Position.Y == height - 1)
                {
                    border.Add(item);
                }
                else
                {
                    inside.Add(item);
                }
            }
            //把内部所有的图形网格分割成一行行的
            List<GraphicContainer> insideLines = new List<GraphicContainer>();
            List<GraphicGrid> insideLine = new List<GraphicGrid>();

            int count = inside.Count / (height - 2);
            int counter = 1;
            foreach (GraphicGrid item in inside)
            {
                insideLine.Add(item);
                if (counter == count)
                {
                    insideLines.Add(new GraphicContainer(insideLine));
                    insideLine = new List<GraphicGrid>();
                    counter = 0;
                }
                counter++;
            }
            Lines = insideLines;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public override void Update()
        {
        }
    }
}