namespace Destroy
{
    using System;

    /// <summary>
    /// 整数型二维向量
    /// </summary>
    public struct Vector2
    {
        /// <summary>
        /// X分量
        /// </summary>
        public int X;

        /// <summary>
        /// Y分量
        /// </summary>
        public int Y;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="obj">另一个对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object obj)
        {
            return this == (Vector2)obj;
        }

        /// <summary>
        /// 获取哈希值
        /// </summary>
        /// <returns>哈希值</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 转换为字符串形式
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return $"[X:{X},Y:{Y}]";
        }

        /// <summary>
        /// 算两点之间距离
        /// </summary>
        /// <param name="other">另一个对象</param>
        /// <returns>距离</returns>
        public int Distance(Vector2 other)
        {
            return Math.Abs(other.X - X) + Math.Abs(other.Y - Y);
        }

        /// <summary>
        /// 零向量
        /// </summary>
        public static Vector2 Zero
        {
            get
            {
                Vector2 vector2 = new Vector2(0, 0);
                return vector2;
            }
        }

        /// <summary>
        /// 等于运算符
        /// </summary>
        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        /// <summary>
        /// 不等于运算符
        /// </summary>
        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return left.X != right.X || left.Y != right.Y;
        }

        /// <summary>
        /// 加运算符
        /// </summary>
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            left.X += right.X;
            left.Y += right.Y;
            return left;
        }

        /// <summary>
        /// 减运算符
        /// </summary>
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            return left;
        }

        /// <summary>
        /// 乘运算符
        /// </summary>
        public static Vector2 operator *(Vector2 left, int right)
        {
            left.X *= right;
            left.Y *= right;
            return left;
        }

        /// <summary>
        /// 除运算符
        /// </summary>
        public static Vector2 operator /(Vector2 left, int right)
        {
            if (right == 0)
            {
                throw new Exception("Error!");
            }

            left.X /= right;
            left.Y /= right;
            return left;
        }

        /// <summary>
        /// 允许强制转换
        /// </summary>
        public static explicit operator Vector2Float(Vector2 vector2)
        {
            Vector2Float vector2Float = new Vector2Float
            {
                X = vector2.X,
                Y = vector2.Y
            };
            return vector2Float;
        }
    }
}