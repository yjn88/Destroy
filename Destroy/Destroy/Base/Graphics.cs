namespace Destroy
{
    using Destroy.Kernel;
    using System.Collections.Generic;

    /// <summary>
    /// 图形容器
    /// </summary>
    public class GraphicContainer
    {
        /// <summary>
        /// 图形网格集合
        /// </summary>
        public List<GraphicGrid> GraphicGrids;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="graphicGrids">图形网格集合</param>
        public GraphicContainer(List<GraphicGrid> graphicGrids)
        {
            GraphicGrids = graphicGrids;
        }

        /// <summary>
        /// 增加图形网格集合的坐标
        /// </summary>
        /// <param name="position">坐标</param>
        public void AddPosition(Vector2 position)
        {
            foreach (GraphicGrid item in GraphicGrids)
            {
                item.Position += position;
            }
        }

        /// <summary>
        /// 设置图形网格集合的坐标
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="offset">偏移量</param>
        public void SetPosition(Vector2 position, Vector2 offset = default(Vector2))
        {
            foreach (GraphicGrid item in GraphicGrids)
            {
                item.Position = position;
                position += offset;
            }
        }

        /// <summary>
        /// 设置图形网格集合的颜色
        /// </summary>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        public void SetColor(Colour foreColor, Colour backColor)
        {
            foreach (GraphicGrid item in GraphicGrids)
            {
                item.Left.SetColor(foreColor, backColor);
                item.Right.SetColor(foreColor, backColor);
            }
        }

        /// <summary>
        /// 设置图形网格集合的深度
        /// </summary>
        /// <param name="depth">深度</param>
        public void SetDepth(uint depth)
        {
            foreach (GraphicGrid item in GraphicGrids)
            {
                item.Depth = depth;
            }
        }

        /// <summary>
        /// 设置图形网格集合是否激活
        /// </summary>
        /// <param name="active">是否激活</param>
        public void SetActive(bool active)
        {
            foreach (GraphicGrid item in GraphicGrids)
            {
                item.Active = active;
            }
        }

        /// <summary>
        /// 设置图形网格是否固定
        /// </summary>
        /// <param name="isFixed">是否固定</param>
        public void SetFixed(bool isFixed)
        {
            foreach (GraphicGrid item in GraphicGrids)
            {
                item.Fixed = isFixed;
            }
        }
    }

    /// <summary>
    /// 图形网格
    /// </summary>
    public class GraphicGrid
    {
        /// <summary>
        /// 坐标
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// 左边的字符信息
        /// </summary>
        public CharInfo Left;

        /// <summary>
        /// 右边的字符信息
        /// </summary>
        public CharInfo Right;

        /// <summary>
        /// 深度, 该值越大表示越后被渲染(因此可以覆盖之前的)
        /// </summary>
        public uint Depth;

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Active;

        /// <summary>
        /// 固定
        /// </summary>
        public bool Fixed;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="left">左边的字符信息</param>
        /// <param name="depth">深度</param>
        public GraphicGrid(Vector2 position, CharInfo left, uint depth = 0)
        {
            Position = position;
            Left = left;
            Right = new CharInfo('\0', left.ForeColor, left.BackColor);
            Depth = depth;
            Active = true;
            Fixed = true;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="left">左边的字符信息</param>
        /// <param name="right">右边的字符信息</param>
        /// <param name="depth">深度</param>
        public GraphicGrid(Vector2 position, CharInfo left, CharInfo right, uint depth = 0)
        {
            Position = position;
            Left = left;
            Right = right;
            Depth = depth;
            Active = true;
            Fixed = true;
        }
    }

    /// <summary>
    /// 图形
    /// </summary>
    public class Graphics
    {
        /// <summary>
        /// 原点位置
        /// </summary>
        public Vector2 OriginPositoin;

        /// <summary>
        /// 图形网格集合数量
        /// </summary>
        public int GridCount => grids.Count;

        /// <summary>
        /// 宽度
        /// </summary>
        public readonly short Width;

        /// <summary>
        /// 高度
        /// </summary>
        public readonly short Height;

        /// <summary>
        /// 字符宽度
        /// </summary>
        public readonly CharWidth CharWidth;

        /// <summary>
        /// 默认字符
        /// </summary>
        public char DefaultChar;

        /// <summary>
        /// 默认前进色与背景色
        /// </summary>
        public Colour DefaultForeColor, DefaultBackColor;

        private List<GraphicGrid> grids;

        private CharInfo[] infos;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="charWidth">字符宽度</param>
        public Graphics(short width, short height, CharWidth charWidth)
        {
            Width = width;
            Height = height;
            CharWidth = charWidth;
            grids = new List<GraphicGrid>();
            infos = new CharInfo[width * (int)charWidth * height];
            DefaultChar = '\0';
            DefaultForeColor = Colour.Gray;
            DefaultBackColor = Colour.Black;
        }

        /// <summary>
        /// 创建图形网格
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="left">左边的字符信息</param>
        /// <param name="depth">深度</param>
        /// <returns>图形网格</returns>
        public GraphicGrid CreatGrid(Vector2 position, CharInfo left, uint depth = 0)
        {
            GraphicGrid grid = new GraphicGrid(position, left, depth);
            grids.Add(grid);
            return grid;
        }

        /// <summary>
        /// 创建图形网格
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="left">左边的字符信息</param>
        /// <param name="right">右边的字符信息</param>
        /// <param name="depth">深度</param>
        /// <returns>图形网格</returns>
        public GraphicGrid CreatGrid(Vector2 position, CharInfo left, CharInfo right, uint depth = 0)
        {
            GraphicGrid grid = new GraphicGrid(position, left, right, depth);
            grids.Add(grid);
            return grid;
        }

        /// <summary>
        /// 根据字符串创建图形网格集合(适用于双宽模式)
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="str">字符串</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        /// <param name="depth">深度</param>
        /// <returns>图形网格集合</returns>
        public List<GraphicGrid> CreatGridByString(Vector2 position, string str, Colour foreColor, Colour backColor, uint depth = 0)
        {
            List<string> strGrids = DivideStringToGrids(str);
            List<GraphicGrid> graphicGrids = new List<GraphicGrid>();

            for (int i = 0; i < strGrids.Count; i++)
            {
                string item = strGrids[i];
                GraphicGrid graphicGrid = null;
                Vector2 pos = new Vector2(position.X + i, position.Y);

                if (item.Length == 1)
                {
                    graphicGrid = CreatGrid(pos, new CharInfo(item[0], foreColor, backColor), depth);
                }
                else if (item.Length == 2)
                {
                    graphicGrid = CreatGrid(pos, new CharInfo(item[0], foreColor, backColor),
                        new CharInfo(item[1], foreColor, backColor), depth);
                }
                graphicGrids.Add(graphicGrid);
            }
            return graphicGrids;
        }

        /// <summary>
        /// 根据字符串创建图形网格集合(适用于双宽模式)
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="str">字符串</param>
        /// <param name="foreColors">前景色</param>
        /// <param name="backColors">背景色</param>
        /// <param name="depth">深度</param>
        /// <returns>图形网格集合</returns>
        public List<GraphicGrid> CreatGridByString(Vector2 position, string str, Colour[] foreColors, Colour[] backColors, uint depth = 0)
        {
            List<string> strGrids = DivideStringToGrids(str);
            List<GraphicGrid> graphicGrids = new List<GraphicGrid>();

            for (int i = 0; i < strGrids.Count; i++)
            {
                GraphicGrid graphicGrid = null;
                Vector2 pos = new Vector2(position.X + i, position.Y);

                string item = strGrids[i];
                Colour foreColor = foreColors[i];
                Colour backColor = backColors[i];

                if (item.Length == 1)
                {
                    graphicGrid = CreatGrid(pos, new CharInfo(item[0], foreColor, backColor), depth);
                }
                else if (item.Length == 2)
                {
                    graphicGrid = CreatGrid(pos, new CharInfo(item[0], foreColor, backColor),
                        new CharInfo(item[1], foreColor, backColor), depth);
                }
                graphicGrids.Add(graphicGrid);
            }
            return graphicGrids;
        }

        /// <summary>
        /// 根据字符串数组创建图形网格集合(适用于双宽模式)
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="strs">字符串数组</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        /// <param name="depth">深度</param>
        /// <returns>图形网格集合</returns>
        public List<GraphicGrid> CreatGridByStrings(Vector2 position, string[] strs, Colour foreColor, Colour backColor, uint depth = 0)
        {
            List<GraphicGrid> graphicGrids = new List<GraphicGrid>();
            for (int i = 0; i < strs.Length; i++)
            {
                Vector2 pos = new Vector2(position.X, position.Y + i);
                List<GraphicGrid> grids = CreatGridByString(pos, strs[i], foreColor, backColor, depth);
                graphicGrids.AddRange(grids);
            }
            return graphicGrids;
        }

        /// <summary>
        /// 根据字符串数组创建图形网格集合(适用于双宽模式)
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="strs">字符串数组</param>
        /// <param name="foreColors">前景色数组</param>
        /// <param name="backColors">背景色数组</param>
        /// <param name="depth">深度</param>
        /// <returns>图形网格集合</returns>
        public List<GraphicGrid> CreatGridByStrings(Vector2 position, string[] strs, Colour[] foreColors, Colour[] backColors, uint depth = 0)
        {
            List<GraphicGrid> graphicGrids = new List<GraphicGrid>();
            for (int i = 0; i < strs.Length; i++)
            {
                Vector2 pos = new Vector2(position.X, position.Y + i);
                Colour foreColor = foreColors[i];
                Colour backColor = backColors[i];
                List<GraphicGrid> grids = CreatGridByString(pos, strs[i], foreColor, backColor, depth);
                graphicGrids.AddRange(grids);
            }
            return graphicGrids;
        }

        /// <summary>
        /// 根据数组创建图形网格集合
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="strs">字符串数组</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        /// <param name="depth">深度</param>
        /// <returns>图形网格集合</returns>
        public List<GraphicGrid> CreatGridByArray2D(Vector2 position, string[,] strs, Colour foreColor, Colour backColor, uint depth = 0)
        {
            List<GraphicGrid> graphicGrids = new List<GraphicGrid>();
            for (int i = 0; i < strs.GetLength(0); i++)
            {
                for (int j = 0; j < strs.GetLength(1); j++)
                {
                    GraphicGrid graphicGrid = null;
                    Vector2 pos = new Vector2(position.X + j, position.Y + i);
                    string str = strs[i, j];

                    if (str.Length == 1)
                    {
                        graphicGrid = CreatGrid(pos, new CharInfo(str[0], foreColor, backColor), depth);
                    }
                    else if (str.Length == 2)
                    {
                        graphicGrid = CreatGrid
                        (
                            pos,
                            new CharInfo(str[0], foreColor, backColor),
                            new CharInfo(str[1], foreColor, backColor),
                            depth
                        );
                    }
                    graphicGrids.Add(graphicGrid);
                }
            }
            return graphicGrids;
        }

        /// <summary>
        /// 根据数组创建图形网格集合
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="strs">字符串数组</param>
        /// <param name="foreColors">前景色数组</param>
        /// <param name="backColors">背景色数组</param>
        /// <param name="depth">深度</param>
        /// <returns>图形网格集合</returns>
        public List<GraphicGrid> CreatGridByArray2D(Vector2 position, string[,] strs, Colour[,] foreColors, Colour[,] backColors, uint depth = 0)
        {
            List<GraphicGrid> graphicGrids = new List<GraphicGrid>();
            for (int i = 0; i < strs.GetLength(0); i++)
            {
                for (int j = 0; j < strs.GetLength(1); j++)
                {
                    GraphicGrid graphicGrid = null;
                    Vector2 pos = new Vector2(position.X + j, position.Y + i);
                    string str = strs[i, j];
                    Colour foreColor = foreColors[i, j];
                    Colour backColor = backColors[i, j];

                    if (str.Length == 1)
                    {
                        graphicGrid = CreatGrid(pos, new CharInfo(str[0], foreColor, backColor), depth);
                    }
                    else if (str.Length == 2)
                    {
                        graphicGrid = CreatGrid
                        (
                            pos,
                            new CharInfo(str[0], foreColor, backColor),
                            new CharInfo(str[1], foreColor, backColor),
                            depth
                        );
                    }
                    graphicGrids.Add(graphicGrid);
                }
            }
            return graphicGrids;
        }

        /// <summary>
        /// 创建矩形
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="size">尺寸</param>
        /// <param name="str">填充字符串</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        /// <param name="depth">深度</param>
        /// <returns>图形网格集合</returns>
        public List<GraphicGrid> CreatRectangle
        (
            Vector2 position, Vector2 size,
            string str,
            Colour foreColor, Colour backColor,
            uint depth = 0
        )
        {
            List<GraphicGrid> graphicGrids = new List<GraphicGrid>();

            for (int i = 0; i < size.Y; i++)
            {
                for (int j = 0; j < size.X; j++)
                {
                    GraphicGrid graphicGrid = null;
                    Vector2 pos = new Vector2(position.X + j, position.Y + i);

                    if (str.Length == 1)
                    {
                        graphicGrid = CreatGrid
                        (
                            pos,
                            new CharInfo(str[0], foreColor, backColor),
                            depth
                        );
                    }
                    else if (str.Length == 2)
                    {
                        graphicGrid = CreatGrid
                        (
                            pos,
                            new CharInfo(str[0], foreColor, backColor),
                            new CharInfo(str[1], foreColor, backColor),
                            depth
                        );
                    }

                    graphicGrids.Add(graphicGrid);
                }
            }

            return graphicGrids;
        }

        /// <summary>
        /// 创建矩形
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="size">尺寸</param>
        /// <param name="borderSize">边框尺寸</param>
        /// <param name="inside">内部填充字符串</param>
        /// <param name="insideForeColor">内部填充字符串前景色</param>
        /// <param name="insideBackColor">内部填充字符串背景色</param>
        /// <param name="border">边框填充字符串</param>
        /// <param name="borderForeColor">边框填充字符串前景色</param>
        /// <param name="borderBackColor">边框填充字符串背景色</param>
        /// <param name="insideGraphicGrids">内部填充图形网格集合</param>
        /// <param name="borderGraphicGrids">边框填充图形网格集合</param>
        /// <param name="depth">深度</param>
        public void CreatRectangle
        (
            Vector2 position, Vector2 size, Vector2 borderSize,
            string inside, Colour insideForeColor, Colour insideBackColor,
            string border, Colour borderForeColor, Colour borderBackColor,
            out List<GraphicGrid> insideGraphicGrids,
            out List<GraphicGrid> borderGraphicGrids,
            uint depth = 0
        )
        {
            insideGraphicGrids = new List<GraphicGrid>();
            borderGraphicGrids = new List<GraphicGrid>();

            for (int i = 0; i < size.Y; i++)
            {
                for (int j = 0; j < size.X; j++)
                {
                    GraphicGrid graphicGrid = null;
                    Vector2 pos = new Vector2(position.X + j, position.Y + i);

                    if (i >= 0 && i < borderSize.Y || j >= 0 && j < borderSize.X ||
                        i <= size.Y - 1 && i > size.Y - 1 - borderSize.Y ||
                        j <= size.X - 1 && j > size.X - 1 - borderSize.X)
                    {
                        if (border.Length == 1)
                        {
                            graphicGrid = CreatGrid
                            (
                                pos,
                                new CharInfo(border[0], borderForeColor, borderBackColor),
                                depth
                            );
                        }
                        else if (border.Length == 2)
                        {
                            graphicGrid = CreatGrid
                            (
                                pos,
                                new CharInfo(border[0], borderForeColor, borderBackColor),
                                new CharInfo(border[1], borderForeColor, borderBackColor),
                                depth
                            );
                        }

                        borderGraphicGrids.Add(graphicGrid);
                    }
                    else
                    {
                        if (inside.Length == 1)
                        {
                            graphicGrid = CreatGrid
                            (
                                pos,
                                new CharInfo(inside[0], insideForeColor, insideBackColor),
                                depth
                            );
                        }
                        else if (inside.Length == 2)
                        {
                            graphicGrid = CreatGrid
                            (
                                pos,
                                new CharInfo(inside[0], insideForeColor, insideBackColor),
                                new CharInfo(inside[1], insideForeColor, insideBackColor),
                                depth
                            );
                        }

                        insideGraphicGrids.Add(graphicGrid);
                    }
                }
            }
        }

        /// <summary>
        /// 设置图形网格
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="left">左边的字符信息</param>
        public void SetGrid(Vector2 position, CharInfo left)
        {
            if (position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height)
            {
                int index = Width * (int)CharWidth * position.Y + position.X * (int)CharWidth;

                if (CharWidth == CharWidth.Single)
                {
                    infos[index] = left;
                }
                else if (CharWidth == CharWidth.Double)
                {
                    infos[index] = left;
                    infos[index + 1] = new CharInfo('\0', left.ForeColor, left.BackColor);
                }
            }
        }

        /// <summary>
        /// 设置图形网格
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="left">左边的字符信息</param>
        /// <param name="right">右边的字符信息</param>
        public void SetGrid(Vector2 position, CharInfo left, CharInfo right)
        {
            if (position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height)
            {
                int index = Width * (int)CharWidth * position.Y + position.X * (int)CharWidth;

                if (CharWidth == CharWidth.Single)
                {
                    infos[index] = left;
                }
                else if (CharWidth == CharWidth.Double)
                {
                    infos[index] = left;
                    infos[index + 1] = right;
                }
            }
        }

        /// <summary>
        /// 根据字符串设置图形网格集合(适用于双宽模式)
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="str">字符串</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        public void SetGridByString(Vector2 position, string str, Colour foreColor, Colour backColor)
        {
            List<string> strGrids = DivideStringToGrids(str);

            for (int i = 0; i < strGrids.Count; i++)
            {
                string item = strGrids[i];
                Vector2 pos = new Vector2(position.X + i, position.Y);

                if (item.Length == 1)
                {
                    SetGrid(pos, new CharInfo(item[0], foreColor, backColor));
                }
                else if (item.Length == 2)
                {
                    SetGrid(pos,
                        new CharInfo(item[0], foreColor, backColor),
                        new CharInfo(item[1], foreColor, backColor));
                }
            }
        }

        /// <summary>
        /// 根据字符串设置图形网格集合(适用于双宽模式)
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="str">字符串</param>
        /// <param name="foreColors">前景色</param>
        /// <param name="backColors">背景色</param>
        public void SetGridByString(Vector2 position, string str, Colour[] foreColors, Colour[] backColors)
        {
            List<string> strGrids = DivideStringToGrids(str);

            for (int i = 0; i < strGrids.Count; i++)
            {
                string item = strGrids[i];
                Vector2 pos = new Vector2(position.X + i, position.Y);
                Colour foreColor = foreColors[i];
                Colour backColor = backColors[i];

                if (item.Length == 1)
                {
                    SetGrid(pos, new CharInfo(item[0], foreColor, backColor));
                }
                else if (item.Length == 2)
                {
                    SetGrid(pos,
                        new CharInfo(item[0], foreColor, backColor),
                        new CharInfo(item[1], foreColor, backColor));
                }
            }
        }

        /// <summary>
        /// 根据字符串数组设置图形网格集合(适用于双宽模式)
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="strs">字符串</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        public void SetGridByStrings(Vector2 position, string[] strs, Colour foreColor, Colour backColor)
        {
            for (int i = 0; i < strs.Length; i++)
            {
                Vector2 pos = new Vector2(position.X, position.Y + i);
                SetGridByString(pos, strs[i], foreColor, backColor);
            }
        }

        /// <summary>
        /// 根据字符串数组设置图形网格集合(适用于双宽模式)
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="strs">字符串</param>
        /// <param name="foreColors">前景色</param>
        /// <param name="backColors">背景色</param>
        public void SetGridByStrings(Vector2 position, string[] strs, Colour[] foreColors, Colour[] backColors)
        {
            for (int i = 0; i < strs.Length; i++)
            {
                Vector2 pos = new Vector2(position.X, position.Y + i);
                Colour foreColor = foreColors[i];
                Colour backColor = backColors[i];
                SetGridByString(pos, strs[i], foreColor, backColor);
            }
        }

        /// <summary>
        /// 根据数组设置图形网格集合
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="strs">字符串</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        public void SetGridByArray2D(Vector2 position, string[,] strs, Colour foreColor, Colour backColor)
        {
            for (int i = 0; i < strs.GetLength(0); i++)
            {
                for (int j = 0; j < strs.GetLength(1); j++)
                {
                    Vector2 pos = new Vector2(position.X + j, position.Y + i);
                    string str = strs[i, j];

                    if (str.Length == 1)
                    {
                        SetGrid(pos, new CharInfo(str[0], foreColor, backColor));
                    }
                    else if (str.Length == 2)
                    {
                        SetGrid(pos,
                            new CharInfo(str[0], foreColor, backColor),
                            new CharInfo(str[1], foreColor, backColor));
                    }
                }
            }
        }

        /// <summary>
        /// 根据数组设置图形网格集合
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="strs">字符串</param>
        /// <param name="foreColors">前景色</param>
        /// <param name="backColors">背景色</param>
        public void SetGridByArray2D(Vector2 position, string[,] strs, Colour[,] foreColors, Colour[,] backColors)
        {
            for (int i = 0; i < strs.GetLength(0); i++)
            {
                for (int j = 0; j < strs.GetLength(1); j++)
                {
                    Vector2 pos = new Vector2(position.X + j, position.Y + i);
                    string str = strs[i, j];
                    Colour foreColor = foreColors[i, j];
                    Colour backColor = backColors[i, j];

                    if (str.Length == 1)
                    {
                        SetGrid(pos, new CharInfo(str[0], foreColor, backColor));
                    }
                    else if (str.Length == 2)
                    {
                        SetGrid(pos,
                            new CharInfo(str[0], foreColor, backColor),
                            new CharInfo(str[1], foreColor, backColor));
                    }
                }
            }
        }

        /// <summary>
        /// 设置矩形
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="size">尺寸</param>
        /// <param name="str">填充字符串</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        public void SetRectangle(Vector2 position, Vector2 size, string str, Colour foreColor, Colour backColor)
        {
            for (int i = 0; i < size.Y; i++)
            {
                for (int j = 0; j < size.X; j++)
                {
                    Vector2 pos = new Vector2(position.X + j, position.Y + i);

                    if (str.Length == 1)
                    {
                        SetGrid(pos, new CharInfo(str[0], foreColor, backColor));
                    }
                    else if (str.Length == 2)
                    {
                        SetGrid(pos,
                            new CharInfo(str[0], foreColor, backColor),
                            new CharInfo(str[1], foreColor, backColor));
                    }
                }
            }
        }

        /// <summary>
        /// 设置矩形
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="size">尺寸</param>
        /// <param name="borderSize">边框尺寸</param>
        /// <param name="inside">内部填充字符串</param>
        /// <param name="insideForeColor">内部填充字符串前景色</param>
        /// <param name="insideBackColor">内部填充字符串背景色</param>
        /// <param name="border">边框填充字符串</param>
        /// <param name="borderForeColor">边框填充字符串前景色</param>
        /// <param name="borderBackColor">边框填充字符串背景色</param>
        public void SetRectangle
        (
            Vector2 position, Vector2 size, Vector2 borderSize,
            string inside, Colour insideForeColor, Colour insideBackColor,
            string border, Colour borderForeColor, Colour borderBackColor
        )
        {
            for (int i = 0; i < size.Y; i++)
            {
                for (int j = 0; j < size.X; j++)
                {
                    Vector2 pos = new Vector2(position.X + j, position.Y + i);

                    if (i >= 0 && i < borderSize.Y || j >= 0 && j < borderSize.X ||
                        i <= size.Y - 1 && i > size.Y - 1 - borderSize.Y ||
                        j <= size.X - 1 && j > size.X - 1 - borderSize.X)
                    {
                        if (border.Length == 1)
                        {
                            SetGrid(pos, new CharInfo(border[0], borderForeColor, borderBackColor));
                        }
                        else if (border.Length == 2)
                        {
                            SetGrid(pos,
                                new CharInfo(border[0], borderForeColor, borderBackColor),
                                new CharInfo(border[1], borderForeColor, borderBackColor));
                        }
                    }
                    else
                    {
                        if (inside.Length == 1)
                        {
                            SetGrid(pos, new CharInfo(inside[0], insideForeColor, insideBackColor));
                        }
                        else if (inside.Length == 2)
                        {
                            SetGrid(pos,
                                new CharInfo(inside[0], insideForeColor, insideBackColor),
                                new CharInfo(inside[1], insideForeColor, insideBackColor));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 添加图形网格
        /// </summary>
        /// <param name="graphicGrid">图形网格</param>
        public void AddGrid(GraphicGrid graphicGrid)
        {
            grids.Add(graphicGrid);
        }

        /// <summary>
        /// 添加图形网格集合
        /// </summary>
        /// <param name="graphicGrids">图形网格集合</param>
        public void AddGrid(List<GraphicGrid> graphicGrids)
        {
            grids.AddRange(graphicGrids);
        }

        /// <summary>
        /// 销毁图形网格
        /// </summary>
        /// <param name="graphicGrid">图形网格</param>
        public void DestroyGrid(GraphicGrid graphicGrid)
        {
            grids.Remove(graphicGrid);
        }

        /// <summary>
        /// 销毁图形网格集合
        /// </summary>
        /// <param name="graphicGrids">图形网格集合</param>
        public void DestroyGrid(List<GraphicGrid> graphicGrids)
        {
            foreach (GraphicGrid item in graphicGrids)
            {
                grids.Remove(item);
            }
        }

        /// <summary>
        /// 自由设置图形网格
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="charInfo">字符信息</param>
        public void SetGridFree(Vector2 position, CharInfo charInfo)
        {
            if (position.X >= 0 && position.X < Width * (int)CharWidth &&
                position.Y >= 0 && position.Y < Height)
            {
                int index = Width * (int)CharWidth * position.Y + position.X;
                infos[index] = charInfo;
            }
        }

        /// <summary>
        /// 清空图形网格集合
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i] = new CharInfo(DefaultChar, DefaultForeColor, DefaultBackColor);
            }
        }

        /// <summary>
        /// 预渲染(对图形网格集合进行预处理)
        /// </summary>
        public void PreRender()
        {
            Clear();

            grids.Sort((left, right) => left.Depth.CompareTo(right.Depth));

            foreach (GraphicGrid item in grids)
            {
                if (item.Active == false)
                {
                    continue;
                }
                Vector2 pos = Vector2.Zero;
                if (item.Fixed)
                {
                    pos = item.Position;
                }
                else
                {
                    pos = item.Position - OriginPositoin;
                }
                SetGrid(pos, item.Left, item.Right);
            }
        }

        /// <summary>
        /// 渲染(渲染的区域不能小于屏幕缓冲区, 否则渲染区域会被截断)
        /// </summary>
        /// <param name="x">横向坐标</param>
        /// <param name="y">纵向坐标</param>
        public void Render(short x = 0, short y = 0)
        {
            //控制台现在最多只能打印双宽字符, 不能打印三宽以上的字符(比如部分阿拉伯文), 并且不支持大部分表情与特殊符号。
            //可以只使用一个CHAR_INFO只打印一个中文的左半部分, 如果要打印一个完整的中文字符则需要两个CHAR_INFO, 并且必须保证第一个CHAR_INFO与第二个CHAR_INFO前景色与背景色一致, 第一个CHAR_INFO保存中文, 第二个CHAR_INFO保存'\0'空字符。
            //一旦第一个CHAR_INFO的中文被覆盖意味着这个中文字符无法显示。
            //一个中文的右半部分可以被隐去, 并且右半部分可以是一个英文或者空字符甚至可以是一个另外的中文的左半部分。
            //一个中文无法单独显示右半部分。
            KERNEL.WRITE_CONSOLE_OUTPUT(CONSOLE.OutputHandle, infos,
                x, y, (short)(Width * (int)CharWidth), Height);
        }

        /// <summary>
        /// 返回该字符的宽度
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns>字符宽度</returns>
        [Unstable]
        public static CharWidth GetCharWidth(char c)
        {
            if (c >= 0x2500 && c <= 0x257F) //ListBox中的特殊符号算双宽字符
            {
                return CharWidth.Double;
            }
            if (c >= 0x4e00 && c <= 0x9fbb) //只要不低于127都算双宽字符
            {
                return CharWidth.Double;
            }
            else
            {
                return CharWidth.Single;
            }
        }

        /// <summary>
        /// 分割字符串成一个个格子, 单宽字符占据半个格子, 
        /// 双宽字符会占据一个格子, 如果单宽后面直接跟双宽, 会在单宽字符后面先补上一个空格
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>分割好的集合</returns>
        public static List<string> DivideStringToGrids(string str)
        {
            List<string> girds = new List<string>();
            int tempLength = 0;
            string temp = string.Empty;

            foreach (char item in str)
            {
                if (GetCharWidth(item) == CharWidth.Single)
                {
                    if (tempLength == 0) //暂存
                    {
                        tempLength = 1;
                        temp = item.ToString();
                    }
                    else
                    {
                        tempLength = 0;
                        temp += item.ToString();
                        girds.Add(temp);
                        temp = string.Empty;
                    }
                }
                else if (GetCharWidth(item) == CharWidth.Double)
                {
                    if (tempLength == 1) //如果当前有缓存,那么先存储一个空格
                    {
                        tempLength = 0;
                        temp += " ";
                        girds.Add(temp);
                        temp = string.Empty;
                    }
                    //再存储
                    girds.Add(item.ToString());
                }
            }
            if (tempLength == 1)
            {
                tempLength = 0;
                temp += " ";
                girds.Add(temp);
                temp = string.Empty;
            }
            return girds;
        }
    }
}