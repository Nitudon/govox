using System;
using System.Collections;
using UnityEngine;
using UniRx;

namespace UdonLib.Commons
{
    //非同期のシーン管理
    public class SceneManager : UdonBehaviourSingleton<SceneManager>
    {
        //コールバック的な
        public new delegate void callback();
        private callback _callbacks;

        public void addCallBack(callback c)
        {
            _callbacks += c;
        }

        //各種フラグ
        public bool isLoading { get { return _async.progress < 0.9f; } }
        public bool isScene(string name)
        {
            return name.Equals(this.name);
        }

        //シーン名
        public new string name{ get { return gameObject.scene.name; } }

        //ここから処理
        #region[Async Scene Manage]
        private AsyncOperation _async;
        
        //呼び出し
        public void SceneLoad(string scene)
        {
            StartCoroutine(AsyncSceneLoadStart(scene));
        }

        //ロード
        protected IEnumerator AsyncSceneLoadStart(string scene,float wait = 0.5f)
        {
            //読み込みチェック
            if(UnityEngine.SceneManagement.SceneManager.GetSceneByName(scene) == null)
            {
                InstantLog.StringLogError("Scene Missing");
                yield break;
            }

            //読み込み開始
            _async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
            _async.allowSceneActivation = false;

            InstantLog.StringLog("Load Start", StringExtensions.TextColor.darkblue);

            //読み込み中
            while (_async.progress < 0.9f)
            {
                yield return new WaitForEndOfFrame();
            }

            //読み込み終了
            InstantLog.StringLog("Scene Loaded", StringExtensions.TextColor.blue);
            if (_callbacks != null)
            {
                _callbacks();
            }
            yield return new WaitForSeconds(wait);

            _async.allowSceneActivation = true;
        }

        //Observer付きロード、プログレスを発行
        private IEnumerator AsyncSceneLoadStart(IObserver<float> observer, string scene, float wait = 0.5f)
        {
            //読み込みチェック
            if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(scene) == null)
            {
                InstantLog.StringLogError("Scene Missing");
                yield break;
            }

            //読み込み開始
            _async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
            _async.allowSceneActivation = false;

            InstantLog.StringLog("Load Start", StringExtensions.TextColor.darkblue);

            //読み込み中
            while (_async.progress < 0.9f)
            {
                //プログレスをObserverに通知
                yield return new WaitForEndOfFrame();
                observer.OnNext(_async.progress);
            }

            //読み込み終了
            InstantLog.StringLog("Scene Loaded", StringExtensions.TextColor.blue);
            if (_callbacks != null)
            {
                _callbacks();
            }
            yield return new WaitForSeconds(wait);

            //  完了通知
            observer.OnNext(1.0f);
            observer.OnCompleted();
            _async.allowSceneActivation = true;
        }

        // Observableとしてロードコルーチンを返す
        protected IObservable<float> LoadSceneObservable(string scene, float wait = 0.5f)
        {
            return Observable.FromCoroutine<float>(observer => AsyncSceneLoadStart(observer, scene, wait));
        }
    }
    #endregion
}
