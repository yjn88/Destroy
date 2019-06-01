namespace Destroy
{
    using Destroy.Kernel;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 标准资源 <see langword="static"/>
    /// </summary>
    public static class Assets
    {
        /// <summary>
        /// 播放MadeWithDestroy的动画
        /// </summary>
        /// <param name="center">是否居中显示</param>
        /// <param name="x">横向坐标</param>
        /// <param name="y">纵向坐标</param>
        /// <param name="consoleKey">按键</param>
        public static void MadeWithDestroy(bool center, short x, short y, ConsoleKey consoleKey = ConsoleKey.Enter)
        {
            string logo = "Made width Destroy";
            if (center)
            {
                x = (short)(CONSOLE.WindowWidth / 2);
                x -= (short)(logo.Length / 2);
                y = (short)(CONSOLE.WindowHeight / 2);
            }
            CONSOLE.SetCursorPosition(x, y);
            CONSOLE.ForegroundColor = Colour.Black;
            CONSOLE.BackgroundColor = Colour.White;
            CONSOLE.Write(logo);
            CONSOLE.ResetColor();
            CONSOLE.SetCursorPosition(0, 0);
            //窗口透明度渐变
            for (int i = 0; i < 256; i++)
            {
                //按下回车键直接恢复透明度并且退出渐变阶段
                if (CONSOLE.GetKey(consoleKey))
                {
                    KERNEL.SET_WINDOW_ALPHA(255);
                    break;
                }
                KERNEL.SET_WINDOW_ALPHA((byte)i);
                KERNEL.SLEEP(10);
            }
            KERNEL.SLEEP(1000);
            CONSOLE.Clear();
        }

        /// <summary>
        /// 运行ILoveDestroy的小游戏
        /// </summary>
        public static void ILoveDestroy()
        {
            string[] lines = new string[]
            {
                @"              ,----------------,              ,---------,",
                @"         ,-----------------------,          ,""        ,""|",
                @"       ,""                      ,""|        ,""        ,""  |",
                @"      +-----------------------+  |      ,""        ,""    |",
                @"      |  .-----------------.  |  |     +---------+      |",
                @"      |  |                 |  |  |     | -==----'|      |",
                @"      |  | I LOVE DESTROY! |  |  |     |         |      |",
                @"      |  | By Charlie      |  |  |/----|`---=    |      |",
                @"      |  | C:\>_           |  |  |   ,/|==== ooo |      ;",
                @"      |  |                 |  |  |  // |(((( [33]|    ,"" ",
                @"      |  `-----------------'  |,"" .;'| |((((     |  ,""   ",
                @"      +-----------------------+  ;;  | |         |,""     ",
                @"         /_)______________(_/  //'   | +---------+       ",
                @"    ___________________________/___  `,                  ",
                @"   /  oooooooooooooooo  .o.  oooo /,   \,""-----------    ",
                @"  / ==ooooooooooooooo==.o.  ooo= //   ,`\--{)B     ,""    ",
                @" /_==__==========__==_ooo__ooo=_/'   /___________,""      ",
                @"                                                         "
            };
            //根据字符串数组初始化宽高
            Resources.GetLinesSize(lines, out int width, out int height);
            //初始化控制台
            Graphics graphics = RuntimeEngine.Construct2(
                "Consolas", true, 8, 16, false,
                (short)width, (short)height, CharWidth.Single);
            //变量定义
            GraphicContainer computer = null;
            Vector2 cursorPos = new Vector2(15, 8);
            char c = '\0';
            bool __ = true;
            float timer = 0;
            float interval = 0.5f;
            int limit = 11;  //最多打印11个英文字符
            int counter = 0; //计数器
            string str = string.Empty; //输入字符
            //键盘
            List<ConsoleKey> keyboard = new List<ConsoleKey>();
            for (int i = 48; i < 91; i++)
            {
                keyboard.Add((ConsoleKey)i);
            }
            keyboard.Add(ConsoleKey.Spacebar);
            keyboard.Add(ConsoleKey.Backspace);
            keyboard.Add(ConsoleKey.Enter);
            //指令集
            Dictionary<string, Action> commands = new Dictionary<string, Action>();
            commands.Add("exit", () => { RuntimeEngine.Exit(); });
            //开始生命周期
            RuntimeEngine.Start(
            () =>
            {
                //Start
                computer = graphics.CreatContainerByLines(lines);
                computer.SetColor(Colour.Black, Colour.White);
            },
            () =>
            {
                //Update
                timer += Time.DeltaTime;
                if (timer >= interval)
                {
                    timer = 0;
                    if (__)
                    {
                        c = '_';
                    }
                    else
                    {
                        c = '\0';
                    }
                    __ = !__;
                }
                foreach (ConsoleKey item in keyboard)
                {
                    //输入指令
                    if (item == ConsoleKey.Enter)
                    {
                        if (Input.GetKeyDown(item))
                        {
                            string lowerCase = str.ToLower();
                            if (commands.ContainsKey(lowerCase))
                            {
                                //执行命令
                                commands[lowerCase]();
                            }
                        }
                    }
                    else if (item == ConsoleKey.Backspace)
                    {
                        if (Input.GetKey(item))
                        {
                            if (counter > 0)
                            {
                                counter--;
                                //删除字符
                                str = str.Substring(0, counter);
                                //删除字符图像
                                GraphicGrid before = computer.GraphicGrids[
                                    cursorPos.Y * width + cursorPos.X];
                                before.Left = new CharInfo('\0', Colour.Black, Colour.White);
                                //光标后退一格
                                cursorPos.X--;
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetKeyDown(item))
                        {
                            if (counter < limit)
                            {
                                counter++;
                                //新增字符
                                str += (char)item;
                                //新增字符图形
                                GraphicGrid before = computer.GraphicGrids[
                                    cursorPos.Y * width + cursorPos.X];
                                before.Left = new CharInfo((char)item, Colour.Black, Colour.White);
                                //光标前进一格
                                cursorPos.X++;
                            }
                        }
                    }
                }
                //设置光标
                GraphicGrid graphicGrid = computer.GraphicGrids[
                    cursorPos.Y * width + cursorPos.X];
                graphicGrid.Left = new CharInfo(c, Colour.Black, Colour.White);
                //执行渲染指令
                graphics.PreRender();
                graphics.Render();
            }
            ,
            () =>
            {
                //Destroy
            }, 30);
        }
    }
}