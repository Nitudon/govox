using System;
using UdonLib.Commons;
using UnityEngine;
using UniRx.Async;

namespace UdonLib.UI
{
    public class DialogManager : UdonBehaviourSingleton<DialogManager>
    {
        public virtual async UniTask<TDialog> CreateDialog<TDialog>(RectTransform parent, TDialog prefab, Action<TDialog> onInitialized = null) 
            where TDialog : DialogPresenterBase
        {
            var instance = Instantiate(prefab, parent);
            onInitialized?.Invoke(instance);
            await instance.OpenDialog();
            instance.OnOpened();
            return instance;
        }
    }
}
