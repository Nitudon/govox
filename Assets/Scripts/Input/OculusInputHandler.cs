using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UdonLib.Commons;

/// <summary>
/// 大雑把な入力処理
/// </summary>
public class OculusInputHandler : InitializableMono, IDisposable
{
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
        if(onOVRInputGet == null)
        {
            onOVRInputGet = new Dictionary<OVRInput.Button, ISubject<Unit>>();
        }

        if(onOVRInputGetObserveCallbacks == null)
        {
            onOVRInputGetObserveCallbacks = new Dictionary<OVRInput.Button, Action>();
        }

        if(onOVRInputGet.TryGetValue(button, out var sub))
        {
            return sub;
        }
        else
        {
            sub = new Subject<Unit>()
                    .AddTo(gameObject)
                    .AddTo(_inputSubscriptionDisposable);

            Action action = () => sub.OnNext(Unit.Default);
            if(onOVRInputGetObserveCallbacks.ContainsKey(button))
            {
                onOVRInputGetObserveCallbacks[button] = action;    
            }
            else
            {
                onOVRInputGetObserveCallbacks.Add(button, action);
            }

            onFixedUpdateObserveCallback += action;
            return sub;
        }
    }

    public void ClearOVRInputGet()
    {
        
    }

    public void Dispose()
    {
        _inputSubscriptionDisposable.Dispose();
    }
}
