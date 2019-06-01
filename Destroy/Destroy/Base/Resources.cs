namespace Destroy
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// 资源管理 <see langword="static"/>
    /// </summary>
    public static class Resources
    {
        /// <summary>
        /// 资源路径目录
        /// </summary>
        public static string ResourcesDirectory = Environment.CurrentDirectory;

        /// <summary>
        /// .txt后缀
        /// </summary>
        public const string TXT = ".txt";

        /// <summary>
        /// 读取.txt文件
        /// </summary>
        /// <param name="fileName">文件名(可以不写.txt后缀)</param>
        /// <returns>字符行数组</returns>
        public static string[] ReadLines(string fileName)
        {
            string path = string.Empty;
            if (fileName.Contains("."))
            {
                path = Path.Combine(ResourcesDirectory, fileName);
            }
            else
            {
                path = Path.Combine(ResourcesDirectory, fileName + TXT);
            }
            if (!File.Exists(path))
            {
                Error.Pop("Path does not exists!");
            }
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            return lines;
        }

        /// <summary>
        /// 获取lines的最大宽高
        /// </summary>
        /// <param name="lines">行</param>
        /// <param name="width">最宽</param>
        /// <param name="height">最高</param>
        /// <param name="ignore">是否忽略每行尾部空格</param>
        public static void GetLinesSize(string[] lines, out int width, out int height, bool ignore = false)
        {
            width = 0;
            height = lines.Length;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                //忽略尾部空格
                if (ignore)
                {
                    line = line.TrimEnd();
                }
                if (line.Length > width)
                {
                    width = line.Length;
                }
            }
        }

        /// <summary>
        /// 创建图形容器对象
        /// </summary>
        /// <param name="graphics">图形对象</param>
        /// <param name="lines">字符行数组</param>
        /// <param name="ignore">是否忽略每行尾部空格</param>
        /// <returns>图形容器对象</returns>
        public static GraphicContainer CreatContainerByLines(this Graphics graphics, string[] lines, bool ignore = false)
        {
            string[] ls = new string[lines.Length];
            if (ignore)
            {
                for (int i = 0; i < ls.Length; i++)
                {
                    ls[i] = lines[i].TrimEnd(); //去除尾部空格
                }
            }
            else
            {
                for (int i = 0; i < ls.Length; i++)
                {
                    ls[i] = lines[i];
                }
            }

            List<GraphicGrid> grids = new List<GraphicGrid>();
            if (graphics.CharWidth == CharWidth.Double)
            {
                grids = graphics.CreatGridByStrings(Vector2.Zero, ls, Colour.Gray, Colour.Black);
            }
            else if (graphics.CharWidth == CharWidth.Single)
            {
                grids = graphics.CreatGridByStrings1(Vector2.Zero, ls, Colour.Gray, Colour.Black);
            }
            GraphicContainer container = new GraphicContainer(grids);
            return container;
        }

        /// <summary>
        /// 设置图形容器对象
        /// </summary>
        /// <param name="graphics">图形对象</param>
        /// <param name="lines">字符行数组</param>
        /// <param name="ignore">是否忽略每行尾部空格</param>
        public static void SetContainerByLines(this Graphics graphics, string[] lines, bool ignore = false)
        {
            string[] ls = new string[lines.Length];
            if (ignore)
            {
                for (int i = 0; i < ls.Length; i++)
                {
                    ls[i] = lines[i].TrimEnd(); //去除尾部空格
                }
            }
            else
            {
                for (int i = 0; i < ls.Length; i++)
                {
                    ls[i] = lines[i];
                }
            }

            if (graphics.CharWidth == CharWidth.Double)
            {
                graphics.SetGridByStrings(Vector2.Zero, ls, Colour.Gray, Colour.Black);
            }
            else if (graphics.CharWidth == CharWidth.Single)
            {
                graphics.SetGridByStrings1(Vector2.Zero, ls, Colour.Gray, Colour.Black);
            }
        }

        /// <summary>
        /// 转换文件编码
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="relative">是否是相对路径</param>
        /// <param name="encoding">目标编码</param>
        public static void ConvertEncoding(string path, bool relative, Encoding encoding)
        {
            if (relative)
            {
                path = Path.Combine(ResourcesDirectory, path);
            }
            Encoding readEncoding = Encoding.Default;

            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    byte[] buffer = reader.ReadBytes(2);

                    if (buffer[0] == 0xEF && buffer[1] == 0xBB)
                    {
                        readEncoding = Encoding.UTF8;
                    }
                    else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                    {
                        readEncoding = Encoding.BigEndianUnicode;
                    }
                    else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                    {
                        readEncoding = Encoding.Unicode;
                    }
                }
            }

            if (readEncoding != encoding)
            {
                string text = string.Empty;
                using (StreamReader newReader = new StreamReader(path, readEncoding, false))
                {
                    text = newReader.ReadToEnd();
                }
                using (StreamWriter writer = new StreamWriter(path, false, encoding))
                {
                    writer.Write(text);
                }
            }
        }
    }
}