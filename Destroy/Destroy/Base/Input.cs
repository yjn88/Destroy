namespace Destroy
{
    using Destroy.Kernel;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 获取鼠标/键盘输入 <see langword="static"/>
    /// </summary>
    [EnableOnRuntime]
    public static class Input
    {
        private static List<ConsoleKey> pressedKeys = new List<ConsoleKey>();

        private static List<MouseButton> clickedButtons = new List<MouseButton>();

        /// <summary>
        /// 控制台具有焦点
        /// </summary>
        public static bool ConsoleInFocus { get; private set; }

        /// <summary>
        /// 鼠标位于控制台窗口内
        /// </summary>
        public static bool MouseInConsole { get; private set; }

        /// <summary>
        /// 控制台光标坐标
        /// </summary>
        public static Vector2 CursorPosition { get; private set; }

        /// <summary>
        /// 鼠标坐标
        /// </summary>
        public static Vector2 MousePosition { get; private set; }

        /// <summary>
        /// 获取按键
        /// </summary>
        /// <param name="consoleKey"></param>
        /// <returns>是否成功</returns>
        public static bool GetKey(ConsoleKey consoleKey)
        {
            return CONSOLE.GetKey(consoleKey);
        }

        /// <summary>
        /// 获取按键按下
        /// </summary>
        /// <param name="consoleKey"></param>
        /// <returns>是否成功</returns>
        public static bool GetKeyDown(ConsoleKey consoleKey)
        {
            if (GetKey(consoleKey))
            {
                if (!pressedKeys.Contains(consoleKey))
                {
                    pressedKeys.Add(consoleKey);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取按键抬起
        /// </summary>
        /// <param name="consoleKey"></param>
        /// <returns>是否成功</returns>
        public static bool GetKeyUp(ConsoleKey consoleKey)
        {
            if (GetKey(consoleKey))
            {
                if (!pressedKeys.Contains(consoleKey))
                {
                    pressedKeys.Add(consoleKey);
                }
                return false;
            }
            else
            {
                if (pressedKeys.Contains(consoleKey))
                {
                    pressedKeys.Remove(consoleKey);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取鼠标
        /// </summary>
        /// <param name="mouseButton">鼠标按键</param>
        /// <returns>是否成功</returns>
        public static bool GetMouseButton(MouseButton mouseButton)
        {
            return CONSOLE.GetKey((ConsoleKey)mouseButton);
        }

        /// <summary>
        /// 获取鼠标按下
        /// </summary>
        /// <param name="mouseButton">鼠标按键</param>
        /// <returns>是否成功</returns>
        public static bool GetMouseButtonDown(MouseButton mouseButton)
        {
            if (GetMouseButton(mouseButton))
            {
                if (!clickedButtons.Contains(mouseButton))
                {
                    clickedButtons.Add(mouseButton);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取鼠标抬起
        /// </summary>
        /// <param name="mouseButton">鼠标按键</param>
        /// <returns>是否成功</returns>
        public static bool GetMouseButtonUp(MouseButton mouseButton)
        {
            if (GetMouseButton(mouseButton))
            {
                if (!clickedButtons.Contains(mouseButton))
                {
                    clickedButtons.Add(mouseButton);
                }
                return false;
            }
            else
            {
                if (clickedButtons.Contains(mouseButton))
                {
                    clickedButtons.Remove(mouseButton);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        internal static void CheckMouseState()
        {
            bool r = KERNEL.READ_CONSOLE_INPUT(CONSOLE.InputHandle,
                out short cursorPosX, out short cursorPosY);
            if (r)
            {
                CursorPosition = new Vector2(cursorPosX, cursorPosY);
            }

            KERNEL.GET_CURSOR_POS(out int mousePosX, out int mousePosY);

            MousePosition = new Vector2(mousePosX, mousePosY);
            ConsoleInFocus = KERNEL.GET_WINDOW_IN_FOCUS();
            //判断鼠标是否则在控制台窗口内
            KERNEL.GET_WINDOW_POS(out int windowPosX, out int windowPosY);
            KERNEL.GET_WINDOW_SIZE(out int width, out int height);
            if (mousePosX > windowPosX && mousePosX < windowPosX + width &&
                mousePosY > windowPosY && mousePosY < windowPosY + height)
            {
                MouseInConsole = true;
            }
            else
            {
                MouseInConsole = false;
            }
        }

        internal static void CheckKeyboardState()
        {
            List<ConsoleKey> consoleKeys = new List<ConsoleKey>();
            foreach (ConsoleKey item in pressedKeys)
            {
                if (GetKey(item))
                {
                    consoleKeys.Add(item);
                }
            }
            pressedKeys = consoleKeys;

            List<MouseButton> mouseButtons = new List<MouseButton>();
            foreach (MouseButton item in clickedButtons)
            {
                if (GetMouseButton(item))
                {
                    mouseButtons.Add(item);
                }
            }
            clickedButtons = mouseButtons;
        }
    }
}