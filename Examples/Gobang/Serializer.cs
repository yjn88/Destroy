namespace Gobang
{
    using Destroy;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public interface ISerializer
    {
        byte[] Serialize(object obj);

        T Deserialize<T>(byte[] data) where T : class;
    }

    /// <summary>
    /// 为了不使用外部序列化依赖, 所以使用C#本身自带的序列化工具
    /// 但是由于C#自带的序列化工具会序列化很多额外的字节, 所以十分不推荐
    /// 在一般项目中使用
    /// </summary>
    public class Serializer : ISerializer
    {
        /// <summary>
        /// obj -> bytes, 如果obj未被标记为 [Serializable] 则返回null
        /// </summary>
        public byte[] Serialize(object obj)
        {
            if (obj == null || !obj.GetType().IsSerializable)
            {
                Error.Pop();
            }

            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                byte[] data = stream.ToArray();
                return data;
            }
        }

        /// <summary>
        /// bytes -> obj, 如果obj未被标记为 [Serializable] 则返回null
        /// </summary>
        public T Deserialize<T>(byte[] data) where T : class
        {
            if (data == null || !typeof(T).IsSerializable)
            {
                Error.Pop();
            }

            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(data))
            {
                object obj = formatter.Deserialize(stream);
                return obj as T;
            }
        }
    }
}