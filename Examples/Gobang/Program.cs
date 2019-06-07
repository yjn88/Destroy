namespace Gobang
{
    using Destroy;
    using Destroy.Kernel;
    using Destroy.UI;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Gobang : IPlayable
    {
        //==========可手动修改的字段==========
        public const int WIDTH = 30;
        public const int HEIGHT = 30;
        public const int CHESSBOARD_WIDTH = 15;
        public const int CHESSBOARD_HEIGHT = 15;
        public const int STEP_TO_WIN = 5;
        public const int FPS = 200;
        public const string TITLE = "Gobang";
        public static Colour ChessboardForeColor = Colour.Black;
        public static Colour ChessboardBackColor = Colour.DarkGray;

        //==========不可手动修改的字段==========
        private Vector2 chessboardPos =
            new Vector2((WIDTH - CHESSBOARD_WIDTH) / 2 + 1, (HEIGHT - CHESSBOARD_HEIGHT) / 2 + 1);
        private Graphics graphics;
        private UIManager manager;
        private Chessboard chessboard;

        //==========字段==========
        public NetworkRole NetworkRole = NetworkRole.None;
        public bool Playing = true;

        public void Play()
        {
            graphics = RuntimeEngine.Construct2
            (
                consoleType: ConsoleType.Chinese,
                bold: false,
                maximum: false,
                width: WIDTH,
                height: HEIGHT,
                charWidth: CharWidth.Double,
                title: TITLE
            );
            RuntimeEngine.Start(Start, Update, null, FPS);
        }

        private void Start()
        {
            manager = new UIManager(graphics);
            string[] buttonTexts = new string[]
            {
                "开启Host模式",
                "开启Client模式",
                "开启Server模式"
            };
            int index = 0;
            foreach (string item in buttonTexts)
            {
                List<GraphicGrid> graphicGrids = graphics.CreatGridByString(new Vector2(0, index), item,
                    Colour.DarkGray, Colour.Black);
                GraphicContainer container = new GraphicContainer(graphicGrids);

                Button button = new Button(manager, graphicGrids);
                button.OnEnter += () => ButtonOnEnter(container);
                button.OnLeave += () => ButtonOnLeave(container);
                button.OnClick += () => ButtonOnClick(container, index);

                index++;
            }

            chessboard = new Chessboard
            (
                graphics, manager, chessboardPos,
                CHESSBOARD_WIDTH, CHESSBOARD_HEIGHT, STEP_TO_WIN,
                ChessboardForeColor, ChessboardBackColor
            );
            //注册方法
            foreach (Chess item in chessboard.Chesses)
            {
                item.Button.OnClick += () =>
                {
                    ChessOnClick(item.GraphicGrid, item.PositionOnChessboard);
                };
                item.Button.OnEnter += () =>
                {
                    ChessOnEnter(item.GraphicGrid);
                };
                item.Button.OnLeave += () =>
                {
                    ChessOnLeave(item.GraphicGrid);
                };
            }
        }

        private void Update()
        {
            if (Playing)
            {
                manager.Update();
            }
            graphics.PreRender();
            graphics.Render();
        }

        private void ButtonOnEnter(GraphicContainer graphicGrid)
        {
            graphicGrid.SetColor(Colour.White, Colour.Black);
        }

        private void ButtonOnLeave(GraphicContainer graphicGrid)
        {
            graphicGrid.SetColor(Colour.DarkGray, Colour.Black);
        }

        private void ButtonOnClick(GraphicContainer graphicGrid, int index)
        {
            switch (index)
            {
                case 0:
                    NetworkRole = NetworkRole.Host;
                    break;
                case 1:
                    NetworkRole = NetworkRole.Client;
                    break;
                case 2:
                    NetworkRole = NetworkRole.Server;
                    break;
            }
            Playing = true;
        }

        private void ChessOnEnter(GraphicGrid graphicGrid)
        {
            graphicGrid.SetColor(graphicGrid.Left.ForeColor, Colour.Gray);
        }

        private void ChessOnLeave(GraphicGrid graphicGrid)
        {
            graphicGrid.SetColor(graphicGrid.Left.ForeColor, ChessboardBackColor);
        }

        private void ChessOnClick(GraphicGrid graphicGrid, Vector2 positionOnChessboard)
        {
            GameResult gameResult = chessboard.Calculate(positionOnChessboard);

            switch (gameResult)
            {
                case GameResult.Fail:
                    {
                        return;
                    }
                case GameResult.Black:
                    {
                        CreatBlackChess(graphicGrid);
                    };
                    break;
                case GameResult.White:
                    {
                        CreatWhiteChess(graphicGrid);
                    }
                    break;
                case GameResult.BlackWin:
                    {
                        CreatBlackChess(graphicGrid);
                        PlaySovietNationalAnthem(graphicGrid);
                    }
                    break;
                case GameResult.WhiteWin:
                    {
                        CreatWhiteChess(graphicGrid);
                        PlaySovietNationalAnthem(graphicGrid);
                    }
                    break;
                case GameResult.BlackDraw:
                    {
                        CreatBlackChess(graphicGrid);
                    }
                    break;
                case GameResult.WhiteDraw:
                    {
                        CreatWhiteChess(graphicGrid);
                    }
                    break;
            }
        }

        private void CreatBlackChess(GraphicGrid graphicGrid)
        {
            //改变字符并且改变前景色
            graphicGrid.Left.UnicodeChar = Assets.SOLID_CIRCLE;
            graphicGrid.SetColor(Colour.Black, graphicGrid.Left.BackColor);
        }

        private void CreatWhiteChess(GraphicGrid graphicGrid)
        {
            //改变字符并且改变前景色
            graphicGrid.Left.UnicodeChar = Assets.SOLID_CIRCLE;
            graphicGrid.SetColor(Colour.White, graphicGrid.Left.BackColor);
        }

        private void PlaySovietNationalAnthem(GraphicGrid graphicGrid)
        {
            DirectoryInfo info = new DirectoryInfo(Environment.CurrentDirectory);
            string resources = Path.Combine(info.Parent.Parent.FullName, "Resources");
            string path = Path.Combine(resources, "CCCP_1977.mp3");

            AUDIO.OPEN(path);
            AUDIO.SET_VOLUME(path, 91);
            AUDIO.PLAY(path, true);
        }
    }

    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Gobang gobang = new Gobang();
            gobang.Play();
        }
    }
}