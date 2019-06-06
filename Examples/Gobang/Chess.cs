﻿namespace Gobang
{
    using Destroy;
    using Destroy.Kernel;
    using Destroy.UI;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public enum ChessType
    {
        None,
        Black,
        White,
    }

    public enum GameResult
    {
        /// <summary>
        /// 下棋失败(在已经有棋子的地方下棋)
        /// </summary>
        Fail,

        /// <summary>
        /// 无结果, 这一步是黑棋走的(没有胜利者产生)
        /// </summary>
        Black,

        /// <summary>
        /// 无结果, 这一步是白棋走的(没有胜利者产生)
        /// </summary>
        White,

        /// <summary>
        /// 黑棋胜利
        /// </summary>
        BlackWin,

        /// <summary>
        /// 白棋胜利
        /// </summary>
        WhiteWin,

        /// <summary>
        /// 棋盘下满, 这一步是黑棋走的(没有胜利者产生)
        /// </summary>
        BlackDraw,

        /// <summary>
        /// 棋盘下满, 这一步是白棋走的(没有胜利者产生)
        /// </summary>
        WhiteDraw,
    }

    public class Chessboard
    {
        public Graphics Graphics;

        public Vector2 Position;

        public Chess[,] Chesses;

        public int Width;

        public int Height;

        public int StepToWin;

        /// <summary>
        /// 是否是黑棋的回合(默认黑棋优先)
        /// </summary>
        public bool RoundOfBlack;

        /// <summary>
        /// 已经下在棋盘上的棋子数量
        /// </summary>
        public int ChessNumber;

        /// <summary>
        /// 是否继续游戏
        /// </summary>
        public bool Playing;

        public Chessboard(Graphics graphics, Vector2 position, int width, int height,
            int stepToWin, Colour foreColor, Colour backColor)
        {
            Graphics = graphics;
            Position = position;
            Chesses = new Chess[height, width];
            Width = width;
            Height = height;
            StepToWin = stepToWin;
            RoundOfBlack = true;
            ChessNumber = 0;
            Playing = true;

            string[] chessboardStrs = CreatChessboard(width, height);

            for (int i = 0; i < height; i++)
            {
                char[] charArray = chessboardStrs[i].ToCharArray();
                for (int j = 0; j < width; j++)
                {
                    Chesses[i, j] = new Chess
                    (
                        this,
                        graphics.CreatGrid
                        (
                            new Vector2(Position.X + j, Position.Y + i),
                            new CharInfo(charArray[j], foreColor, backColor)
                        )
                    );
                }
            }
        }

        public void Update()
        {
            foreach (Chess item in Chesses)
            {
                item.Update();
            }
        }

        /// <summary>
        /// 创建棋盘
        /// </summary>
        /// <param name="width">棋盘宽度</param>
        /// <param name="height">棋盘高度</param>
        /// <returns>字符串数组</returns>
        public static string[] CreatChessboard(int width, int height)
        {
            width -= 2;
            height -= 2;

            List<string> lines = new List<string>();

            string topLine = string.Empty;
            string middleLine = string.Empty;
            string bottomLine = string.Empty;
            StringBuilder builder = new StringBuilder();

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

            return lines.ToArray();
        }

        public GameResult Calculate(Vector2 position)
        {
            Chess chess = Chesses[position.Y, position.X];

            //下棋失败
            if (!Playing || chess.ChessType != ChessType.None)
            {
                return GameResult.Fail;
            }
            //下棋成功
            else
            {
                ChessNumber++;
                if (RoundOfBlack)
                {
                    chess.ChessType = ChessType.Black;
                }
                else
                {
                    chess.ChessType = ChessType.White;
                }
            }

            #region 以前的老项目搬运过来的算法

            //遍历棋盘
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    //各方向连线
                    int horizontal = 1, vertical = 1, obliqueLine_1 = 1, obliqueLine_2 = 1;

                    ChessType current = Chesses[i, j].ChessType;

                    if (current != chess.ChessType)
                    {
                        continue;
                    }

                    //判断n连
                    for (int link = 1; link < StepToWin; link++)
                    {
                        //扫描横线
                        if (i + link < Height)
                        {
                            if (current == Chesses[i + link, j].ChessType)
                            {
                                horizontal++;
                            }
                        }
                        //扫描竖线
                        if (j + link < Width)
                        {
                            if (current == Chesses[i, j + link].ChessType)
                            {
                                vertical++;
                            }
                        }
                        //扫描右上斜线
                        if (i + link < Height && j + link < Width)
                        {
                            if (current == Chesses[i + link, j + link].ChessType)
                            {
                                obliqueLine_1++;
                            }
                        }
                        //扫描右下斜线
                        if (i + link < Height && j - link >= 0)
                        {
                            if (current == Chesses[i + link, j - link].ChessType)
                            {
                                obliqueLine_2++;
                            }
                        }
                    }

                    //胜利者产生
                    if (horizontal == StepToWin || vertical == StepToWin ||
                        obliqueLine_1 == StepToWin || obliqueLine_2 == StepToWin)
                    {
                        Playing = false;
                        if (RoundOfBlack)
                        {
                            return GameResult.BlackWin;
                        }
                        else
                        {
                            return GameResult.WhiteWin;
                        }
                    }
                }
            }

            //平局产生
            if (ChessNumber == Chesses.Length)
            {
                Playing = false;
                if (RoundOfBlack)
                {
                    return GameResult.BlackDraw;
                }
                else
                {
                    return GameResult.WhiteDraw;
                }
            }

            //无结果, 只是黑棋或者白棋下了一步棋, 继续交替下棋
            if (RoundOfBlack)
            {
                RoundOfBlack = !RoundOfBlack;
                return GameResult.Black;
            }
            else
            {
                RoundOfBlack = !RoundOfBlack;
                return GameResult.White;
            }

            #endregion
        }
    }

    public class Chess
    {
        public ChessType ChessType;

        public Vector2 Position => graphicGrid.Position;

        public Vector2 PositionOnChessboard => Position - chessboard.Position;

        private Chessboard chessboard;

        private GraphicGrid graphicGrid;

        private Button button;

        public Chess(Chessboard chessboard, GraphicGrid graphicGrid)
        {
            ChessType = ChessType.None;
            this.chessboard = chessboard;
            this.graphicGrid = graphicGrid;
            button = new Button(graphicGrid, CharWidth.Double);
            button.Touch = true;
            button.OnEnter += Enter;
            button.OnLeave += Leave;
            button.OnClick += Click;
        }

        public void Update()
        {
            button.Update();
        }

        private void Enter()
        {
            graphicGrid.SetColor(graphicGrid.Left.ForeColor, Colour.Gray);
        }

        private void Leave()
        {
            graphicGrid.SetColor(graphicGrid.Left.ForeColor, Gobang.ChessboardBackColor);
        }

        private void Click()
        {
            GameResult gameResult = chessboard.Calculate(PositionOnChessboard);

            void CreatBlackChess()
            {
                //改变字符并且改变前景色
                graphicGrid.Left.UnicodeChar = Assets.SOLID_CIRCLE;
                graphicGrid.SetColor(Colour.Black, graphicGrid.Left.BackColor);
            }

            void CreatWhiteChess()
            {
                //改变字符并且改变前景色
                graphicGrid.Left.UnicodeChar = Assets.SOLID_CIRCLE;
                graphicGrid.SetColor(Colour.White, graphicGrid.Left.BackColor);
            }

            void PlaySovietNationalAnthem()
            {
                DirectoryInfo info = new DirectoryInfo(Environment.CurrentDirectory);
                string resources = Path.Combine(info.Parent.Parent.FullName, "Resources");
                string path = Path.Combine(resources, "CCCP_1977.mp3");

                AUDIO.OPEN(path);
                AUDIO.SET_VOLUME(path, 50);
                AUDIO.PLAY(path, true);
                AUDIO.CLOSE(path);
            }

            switch (gameResult)
            {
                case GameResult.Fail:
                    {
                        return;
                    }
                case GameResult.Black:
                    {
                        CreatBlackChess();
                    };
                    break;
                case GameResult.White:
                    {
                        CreatWhiteChess();
                    }
                    break;
                case GameResult.BlackWin:
                    {
                        CreatBlackChess();
                        PlaySovietNationalAnthem();
                    }
                    break;
                case GameResult.WhiteWin:
                    {
                        CreatWhiteChess();
                        PlaySovietNationalAnthem();
                    }
                    break;
                case GameResult.BlackDraw:
                    {
                        CreatBlackChess();
                    }
                    break;
                case GameResult.WhiteDraw:
                    {
                        CreatWhiteChess();
                    }
                    break;
            }
        }
    }
}