namespace ZombieInfection
{
    using Destroy;

    internal class Program
    {
        private const int WIDTH = 50;
        private const int HEIGHT = 30;
        private static Graphics graphics;
        private static Physics physics;

        private static void Main()
        {
            graphics = new Graphics(WIDTH, HEIGHT, CharWidth.Double);
            physics = new Physics();
            RuntimeEngine.Construct(ConsoleType.Default, true, false, WIDTH * 2, HEIGHT);
            RuntimeEngine.Start(Start, Update, null, 60);
        }

        private static void Start()
        {
        }

        private static void Update()
        {
        }
    }
}