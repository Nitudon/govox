using System.Threading.Tasks;
using UniRx;

namespace UdonLib.Commons
{
    public static class AsyncWWWDownloader
    {
        public static async Task<byte[]> DownloadAsync(string url)
        {
            var data = await ObservableWWW.GetAndGetBytes(url);
            return data;
        }

        public static async Task<T> DownloadAsync<T>(string url) where T : class
        {
            var data = await ObservableWWW.GetAndGetBytes(url);
            return data.FromBinary<T>();
        }

        public static async Task DownloadAsync(string path, string name, string url)
        {
            var data = await ObservableWWW.GetAndGetBytes(url);
            BinarySerializeUtility.WriteBinary(path + name, data);
        }
    }
}