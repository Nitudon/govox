using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UniRx.Async;

namespace UdonLib.Commons
{
    public static class UnityWebRequestAsync
    {
        #region GET Request
        public static async Task<string> GetAsync(string uri)
        {
            UnityWebRequest request = UnityWebRequest.Get(uri);
            await request.SendWebRequest();

            return request.downloadHandler.text;
        }

        public static async Task<T> GetAsync<T>(string uri)
        {
            UnityWebRequest request = UnityWebRequest.Get(uri);
            await request.SendWebRequest();

            return JsonUtility.FromJson<T>(request.downloadHandler.text);
        }

        public static async Task<byte[]> GetBytesAsync<T>(string uri)
        {
            UnityWebRequest request = UnityWebRequest.Get(uri);
            await request.SendWebRequest();

            return request.downloadHandler.data;
        }
        #endregion

        #region POST Request
        public static async Task PostAsync(string uri, string data)
        {
            UnityWebRequest request = UnityWebRequest.Post(uri, data);
            await request.SendWebRequest();
        }

        public static async Task PostAsync(string uri, WWWForm data)
        {
            UnityWebRequest request = UnityWebRequest.Post(uri, data);
            await request.SendWebRequest();
        }

        public static async Task<T> PostAsync<T>(string uri, string data)
        {
            UnityWebRequest request = UnityWebRequest.Post(uri, data);
            await request.SendWebRequest();

            return JsonUtility.FromJson<T>(request.downloadHandler.text);
        }

        public static async Task<T> PostAsync<T>(string uri, WWWForm data)
        {
            UnityWebRequest request = UnityWebRequest.Post(uri, data);
            await request.SendWebRequest();

            return JsonUtility.FromJson<T>(request.downloadHandler.text);
        }
        #endregion

        #region PUT Request
        public static async Task PutAsync(string uri, string data)
        {
            UnityWebRequest request = UnityWebRequest.Put(uri, data);
            await request.SendWebRequest();
        }

        public static async Task PutAsync(string uri, byte[] data)
        {
            UnityWebRequest request = UnityWebRequest.Put(uri, data);
            await request.SendWebRequest();
        }

        public static async Task<T> PutAsync<T>(string uri, string data)
        {
            UnityWebRequest request = UnityWebRequest.Put(uri, data);
            await request.SendWebRequest();

            return JsonUtility.FromJson<T>(request.downloadHandler.text);
        }

        public static async Task<T> PutAsync<T>(string uri, byte[] data)
        {
            UnityWebRequest request = UnityWebRequest.Put(uri, data);
            await request.SendWebRequest();

            return JsonUtility.FromJson<T>(request.downloadHandler.text);
        }
        #endregion

        #region DELETE Request
        public static async Task DeleteAsync(string uri)
        {
            UnityWebRequest request = UnityWebRequest.Delete(uri);
            await request.SendWebRequest();
        }

        public static async Task<T> DeleteAsync<T>(string uri)
        {
            UnityWebRequest request = UnityWebRequest.Delete(uri);
            await request.SendWebRequest();

            return JsonUtility.FromJson<T>(request.downloadHandler.text);
        }
        #endregion
    }
}