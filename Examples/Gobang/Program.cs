namespace Gobang
{
    using Destroy;
    using Destroy.Kernel;
    using Destroy.UI;
    using System.Collections.Generic;
    using System.Text;

    internal class Program
    {
        public enum ChessType
        {
            None,
            Black,
            White,
        }

        public class Chess
        {
            public ChessType ChessType;
            private GraphicGrid graphicGrid;
            private Button button;

            public Chess(GraphicGrid graphicGrid)
            {
                this.graphicGrid = graphicGrid;
                button = new Button(graphicGrid, CharWidth.Double);
                //增强鼠标灵敏度
                button.Touch = true;
                //设置悬浮特效
                button.OnEnter += () =>
                {
                    graphicGrid.SetColor(graphicGrid.Left.ForeColor, Colour.Gray);
                };
                button.OnLeave += () =>
                {
                    graphicGrid.SetColor(graphicGrid.Left.ForeColor, Colour.DarkGray);
                };
                //下棋
                button.OnClick += () =>
                {
                    Vector2 pos = graphicGrid.Position - ChessBoardPos;

                    if (Black)
                    {
                        if (Chesses[pos.Y, pos.X].ChessType == ChessType.None)
                        {
                            //改变字符并且改变颜色
                            graphicGrid.Left.UnicodeChar = Assets.SOLID_CIRCLE;
                            graphicGrid.SetColor(Colour.Black, graphicGrid.Left.BackColor);
                            //改变状态
                            ChessType = ChessType.Black;
                        }
                    }
                    else
                    {
                        if (Chesses[pos.Y, pos.X].ChessType == ChessType.None)
                        {
                            graphicGrid.Left.UnicodeChar = Assets.SOLID_CIRCLE;
                            graphicGrid.SetColor(Colour.White, graphicGrid.Left.BackColor);
                            //改变状态
                            ChessType = ChessType.White;
                        }
                    }
                    //交替下棋
                    Black = !Black;
                };
                ChessType = ChessType.None;
            }

            public void Update()
            {
                button.Update();
            }
        }

        //黑棋先走
        public static bool Black = true;
        public const int WIDTH = 15;
        public const int HEIGHT = 15;
        public const int FPS = 200;
        public static Vector2 ChessBoardPos = new Vector2(8, 8);
        public static Chess[,] Chesses = new Chess[HEIGHT, WIDTH];

        public static void Main()
        {
            Graphics graphics = RuntimeEngine.Construct2
            (
                consoleType: ConsoleType.Chinese,
                bold: false,
                maximum: false,
                width: 30,
                height: 30,
                charWidth: CharWidth.Double,
                title: "Gobang"
            );

            void Start()
            {
                List<string> lines = new List<string>();

                #region 构造棋盘字符

                string topLine = string.Empty;
                string middleLine = string.Empty;
                string bottomLine = string.Empty;
                StringBuilder builder = new StringBuilder();
                int width = WIDTH - 2;
                int height = HEIGHT - 2;
                //第一行
                builder.Append("┌");
                for (int i = 0; i < width; i++)
                {
                    builder.Append("┬");
                }
                builder.Append("┐");
                topLine = builder.ToString();
                builder.Clear();
                //中间行
                builder.Append("├");
                for (int i = 0; i < width; i++)
                {
                    builder.Append("┼");
                }
                builder.Append("┤");
                middleLine = builder.ToString();
                builder.Clear();
                //最后一行
                builder.Append("└");
                for (int i = 0; i < width; i++)
                {
                    builder.Append("┴");
                }
                builder.Append("┘");
                bottomLine = builder.ToString();
                builder.Clear();
                //拼接行
                lines.Add(topLine);
                for (int i = 0; i < height; i++)
                {
                    lines.Add(middleLine);
                }
                lines.Add(bottomLine);

                #endregion

                //构造棋盘二维数组
                for (int i = 0; i < Chesses.GetLength(0); i++)
                {
                    char[] charArray = lines[i].ToCharArray();
                    for (int j = 0; j < Chesses.GetLength(1); j++)
                    {
                        //创建棋子
                        Chesses[i, j] = new Chess(
                            graphics.CreatGrid(
                            new Vector2(ChessBoardPos.X + j, ChessBoardPos.Y + i),
                            new CharInfo(charArray[j], Colour.Black, Colour.DarkGray))
                        );
                    }
                }
            }

            void Update()
            {
                foreach (Chess item in Chesses)
                {
                    item.Update();
                }
                graphics.PreRender();
                graphics.Render();
            }

            RuntimeEngine.Start(Start, Update, null, FPS);
        }
    }
}