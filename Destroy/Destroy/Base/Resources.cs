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
        /// 使用.txt文件创建图形容器对象
        /// </summary>
        /// <param name="name">文件名(可以不写.txt后缀)</param>
        /// <param name="graphics">图形对象</param>
        /// <returns>图形容器对象</returns>
        public static GraphicContainer CreatByTxt(this Graphics graphics, string name)
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

            List<GraphicGrid> grids = graphics.CreatGridByStrings(
                Vector2.Zero, lines, Colour.Gray, Colour.Black);
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