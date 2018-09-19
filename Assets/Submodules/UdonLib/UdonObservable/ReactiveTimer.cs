using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace UdonObservable
{
    namespace Commons
    {
        public static class ReactiveTimer
        {
            public static IReadOnlyReactiveProperty<int> ReactiveTimerForSeconds()
            {
                return Observable
                .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
                .Select(x => (int)(x + 1))
                .TakeWhile(x => x >= 0)
                .ToReactiveProperty();
            }

            public static IReadOnlyReactiveProperty<int> ReactiveTimerForSeconds(int count)
            {
                return Observable
                .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
                .Select(x => (int)(count - x))
                .TakeWhile(x => x >= 0)
                .ToReactiveProperty();
            }

            public static IObservable<int> ObservableTimerForSeconds()
            {
                return Observable
                .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
                .Select(x => (int)(x + 1))
                .TakeWhile(x => x >= 0);
            }

            public static IObservable<int> ObservableTimerForSeconds(int count)
            {
                return Observable
                .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
                .Select(x => (int)(count - x))
                .TakeWhile(x => x >= 0);
            }

        }
    }
}
