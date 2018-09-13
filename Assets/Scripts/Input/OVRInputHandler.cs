using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UdonLib.Commons;

/// <summary>
/// 大雑把な入力処理
/// </summary>
public class OVRInputHandler : InitializableMono, IDisposable
{
    private enum InputType : byte
    {
        Get,
        GetUp,
        GetDown,
    }

    private Dictionary<OVRInput.Button, ISubject<Unit>> onOVRInputGet;
    private Dictionary<OVRInput.Button, ISubject<Unit>> onOVRInputGetUp;
    private Dictionary<OVRInput.Button, ISubject<Unit>> onOVRInputGetDown;

    private Dictionary<OVRInput.Button, Action> onOVRInputGetObserveCallbacks;
    private Dictionary<OVRInput.Button, Action> onOVRInputGetUpObserveCallbacks;
    private Dictionary<OVRInput.Button, Action> onOVRInputGetDownObserveCallbacks;

    private Action onFixedUpdateObserveCallback; 
    private CompositeDisposable _inputSubscriptionDisposable;

    private bool _isValid;

    public override void Initialize()
    {
        _inputSubscriptionDisposable = new CompositeDisposable();

        this.FixedUpdateAsObservable()
            .Where(_ => _isValid)
            .Subscribe(_ => onFixedUpdateObserveCallback?.Invoke())
            .AddTo(gameObject)
            .AddTo(_inputSubscriptionDisposable);
    }

    public ISubject<Unit> OnOVRInputGet(OVRInput.Button button)
    {
        return OnOVRInputImpl(ref onOVRInputGet, ref onOVRInputGetObserveCallbacks, button);
    }

    public void ClearOVRInputGet(OVRInput.Button button)
    {
        ClearOnOVRInputImpl(ref onOVRInputGet, ref onOVRInputGetObserveCallbacks, button);
    }

    public ISubject<Unit> OnOVRInputGetUp(OVRInput.Button button)
    {
        return OnOVRInputImpl(ref onOVRInputGetUp, ref onOVRInputGetUpObserveCallbacks, button);
    }

    public void ClearOVRInputGetUp(OVRInput.Button button)
    {
        ClearOnOVRInputImpl(ref onOVRInputGetUp, ref onOVRInputGetUpObserveCallbacks, button);
    }

    public ISubject<Unit> OnOVRInputGetDown(OVRInput.Button button)
    {
        return OnOVRInputImpl(ref onOVRInputGetDown, ref onOVRInputGetDownObserveCallbacks, button);
    }

    public void ClearOVRInputGetDown(OVRInput.Button button)
    {
        ClearOnOVRInputImpl(ref onOVRInputGetDown, ref onOVRInputGetDownObserveCallbacks, button);
    }

    private ISubject<Unit> OnOVRInputImpl(ref Dictionary<OVRInput.Button, ISubject<Unit>> subDict, ref Dictionary<OVRInput.Button, Action> actDict, OVRInput.Button button)
    {
        if (subDict == null)
        {
            subDict = new Dictionary<OVRInput.Button, ISubject<Unit>>();
        }

        if (actDict == null)
        {
            actDict = new Dictionary<OVRInput.Button, Action>();
        }

        if (subDict.TryGetValue(button, out var sub))
        {
            return sub;
        }
        else
        {
            sub = new Subject<Unit>()
                    .AddTo(gameObject)
                    .AddTo(_inputSubscriptionDisposable);

            Action action = () => sub.OnNext(Unit.Default);
            if (actDict.ContainsKey(button))
            {
                actDict[button] = action;
            }
            else
            {
                actDict.Add(button, action);
            }

            onFixedUpdateObserveCallback += action;
            return sub;
        }
    }

    private void ClearOnOVRInputImpl(ref Dictionary<OVRInput.Button, ISubject<Unit>> subDict, ref Dictionary<OVRInput.Button, Action> actDict, OVRInput.Button button)
    {
        if (subDict.TryGetValue(button, out var sub))
        {
            sub.OnCompleted();
            subDict.Remove(button);

            if (actDict.TryGetValue(button, out var act))
            {
                onFixedUpdateObserveCallback -= act;
                actDict.Remove(button);
            }
        }
    }

    public void ClearAll()
    {
        _inputSubscriptionDisposable.Clear();

        onFixedUpdateObserveCallback = null;

        onOVRInputGet?.Clear();
        onOVRInputGetUp?.Clear();
        onOVRInputGetDown?.Clear();

        onOVRInputGetObserveCallbacks?.Clear();
        onOVRInputGetUpObserveCallbacks?.Clear();
        onOVRInputGetDownObserveCallbacks?.Clear();
    }

    public void Dispose()
    {
        ClearAll();

        _inputSubscriptionDisposable.Dispose();

        onOVRInputGet = null;
        onOVRInputGetUp = null;
        onOVRInputGetDown = null;

        onOVRInputGetObserveCallbacks = null;
        onOVRInputGetUpObserveCallbacks = null;
        onOVRInputGetDownObserveCallbacks = null;
    }

    private void OnDestroy()
    {
        Dispose();
    }
}
