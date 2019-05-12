namespace Destroy
{
    using System;
    using System.Text;
    using System.Threading;
    using Destroy.Kernel;

    /// <summary>
    /// 运行时激活
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EnableOnRuntime : Attribute
    {
    }

    /// <summary>
    /// 运行时引擎, 接管整个游戏的生命周期 <see langword="static"/>
    /// </summary>
    public static class RuntimeEngine
    {
        private static bool run;

        /// <summary>
        /// 构建控制台
        /// </summary>
        /// <param name="consoleType">控制台类型</param>
        /// <param name="maximum">最大化</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="title">标题</param>
        /// <returns>是否成功</returns>
        public static void Construct(ConsoleType consoleType, bool maximum, short width, short height, string title = "CONSOLE")
        {
            //以下2方法必须在设置字体前使用:
            //设置标准调色盘
            CONSOLE.SetStdConsolePalette(CONSOLE.OutputHandle);
            //设置标准控制台模式
            CONSOLE.SetStdConsoleMode(CONSOLE.OutputHandle, CONSOLE.InputHandle);
            //65001 = UTF8
            KERNEL.SET_CONSOLE_CP((uint)Encoding.UTF8.CodePage);
            KERNEL.SET_CONSOLE_OUTPUT_CP((uint)Encoding.UTF8.CodePage);
            CONSOLE.Title = title;

            switch (consoleType)
            {
                case ConsoleType.Default:
                    SetFontAndWindow(width, height, maximum, "Consolas", 16, 16);
                    break;
                case ConsoleType.Pixel:
                    SetFontAndWindow(width, height, maximum, "Terminal", 8, 8);
                    break;
                case ConsoleType.HignQuality:
                    SetFontAndWindow(width, height, maximum, "MS Gothic", 1, 1);
                    break;
            }
            if (maximum)
            {
                KERNEL.SET_WINDOW_POS(0, 0);
            }
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        public static void Start(Action onStart, Action onUpdate, Action onDestroy, int fps)
        {
            run = true;
            //这一帧距离上一帧的时间(单位:秒)
            float deltaTime = 0;
            //每帧应该使用的时间(单位:毫秒)
            long tickTime = 1000 / fps;
            //代码一帧运行花费的时间(单位:毫秒)
            long timeCost = 0;

            //初始化游戏
            onStart?.Invoke();

            while (run)
            {
                KERNEL.START_TIMING(out long freq, out long start);

                //每帧执行一次, 防止控制台窗口大小变化时光标再次出现
                KERNEL.SET_CONSOLE_CURSOR_INFO(CONSOLE.OutputHandle, false, 1);
                Time.DeltaTime = deltaTime;         //赋值DeltaTime
                Time.TotalTime += Time.DeltaTime;   //赋值TotalTime
                Input.CheckMouseState();            //检测鼠标状态
                onUpdate?.Invoke();                 //每帧更新游戏
                Input.CheckKeyboardState();         //检测键盘状态

                KERNEL.END_TIMING(freq, start, out timeCost);

                while (timeCost < tickTime)
                {
                    Thread.Sleep(0);                //短暂让出线程防止死循环
                    KERNEL.END_TIMING(freq, start, out timeCost);
                }

                deltaTime = (float)timeCost / 1000;
            }

            //结束游戏
            onDestroy?.Invoke();
        }

        /// <summary>
        /// 退出游戏
        /// </summary>
        public static void Exit()
        {
            run = false;
        }

        /// <summary>
        /// Warning:该方法不稳定, 出现错误时需要手动设置(屏幕缓冲区, 窗口大小, 字体大小等)
        /// </summary>
        private static void SetFontAndWindow(short width, short height, bool maximum, string fontName, short fontWidth, short fontHeight)
        {
            CONSOLE.SetConsoleFont(CONSOLE.OutputHandle, false, fontWidth, fontHeight, fontName);

            KERNEL.GET_LARGEST_CONSOLE_WINDOW_SIZE(CONSOLE.OutputHandle,
                    out short largestWidth, out short largestHeight);

            if (maximum)
            {
                width = largestWidth;
                height = largestHeight;
            }
            else if (width > largestWidth || height > largestHeight)
            {
                throw new Exception("specific width/height is too big!");
            }

            KERNEL.SET_CONSOLE_WINDOW_SIZE(CONSOLE.OutputHandle, 1, 1);
            KERNEL.SET_CONSOLE_BUFFER_SIZE(CONSOLE.OutputHandle, width, height);
            KERNEL.SET_CONSOLE_WINDOW_SIZE(CONSOLE.OutputHandle, width, height);
        }
    }
}