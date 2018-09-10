using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UdonLib.Commons {
    //ForEach以外おまけ
    public static class ArrayExtension {

        public static void InvokeSafe<T>(this Action<T> action,T arg)
        {
            if (null == action)
            {
                InstantLog.StringLogWarning("Action Missing");
                return;
            }

            action(arg);
        }

        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            for(int i = 0; i < array.Length; ++i)
            {
                action(array[i]);
            }
        }

        public static void ForEachReverse<T>(this T[] array, Action<T> action)
        {
            for (int i = array.Length-1; i >= 0; --i)
            {
                action(array[i]);
            }
        }

        public static void ForEachCount<T>(this T[] array, Action<T,int> action)
        {
            for (int i = 0;i < array.Length;++i)
            {
                action(array[i],i);
            }
        }
    }
}
