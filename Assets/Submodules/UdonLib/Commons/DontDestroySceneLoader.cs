using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Awake前にDontDestroyシーン（共有シーン）を自動でロードするクラス
/// </summary>
public class DontDestroySceneLoader
{
    /*
    //ゲーム開始時(シーン読み込み前)に実行される
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadManagerScene()
    {
        var managerSceneName = "SceneController";

        if (!SceneManager.GetSceneByName(managerSceneName).IsValid())
        {
           SceneManager.LoadScene(managerSceneName, LoadSceneMode.Additive);
        }
    }*/

}