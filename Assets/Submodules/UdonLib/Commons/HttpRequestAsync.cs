using System.Threading.Tasks;
using UnityEngine;
using UniRx;

namespace UdonLib.Commons
{
    /// <summary>
    /// UnityWebRequestAsyncのほうがよさそう
    /// </summary>
    public static class HttpRequestAsync
    {
        private static int _requestNumber = 1;

        public static async Task<string> GetRequestAsync(string endpoint)
        {
            int number;

#if UNITY_EDITOR
            InstantLog.StringLog($"GET@REQUEST#{_requestNumber}: {endpoint}", StringExtensions.TextColor.blue);
            number = _requestNumber++;
#endif
            var www = await ObservableWWW.Get(endpoint);
            if (www == null)
            {
                Debug.LogError("www request error");
            }
#if UNITY_EDITOR
            InstantLog.StringLog($"GET@RESPONCE#{number}: {www}", StringExtensions.TextColor.blue);
#endif
            return www;
        }

        public static async Task<T> GetRequestAsync<T>(string endpoint)
        {
            int number;

#if UNITY_EDITOR
            InstantLog.StringLog($"GET@REQUEST#{_requestNumber}: {endpoint}", StringExtensions.TextColor.blue);
            number = _requestNumber++;
#endif
            var www = await ObservableWWW.Get(endpoint);
            if (www == null)
            {
                Debug.LogError("www request error");
            }
#if UNITY_EDITOR
            InstantLog.StringLog($"GET@RESPONCE#{number}: {www}", StringExtensions.TextColor.blue);
#endif
            return JsonUtility.FromJson<T>(www);
        }

        public static async Task<string> PostRequestAsync(string endpoint, byte[] post)
        {
            int number;

#if UNITY_EDITOR
            InstantLog.StringLog($"POST@REQUEST#{_requestNumber}: {endpoint}", StringExtensions.TextColor.yellow);
            number = _requestNumber++;
#endif
            var www = await ObservableWWW.Post(endpoint, post);
            if (www == null)
            {
                Debug.LogError("www request error");
            }
#if UNITY_EDITOR
            InstantLog.StringLog($"POST@RESPONCE#{number}: {www}", StringExtensions.TextColor.yellow);
#endif
            return www;
        }

        public static async Task<string> PostRequestAsync(string endpoint, WWWForm form)
        {
            int number;

#if UNITY_EDITOR
            InstantLog.StringLog($"POST@REQUEST#{_requestNumber}: {endpoint}", StringExtensions.TextColor.yellow);
            number = _requestNumber++;
#endif
            var www = await ObservableWWW.Post(endpoint, form);
            if (www == null)
            {
                Debug.LogError("www request error");
            }
#if UNITY_EDITOR
            InstantLog.StringLog($"POST@RESPONCE#{number}: {www}", StringExtensions.TextColor.yellow);
#endif
            return www;
        }

        public static async Task<T> PostRequestAsync<T>(string endpoint, byte[] post)
        {
            int number;

#if UNITY_EDITOR
            InstantLog.StringLog($"POST@REQUEST#{_requestNumber}: {endpoint}", StringExtensions.TextColor.yellow);
            number = _requestNumber++;
#endif
            var www = await ObservableWWW.Post(endpoint, post);
            if (www == null)
            {
                Debug.LogError("www request error");
            }
#if UNITY_EDITOR
            InstantLog.StringLog($"POST@RESPONCE#{number}: {www}", StringExtensions.TextColor.yellow);
#endif
            return JsonUtility.FromJson<T>(www);
        }

        public static async Task<T> PostRequestAsync<T>(string endpoint, WWWForm form)
        {
            int number;

#if UNITY_EDITOR
            InstantLog.StringLog($"POST@REQUEST#{_requestNumber}: {endpoint}", StringExtensions.TextColor.yellow);
            number = _requestNumber++;
#endif
            var www = await ObservableWWW.Post(endpoint, form);
            if (www == null)
            {
                Debug.LogError("www request error");
            }
#if UNITY_EDITOR
            InstantLog.StringLog($"POST@RESPONCE#{number}: {www}", StringExtensions.TextColor.yellow);
#endif
            return JsonUtility.FromJson<T>(www);
        }
    }
}