﻿namespace Destroy
{
    using System;

    /// <summary>
    /// 颜色
    /// </summary>
    public class Colour
    {
        /// <summary>
        /// 控制台颜色
        /// </summary>
        public ConsoleColor ConsoleColor;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="consoleColor">控制台颜色</param>
        public Colour(ConsoleColor consoleColor)
        {
            ConsoleColor = consoleColor;
        }

        #region Colours

        /// <summary>
        /// 黑色
        /// </summary>
        public static Colour Black = new Colour(ConsoleColor.Black);

        /// <summary>
        /// 藏蓝色
        /// </summary>
        public static Colour DarkBlue = new Colour(ConsoleColor.DarkBlue);

        /// <summary>
        /// 深绿色
        /// </summary>
        public static Colour DarkGreen = new Colour(ConsoleColor.DarkGreen);

        /// <summary>
        /// 深紫色
        /// </summary>
        public static Colour DarkCyan = new Colour(ConsoleColor.DarkCyan);

        /// <summary>
        /// 深红色
        /// </summary>
        public static Colour DarkRed = new Colour(ConsoleColor.DarkRed);

        /// <summary>
        /// 深紫红色
        /// </summary>
        public static Colour DarkMagenta = new Colour(ConsoleColor.DarkMagenta);

        /// <summary>
        /// 深黄色
        /// </summary>
        public static Colour DarkYellow = new Colour(ConsoleColor.DarkYellow);

        /// <summary>
        /// 灰色
        /// </summary>
        public static Colour Gray = new Colour(ConsoleColor.Gray);

        /// <summary>
        /// 深灰色
        /// </summary>
        public static Colour DarkGray = new Colour(ConsoleColor.DarkGray);

        /// <summary>
        /// 蓝色
        /// </summary>
        public static Colour Blue = new Colour(ConsoleColor.Blue);

        /// <summary>
        /// 绿色
        /// </summary>
        public static Colour Green = new Colour(ConsoleColor.Green);

        /// <summary>
        /// 青色
        /// </summary>
        public static Colour Cyan = new Colour(ConsoleColor.Cyan);

        /// <summary>
        /// 红色
        /// </summary>
        public static Colour Red = new Colour(ConsoleColor.Red);

        /// <summary>
        /// 紫红色
        /// </summary>
        public static Colour Magenta = new Colour(ConsoleColor.Magenta);

        /// <summary>
        /// 黄色
        /// </summary>
        public static Colour Yellow = new Colour(ConsoleColor.Yellow);

        /// <summary>
        /// 白色
        /// </summary>
        public static Colour White = new Colour(ConsoleColor.White);

        #endregion

        /// <summary>
        /// 颜色转ushort
        /// </summary>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        /// <returns>ushort</returns>
        public static ushort ColourToUshort(Colour foreColor, Colour backColor)
        {
            return (ushort)((int)backColor * 16 + (int)foreColor);
        }

        /// <summary>
        /// ushort转颜色
        /// </summary>
        /// <param name="attribute">ushort</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="backColor">背景色</param>
        public static void UshortToColour(ushort attribute, out Colour foreColor, out Colour backColor)
        {
            foreColor = (Colour)(attribute & 0x000F);
            backColor = (Colour)((attribute & 0x00F0) / 16);
        }

        /// <summary>
        /// 允许颜色转int
        /// </summary>
        /// <param name="colour">颜色</param>
        public static explicit operator int(Colour colour)
        {
            return (int)colour.ConsoleColor;
        }

        /// <summary>
        /// 允许int转颜色
        /// </summary>
        /// <param name="integer">int</param>
        public static explicit operator Colour(int integer)
        {
            foreach (object item in Enum.GetValues(typeof(ConsoleColor)))
            {
                ConsoleColor consoleColor = (ConsoleColor)item;
                if (integer == (int)consoleColor)
                {
                    return new Colour(consoleColor);
                }
            }
            Error.Pop();
            return Black;
        }
    }
}