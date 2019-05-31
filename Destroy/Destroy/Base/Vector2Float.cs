namespace Destroy
{
    using System;

    /// <summary>
    /// 浮点数型二维向量
    /// </summary>
    public struct Vector2Float
    {
        /// <summary>
        /// X分量
        /// </summary>
        public float X;

        /// <summary>
        /// Y分量
        /// </summary>
        public float Y;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2Float(float x, float y)
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
            return this == (Vector2Float)obj;
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
        /// 向量长度
        /// </summary>
        public float Magnitude
        {
            get
            {
                float magSquare = X * X + Y * Y;
                return (float)Math.Sqrt(magSquare);
            }
        }

        /// <summary>
        /// 单位向量
        /// </summary>
        public Vector2Float Normalized => this / Magnitude;

        /// <summary>
        /// 算两点之间距离
        /// </summary>
        /// <param name="other">另一个对象</param>
        /// <returns>距离</returns>
        public float Distance(Vector2Float other)
        {
            Vector2Float vector2Float = this - other;
            return vector2Float.Magnitude;
        }

        /// <summary>
        /// 零向量
        /// </summary>
        public static Vector2Float Zero
        {
            get
            {
                Vector2Float vector2Float = new Vector2Float(0, 0);
                return vector2Float;
            }
        }

        /// <summary>
        /// 等于运算符
        /// </summary>
        public static bool operator ==(Vector2Float left, Vector2Float right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        /// <summary>
        /// 不等于运算符
        /// </summary>
        public static bool operator !=(Vector2Float left, Vector2Float right)
        {
            return left.X != right.X || left.Y != right.Y;
        }

        /// <summary>
        /// 加运算符
        /// </summary>
        public static Vector2Float operator +(Vector2Float left, Vector2Float right)
        {
            left.X += right.X;
            left.Y += right.Y;
            return left;
        }

        /// <summary>
        /// 减运算符
        /// </summary>
        public static Vector2Float operator -(Vector2Float left, Vector2Float right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            return left;
        }

        /// <summary>
        /// 乘运算符
        /// </summary>
        public static Vector2Float operator *(Vector2Float left, float right)
        {
            left.X *= right;
            left.Y *= right;
            return left;
        }

        /// <summary>
        /// 除运算符
        /// </summary>
        public static Vector2Float operator /(Vector2Float left, float right)
        {
            if (right == 0)
            {
                Error.Pop();
            }
            left.X /= right;
            left.Y /= right;
            return left;
        }

        /// <summary>
        /// 允许强制转换
        /// </summary>
        public static explicit operator Vector2(Vector2Float vector2Float)
        {
            Vector2 vector2 = new Vector2
            {
                X = (int)vector2Float.X,
                Y = (int)vector2Float.Y
            };
            return vector2;
        }
    }
}