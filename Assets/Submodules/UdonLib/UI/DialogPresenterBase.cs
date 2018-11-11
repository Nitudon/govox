using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UdonLib.Commons;

namespace UdonLib.UI
{
    /// <summary>
    /// ダイアログのプレゼンターベース
    /// </summary>
    /// <typeparam name="TView">対応するビュー</typeparam>
    public class DialogPresenterBase<TView> : UIMono, IInitializable where TView : DialogViewBase
    {
        [SerializeField]
        protected TView _view;

        public TView View => _view;

        public Action onCreatedCallback;
        public Action onClosedCallback;

        public virtual void Initialize()
        {
            _view.Initialize();
        }

        public static void OpenDialog<TDialog>(RectTransform parent, Action<TDialog> preParam) where TDialog : DialogPresenterBase<TView>
        {

        }

        public virtual void CloseDialog()
        {
            _view.CloseDialogAnimation(() =>
            {
                onClosedCallback?.Invoke();
                Destroy(gameObject);
            });
        }

        protected virtual DialogPresenterBase<TView> CreateDialog(RectTransform parent, DialogPresenterBase<TView> prefab, Action onCreated = null)
        {
            var instance = Instantiate(prefab, parent);
            onCreatedCallback?.Invoke();
            instance.View.OpenDialogAnimation(onCreated);
            return instance;
        }
    }

    /// <summary>
    /// ダイアログのプレゼンターベース
    /// </summary>
    /// <typeparam name="TView">対応するビュー</typeparam>
    [RequireComponent(typeof(DialogViewBase))]
    public class DialogPresenterBase<TView, TResult> : DialogPresenterBase<TView>, IInitializable where TView : DialogViewBase
    {
        private TaskCompletionSource<TResult> tcs = null;

        protected TResult _result;
        public TResult Result => _result;

        public override void Initialize()
        {
            tcs = new TaskCompletionSource<TResult>();
            base.Initialize();
        }

        public override void CloseDialog()
        {
            tcs.SetResult(_result);
            base.CloseDialog();
        }

        public virtual TaskAwaiter<TResult> GetAwaiter()
        {
            return tcs.Task.GetAwaiter();
        }

        public async Task<TResult> WaitClose()
        {
            return await tcs.Task;
        }
    }

    public class SimpleDialog : DialogPresenterBase<DialogViewBase>
    {

    }
}