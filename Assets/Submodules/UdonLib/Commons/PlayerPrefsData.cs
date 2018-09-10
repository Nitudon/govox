using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UdonLib.Commons {
    //汎用的Pref
    [System.Serializable]
    public abstract class PlayerPrefsData<T> where T : PlayerPrefsData<T>, new()
    {
        /// <summary>
        /// 保存キー取得
        /// </summary>
        static string GetKey()
        {
            return typeof(T).Name;
        }

        /// <summary>
        /// すでに保存されているか
        /// </summary>
        public static bool isSaved
        {
            get { return PlayerPrefs.HasKey(GetKey()); }
        }

        /// <summary>
        /// 保存データの読み込み（データが無い場合は新規データを生成し保存する）
        /// </summary>
        public static T Load()
        {
            T loaded = new T();
            if (!isSaved)
                loaded.Save();//新規データ保存
            else
            {
                var json = PlayerPrefs.GetString(GetKey());
                JsonUtility.FromJsonOverwrite(json, loaded);
            }
            return loaded;
        }

        /// <summary>
        /// データの保存
        /// </summary>
        public void Save()
        {
            var json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(GetKey(), json);
            PlayerPrefs.Save();
        }     
    }
}
#if UNITY_EDITOR
//エディタ上でのPrefDelete
public class PlayerPrefsEditor:EditorWindow
        {
            [MenuItem("Edit/PlayerPrefs/DeleteAll")]
            static void DeleteAll()
            {
                PlayerPrefs.DeleteAll();
                UdonLib.Commons.InstantLog.StringLog("Delete All Data Of PlayerPrefs!!",UdonLib.Commons.StringExtensions.TextColor.blue);
            }
        }
#endif