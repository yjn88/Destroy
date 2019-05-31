namespace Destroy
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// 资源管理
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
        /// 获取.txt文件里行的最大宽高
        /// </summary>
        /// <param name="name">文件名(可以不写.txt后缀)</param>
        /// <param name="width">最宽</param>
        /// <param name="height">最高</param>
        /// <param name="trim">是否忽略每行尾部空格</param>
        public static void GetMaxSizeInTxt(string name, out int width, out int height, bool trim = true)
        {
            string path = string.Empty;
            if (name.Contains("."))
            {
                path = Path.Combine(ResourcesDirectory, name);
            }
            else
            {
                path = Path.Combine(ResourcesDirectory, name + TXT);
            }
            if (!File.Exists(path))
            {
                Error.Pop("Path does not exists!");
            }

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            width = 0;
            height = lines.Length;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length > width)
                {
                    width = lines[i].Length;
                }
                //去除尾部空格
                if (trim)
                {
                    lines[i] = lines[i].TrimEnd();
                }
            }
        }

        /// <summary>
        /// 使用.txt文件创建图形容器对象
        /// </summary>
        /// <param name="name">文件名(可以不写.txt后缀)</param>
        /// <param name="graphics">图形对象</param>
        /// <param name="trim">是否去除每行尾部空格</param>
        /// <returns>图形容器对象</returns>
        public static GraphicContainer CreatByTxt(this Graphics graphics, string name, bool trim = true)
        {
            string path = string.Empty;
            if (name.Contains("."))
            {
                path = Path.Combine(ResourcesDirectory, name);
            }
            else
            {
                path = Path.Combine(ResourcesDirectory, name + TXT);
            }
            if (!File.Exists(path))
            {
                Error.Pop("Path does not exists!");
            }

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            if (trim)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    //去除尾部空格
                    lines[i] = lines[i].TrimEnd();
                }
            }

            List<GraphicGrid> grids = new List<GraphicGrid>();
            if (graphics.CharWidth == CharWidth.Double)
            {
                grids = graphics.CreatGridByStrings(Vector2.Zero, lines, Colour.Gray, Colour.Black);
            }
            else if (graphics.CharWidth == CharWidth.Single)
            {
                grids = graphics.CreatGridByStrings1(Vector2.Zero, lines, Colour.Gray, Colour.Black);
            }

            GraphicContainer container = new GraphicContainer(grids);
            return container;
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