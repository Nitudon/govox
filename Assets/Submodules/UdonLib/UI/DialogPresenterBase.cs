using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;

namespace UdonLib.UI
{
    /// <summary>
    /// ダイアログのプレゼンターベース
    /// </summary>
    /// <typeparam name="TView">対応するビュー</typeparam>
    [RequireComponent(typeof(DialogViewBase))]
    public class DialogPresenterBase<TView> : UIMono, IInitializable where TView : DialogViewBase
    {
        [SerializeField]
        private TView _view;

        public virtual void Initialize()
        {
            _view.Initialize();
        }
    }
}