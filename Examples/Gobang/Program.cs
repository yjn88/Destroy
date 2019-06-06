namespace Gobang
{
    using Destroy;

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
        private Chessboard chessboard;

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
            chessboard = new Chessboard
            (
                graphics, chessboardPos,
                CHESSBOARD_WIDTH, CHESSBOARD_HEIGHT, STEP_TO_WIN,
                ChessboardForeColor, ChessboardBackColor
            );
        }

        private void Update()
        {
            chessboard.Update();
            graphics.PreRender();
            graphics.Render();
        }
    }

    public class Program
    {
        [System.STAThread]
        public static void Main()
        {
            Gobang gobang = new Gobang();
            gobang.Play();
        }
    }
}