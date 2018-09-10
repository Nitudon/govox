using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace UdonLib.Commons
{
    /// <summary>
    /// オブジェクトのバイナリシリアライズUtility
    /// </summary>
    public static class BinarySerializeUtility
    {
        /// <summary>
        /// MemoryStreamでバイナリシリアライズ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ToBinary<T>(this T obj) where T : class
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, obj);
            return stream.ToArray();
        }

        /// <summary>
        /// MemoryStreamでバイナリデシリアライズ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T FromBinary<T>(this byte[] bytes) where T : class
        {
            MemoryStream stream = new MemoryStream(bytes);
            BinaryFormatter bf = new BinaryFormatter();
            T obj = null;
            obj = bf.Deserialize(stream) as T;
            return obj;
        }

        /// <summary>
        /// オブジェクトからバイナリへのシリアライズ
        /// </summary>
        /// <typeparam name="T">オブジェクトの型</typeparam>
        /// <param name="path">バイナリの書き出しパス</param>
        /// <param name="obj">オブジェクト</param>
        public static void Serialize<T>(string path, T obj) where T : class
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
            }
        }

        /// <summary>
        /// バイナリからオブジェクトへのデシリアライズ
        /// </summary>
        /// <typeparam name="T">デシリアライズする型</typeparam>
        /// <param name="path">バイナリのパス</param>
        /// <returns>デシリアライズしたオブジェクト</returns>
        public static T Deserialize<T>(string path) where T : class
        {
            T obj = null;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter f = new BinaryFormatter();
                obj = f.Deserialize(fs) as T;
            }
            return obj;
        }

        /// <summary>
        /// 非シリアライズオブジェクトからバイナリへの強制シリアライズ
        /// </summary>
        /// <typeparam name="T">オブジェクトの型</typeparam>
        /// <param name="path">バイナリの書き出しパス</param>
        /// <param name="obj">オブジェクト</param>
        public static void ForceSerialize<T>(string path, T obj) where T : ISerializationSurrogate
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                var selector = new SurrogateSelector();
                var context = new StreamingContext(StreamingContextStates.All);

                selector.AddSurrogate(typeof(T), context, obj);

                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
            }
        }

        /// <summary>
        /// バイナリから非シリアライズオブジェクトへの強制デシリアライズ
        /// </summary>
        /// <typeparam name="T">デシリアライズする型</typeparam>
        /// <param name="path">バイナリのパス</param>
        /// <returns>デシリアライズしたオブジェクト</returns>
        public static T ForceDeserialize<T>(string path) where T : ISerializationSurrogate, new()
        {
            T obj;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var selector = new SurrogateSelector();

                var context = new StreamingContext(StreamingContextStates.All);

                selector.AddSurrogate(typeof(T), context, new T());

                BinaryFormatter f = new BinaryFormatter();
                obj = (T)f.Deserialize(fs);
            }
            return obj;
        }

        /// <summary>
        /// ファイルのバイナリ書き出し
        /// </summary>
        /// <param name="path">バイナリのパス</param>
        public static void WriteBinary(string path, byte[] bytes)
        {
            var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }

        /// <summary>
        /// ファイルのバイナリ書き出し
        /// </summary>
        /// <param name="path">バイナリのパス</param>
        public static void WriteBinaryAsync(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// ファイルのバイナリ読み出し
        /// </summary>
        /// <param name="path">バイナリのパス</param>
        /// <returns>バイト配列</returns>
        public static byte[] ReadBinary(string path, bool deleteFlg = false)
        {
            if(!File.Exists(path))
            {
                InstantLog.StringLogError("ReadBinary : file does not exist");
                return null;
            }

            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] bs = new byte[fs.Length];
            fs.Read(bs, 0, bs.Length);
            if(deleteFlg)
            {
                File.Delete(path);
            }
            fs.Close();

            return bs;
        }
    }
}