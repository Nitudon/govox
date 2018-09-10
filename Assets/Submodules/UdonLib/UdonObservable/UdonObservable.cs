namespace UdonObservable
{
    using UnityEngine;
    using UniRx;
    using UniRx.Triggers;
    using System;
    using System.Linq;

    /// <summary>
    /// Input周りのRx
    /// マウス、キーボード、ゲームパッドの各種ボタンに関するObservable
    /// 
    /// Foo : そのボタンを押している時
    /// FooDown : そのボタンを押した時
    /// FooUp : そのボタンを押すのをやめた時
    /// </summary>
    namespace InputRx
    {
        namespace Key
        {
            /// <summary>
            /// キーボード周りのObservable
            /// </summary>
            /// 
            public class KeyObservable : MonoBehaviour
            {
                public static IObservable<Unit> GetKeyDownObservable(KeyCode key)
                {
                    return
                    Observable.EveryUpdate()
                        .Where(_ => Input.GetKeyDown(key))
                        .AsUnitObservable()
                        .Publish()
                        .RefCount();
                }

                public static IObservable<Unit> GetKeyObservable(KeyCode key)
                {
                    return
                    Observable.EveryUpdate()
                        .Where(_ => Input.GetKey(key))
                        .AsUnitObservable()
                        .Publish()
                        .RefCount();
                }


                public static IObservable<Unit> GetKeyUpObservable(KeyCode key)
                {
                    return
                    Observable.EveryUpdate()
                        .Where(_ => Input.GetKeyUp(key))
                        .AsUnitObservable()
                        .Publish()
                        .RefCount();
                }

                public static IObservable<Unit> GetKeyHoldObservable(KeyCode key, GameObject gameobject, float duration = 2.0f)
                {
                    return
                        GetKeyDownObservable(key)
                        .SelectMany(Observable.Timer(TimeSpan.FromSeconds(duration)))
                        .TakeUntil(GetKeyUpObservable(key))
                        .AsUnitObservable()
                        .RepeatUntilDestroy(gameobject)
                        .Publish()
                        .RefCount();
                }

                 //複数回のキーボード入力の監視(count:打鍵回数,duration:打鍵の間隔)
                public static IObservable<Unit> KeyCountObservable(KeyCode key, int count, float duration = 0.2f)
                {
                    return
                    GetKeyDownObservable(key)
                    .Buffer(GetKeyDownObservable(key)
                    .Throttle(TimeSpan.FromSeconds(duration)))
                    .Where(x => x.Count >= count)
                    .AsUnitObservable()
                    .Publish()
                    .RefCount();
                }

                public static IDisposable MultiKeyActionRegister(KeyCode[] keys, params Action[] actions)
                {
                    if (keys.Length != actions.Length)
                    {
                        Debug.LogError("登録するキーの数と対応するメソッドの数が一致していません");
                        return null;
                    }

                   return keys
                        .ToList()
                        .Select((key, index) => GetKeyObservable(key).Select(_ => actions[index]))
                        .Merge()
                        .Where(action => action != null)
                        .Subscribe(action => action.Invoke());
                }

            }
        }

        namespace GamePad
        {
            //ゲームパッドに関するObservable
            //基本的に引数としてコントローラーID、対応するボタンを引数として使用する
            //コントローラーIDを指定しない場合、自動的に１Pでの対応となる

#if UNITY_ANDROID || UNITY_UNITY_IOS
            [Obsolete("最適化されてない開発段階なのでスマホ禁止")]
#endif
            public static class GamePadObservable
            {
                public enum Player { Player1 = 1, Player2, Player3, Player4 }//コントローラーID
                public enum ButtonCode { A, B, X, Y, L1, R1, SELECT, START }//各種ボタン
                public enum AxisCode { Horizontal, Vertical }//ゲームパッドのスティックの情報

                private static readonly Func<ButtonCode, String> ButtonCodeToInputSignal = button => button.ToString();
                private static readonly Func<AxisCode, String> AxisCodeToInputSignal = axis => axis.ToString();

                //GetAnyButton:いずれかのボタンの入力に関してのObservable
                public static IObservable<Unit> GetAnyButtonDownObservable(Player gamePadId = Player.Player1)
                {
                    return
                        Observable.EveryUpdate()
                            .Where(_ => Enum.GetValues(typeof(ButtonCode)).Cast<ButtonCode>().Any(x => Input.GetButtonDown(gamePadId.ToString() + " " + ButtonCodeToInputSignal(x))))
                            .AsUnitObservable()
                            .Publish()
                            .RefCount();
                }

                public static IObservable<Unit> GetAnyButtonObservable(Player gamePadId = Player.Player1)
                {
                    return
                        Observable.EveryUpdate()
                            .Where(_ => Enum.GetValues(typeof(ButtonCode)).Cast<ButtonCode>().Any(x => Input.GetButton(gamePadId.ToString() + " " + ButtonCodeToInputSignal(x))))
                            .AsUnitObservable()
                            .Publish()
                            .RefCount();
                }

                public static IObservable<Unit> GetAnyButtonUpObservable(Player gamePadId = Player.Player1)
                {
                    return
                        Observable.EveryUpdate()
                            .Where(_ => Enum.GetValues(typeof(ButtonCode)).Cast<ButtonCode>().Any(x => Input.GetButtonUp(gamePadId.ToString() + " " + ButtonCodeToInputSignal(x))))
                            .AsUnitObservable()
                            .Publish()
                            .RefCount();
                }

                //GetButtons:指定したボタン群の中で、いずれかのボタンの入力に関してのObservable
                public static IObservable<Unit> GetButtonsDownObservable(Player gamePadId, params ButtonCode[] buttons)
                {
                    return
                        Observable.EveryUpdate()
                            .Where(_ => buttons.Any(x => Input.GetButtonDown(gamePadId.ToString() + " " + ButtonCodeToInputSignal(x))))
                            .AsUnitObservable()
                            .Publish()
                            .RefCount();
                }

                public static IObservable<Unit> GetButtonsDownObservable(params ButtonCode[] buttons)
                {
                    return
                       GetButtonsDownObservable(Player.Player1, buttons);
                }

                public static IObservable<Unit> GetButtonsObservable(Player gamePadId, params ButtonCode[] buttons)
                {
                    return
                        Observable.EveryUpdate()
                            .Where(_ => buttons.Any(x => Input.GetButton(gamePadId.ToString() + " " + ButtonCodeToInputSignal(x))))
                            .AsUnitObservable()
                            .Publish()
                            .RefCount();
                }

                public static IObservable<Unit> GetButtonsObservable(params ButtonCode[] buttons)
                {
                    return
                       GetButtonsObservable(Player.Player1, buttons);
                }

                public static IObservable<Unit> GetButtonsUpObservable(Player gamePadId, params ButtonCode[] buttons)
                {
                    return
                        Observable.EveryUpdate()
                            .Where(_ => buttons.Any(x => Input.GetButtonUp(gamePadId.ToString() + " " + ButtonCodeToInputSignal(x))))
                            .AsUnitObservable()
                            .Publish()
                            .RefCount();
                }

                public static IObservable<Unit> GetButtonsUpObservable(params ButtonCode[] buttons)
                {
                    return
                       GetButtonsDownObservable(Player.Player1, buttons);
                }

                public static IObservable<Unit> GetButtonDownObservable(ButtonCode button, Player gamePadId = Player.Player1)
                {
                    return
                        Observable.EveryUpdate()
                            .Where(_ => Input.GetButtonDown(gamePadId.ToString() + " " + ButtonCodeToInputSignal(button)))
                            .AsUnitObservable()
                            .Publish()
                            .RefCount();
                }

                public static IObservable<Unit> GetButtonObservable(ButtonCode button, Player gamePadId = Player.Player1)
                {
                    return
                        Observable.EveryUpdate()
                            .Where(_ => Input.GetButton(gamePadId.ToString() + " " + ButtonCodeToInputSignal(button)))
                            .AsUnitObservable()
                            .Publish()
                            .RefCount();
                }

                public static IObservable<Unit> GetButtonUpObservable(ButtonCode button, Player gamePadId = Player.Player1)
                {
                    return
                        Observable.EveryUpdate()
                            .Where(_ => Input.GetButtonUp(gamePadId.ToString() + " " + ButtonCodeToInputSignal(button)))
                            .AsUnitObservable()
                            .Publish()
                            .RefCount();
                }

                //スティック周りのObservable

                //縦方向のスティックの監視
                public static IObservable<float> GetAxisHorizontalObservable(Player gamePadId = Player.Player1)
                {
                    return
                        Observable.EveryUpdate()
                        .Select(x => Input.GetAxis(gamePadId.ToString() + " " + AxisCodeToInputSignal(AxisCode.Horizontal)))
                        .Publish()
                        .RefCount();
                }

                //横方向のスティックの監視
                public static IObservable<float> GetAxisVerticalObservable(Player gamePadId = Player.Player1)
                {
                    return
                        Observable.EveryUpdate()
                        .Select(x => Input.GetAxis(gamePadId.ToString() + " " + AxisCodeToInputSignal(AxisCode.Vertical)))
                        .Publish()
                        .RefCount();
                }

                //統合したスティックの監視
                //StickInfoユニットとして情報を管理
                public static IObservable<GamepadStickInput.StickInfo> GetAxisStickObservable(Player gamePadId = Player.Player1)
                {
                    return
                    GetAxisHorizontalObservable(gamePadId)
                        .Zip(GetAxisVerticalObservable(gamePadId), GamepadStickInput.GamePadStick)
                        .Publish()
                        .RefCount();
                }
            }
        }

    }
    namespace ColiderRx
    {
        /// <summary>
        /// コライダー周りのObservable
        /// Enter:接触した時
        /// Exit:乖離した時
        /// Stay:接触している時
        /// 
        /// また、タグを引数にとってコライダー指定もできる
        /// </summary>
#if UNITY_ANDROID || UNITY_UNITY_IOS
        [Obsolete("最適化されてない開発段階なのでスマホ禁止")]
#endif
        public class ColiderObservable : MonoBehaviour
        {
            public static IObservable<Collider> OnTriggerEnterObservable(GameObject gameobject)
            {
                return gameobject.OnTriggerEnterAsObservable();
            }

            public static IObservable<Collider> OnTriggerEnterWithTagObservable(GameObject gameobject, string tag)
            {
                return gameobject.OnTriggerEnterAsObservable()
                    .Where(x => x.tag == tag);
            }

            public static IObservable<Collider> OnTriggerExitObservable(GameObject gameobject)
            {
                return gameobject.OnTriggerExitAsObservable();
            }

            public static IObservable<Collider> OnTriggerExitWithTagObservable(GameObject gameobject, string tag)
            {
                return gameobject.OnTriggerExitAsObservable()
                    .Where(x => x.tag == tag);
            }

            public static IObservable<Collider> OnTriggerStayObservable(GameObject gameobject)
            {
                return gameobject.OnTriggerStayAsObservable();
            }

            public static IObservable<Collider> OnTriggerStayWithTagObservable(GameObject gameobject, string tag)
            {
                return gameobject.OnTriggerStayAsObservable()
                    .Where(x => x.tag == tag);
            }

            public static IObservable<Unit> TriggerHoldObservable(GameObject gameobject, float duration = 2.0f)
            {
                return
                   OnTriggerEnterObservable(gameobject)
                   .SelectMany(Observable.Timer(TimeSpan.FromSeconds(duration)))
                   .TakeUntil(OnTriggerExitObservable(gameobject))
                   .AsUnitObservable()
                   .RepeatUntilDestroy(gameobject);
            }

            public static IObservable<Unit> TriggerHoldWithTagObservable(GameObject gameobject, string tag, float duration = 2.0f)
            {
                return
                   OnTriggerEnterObservable(gameobject)
                   .Where(x => x.tag == tag)
                   .SelectMany(Observable.Timer(TimeSpan.FromSeconds(duration)))
                   .TakeUntil(OnTriggerExitObservable(gameobject))
                   .AsUnitObservable()
                   .RepeatUntilDestroy(gameobject);
            }
        }
    }

    namespace Animation
    {
        public class AnimatorObservable : StateMachineBehaviour
        {
            private Subject<AnimatorStateInfo> _onStateEnterSubject;

            public IObservable<AnimatorStateInfo> OnStateEnterObservable() {
                return _onStateEnterSubject.AsObservable() ?? (_onStateEnterSubject = new Subject<AnimatorStateInfo>()).AsObservable();
            }

            public IObservable<AnimatorStateInfo> OnStateEnterObservable(Animator animator)
            {
                AnimatorObservable smb = animator.GetBehaviour<AnimatorObservable>();

                return smb.OnStateEnterObservable();
            }

            public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            {
                _onStateEnterSubject.OnNext(stateInfo);
            }

            private Subject<AnimatorStateInfo> _onStateExitSubject;

            public IObservable<AnimatorStateInfo> OnStateExitObservable()
            {
                return _onStateExitSubject.AsObservable() ?? (_onStateExitSubject = new Subject<AnimatorStateInfo>()).AsObservable();
            }

            public IObservable<AnimatorStateInfo> OnStateExitObservable(Animator animator)
            {
                AnimatorObservable smb = animator.GetBehaviour<AnimatorObservable>();

                return smb.OnStateExitObservable();
            }

            public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            {
                _onStateExitSubject.OnNext(stateInfo);
            }

            private Subject<int> _onStateMachineEnterSubject;

            public IObservable<int> OnStateMachineEnterObservable()
            {
                return _onStateMachineEnterSubject.AsObservable() ?? (_onStateMachineEnterSubject = new Subject<int>()).AsObservable();
            }

            public IObservable<int> OnStateMachineEnterObservable(Animator animator)
            {
                AnimatorObservable smb = animator.GetBehaviour<AnimatorObservable>();

                return smb.OnStateMachineEnterObservable();
            }

            public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
            {
                _onStateMachineEnterSubject.OnNext(stateMachinePathHash);
            }

            private Subject<int> _onStateMachineExitSubject;

            public IObservable<int> OnStateMachineExitObservable()
            {
                return _onStateMachineExitSubject.AsObservable() ?? (_onStateMachineExitSubject = new Subject<int>()).AsObservable();
            }

            public IObservable<int> OnStateMachineExitObservable(Animator animator)
            {
                AnimatorObservable smb = animator.GetBehaviour<AnimatorObservable>();

                return smb.OnStateMachineExitObservable();
            }

            public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
            {
                _onStateMachineExitSubject.OnNext(stateMachinePathHash);
            }

            private Subject<AnimatorStateInfo> _onStateMoveSubject;

            public IObservable<AnimatorStateInfo> OnStateMoveObservable()
            {
                return _onStateMoveSubject.AsObservable() ?? (_onStateMoveSubject = new Subject<AnimatorStateInfo>()).AsObservable();
            }

            public IObservable<AnimatorStateInfo> OnStateMoveObservable(Animator animator)
            {
                AnimatorObservable smb = animator.GetBehaviour<AnimatorObservable>();

                return smb.OnStateMoveObservable();
            }

            public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            {
                _onStateMoveSubject.OnNext(stateInfo);
            }

            private Subject<AnimatorStateInfo> _onStateUpdateSubject;

            public IObservable<AnimatorStateInfo> OnStateUpdateObservable()
            {
                return _onStateUpdateSubject.AsObservable() ?? (_onStateUpdateSubject = new Subject<AnimatorStateInfo>()).AsObservable();
            }

            public IObservable<AnimatorStateInfo> OnStateUpdateObservable(Animator animator)
            {
                AnimatorObservable smb = animator.GetBehaviour<AnimatorObservable>();

                return smb.OnStateUpdateObservable();
            }

            public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            {
                _onStateUpdateSubject.OnNext(stateInfo);
            }

            private Subject<AnimatorStateInfo> _onStateIkSubject;

            public IObservable<AnimatorStateInfo> OnStateIKObservable()
            {
                return _onStateIkSubject.AsObservable() ?? (_onStateIkSubject = new Subject<AnimatorStateInfo>()).AsObservable();
            }

            public IObservable<AnimatorStateInfo> OnStateIKObservable(Animator animator)
            {
                AnimatorObservable smb = animator.GetBehaviour<AnimatorObservable>();

                return smb.OnStateIKObservable();
            }

            public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            {
                _onStateIkSubject.OnNext(stateInfo);
            }
        }
    }

    namespace MusicEngine
    {
        
        /// <summary>
        /// 音に関するObservable（引用:MusicEngine）
        /// Timingユニット単位での監視や小節、拍といった監視も可能
        /// </summary>

        public class SoundObservable
        {

            public static ReactiveProperty<int> BarChangedObservable()
            {
                return Observable.EveryUpdate()
                    .Where(x => x != Music.Just.Bar)
                    .Select(_ => Music.Just.Bar)
                    .Publish()
                    .RefCount()
                    .ToReactiveProperty();
            }

            public static ReactiveProperty<int> BeatChangedObservable()
            {
                return Observable.EveryUpdate()
                    .Where(x => x != Music.Just.Beat)
                    .Select(_ => Music.Just.Beat)
                    .Publish()
                    .RefCount()
                    .ToReactiveProperty();
            }

            public static ReactiveProperty<int> UnitChangedObservable()
            {
                return Observable.EveryUpdate()
                    .Where(x => x != Music.Just.Unit)
                    .Select(_ => Music.Just.Unit)
                    .Publish()
                    .RefCount()
                    .ToReactiveProperty();
            }

            public static IObservable<Unit> TimingObservable(Timing time)
            {
                return Observable.EveryUpdate()
                    .Where(x => Music.Just.Equals(time))
                    .AsUnitObservable();
            }

        }
    }
}

