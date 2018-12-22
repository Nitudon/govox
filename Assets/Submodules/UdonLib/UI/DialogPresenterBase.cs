using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UdonLib.Commons;
using UniRx.Async;

namespace UdonLib.UI
{
    /// <summary>
    /// ダイアログの基本の振る舞い
    /// </summary>
    public interface IDialogPresenter : IAsyncInitializable
    {
        UniTask OpenDialog();

        UniTask CloseDialog();

        void OnOpened();

        void OnClosed();
    }

    /// <summary>
    /// ダイアログの雛形
    /// </summary>
    public abstract class DialogPresenterBase : UIMono, IDialogPresenter
    {
        public abstract UniTask Initialize();

        public abstract UniTask OpenDialog();

        public abstract UniTask CloseDialog();

        public virtual void OnOpened()
        {

        }

        public virtual void OnClosed()
        {

        }
    }

    /// <summary>
    /// ダイアログのプレゼンターベース
    /// </summary>
    /// <typeparam name="TView">対応するビュー</typeparam>
    public class DialogPresenterBase<TView> : DialogPresenterBase
        where TView : DialogViewBase
    {
        [SerializeField]
        protected TView _view;

        public TView View => _view;

        public Action onOpenedCallback;
        public Action onClosedCallback;

        public override async UniTask Initialize()
        {
            _view.Initialize();
            await _view.OpenDialogAnimation();
        }

        public override async UniTask OpenDialog()
        {
            await _view.OpenDialogAnimation();
        }

        public override async UniTask CloseDialog()
        {
            await _view.CloseDialogAnimation();
        }

        public override void OnOpened()
        {
            base.OnOpened();
            onOpenedCallback?.Invoke();
        }

        public override void OnClosed()
        {
            base.OnClosed();
            onClosedCallback?.Invoke();
        }
    }

    /// <summary>
    /// ダイアログのプレゼンターベース
    /// </summary>
    /// <typeparam name="TView">対応するビュー</typeparam>
    [RequireComponent(typeof(DialogViewBase))]
    public class DialogPresenterBase<TView, TResult> : DialogPresenterBase<TView>
        where TView : DialogViewBase
    {
        private TaskCompletionSource<TResult> tcs = null;

        protected TResult _result;
        public TResult Result => _result;

        public override async UniTask Initialize()
        {
            tcs = new TaskCompletionSource<TResult>();
            await base.Initialize();
        }

        public override async UniTask CloseDialog()
        {
            tcs.SetResult(_result);
            await base.CloseDialog();
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