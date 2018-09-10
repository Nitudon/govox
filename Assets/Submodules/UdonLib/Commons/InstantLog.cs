using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdonLib.Commons
{
    //Logを使いやすく
    public static class InstantLog
    {
        [Conditional("UNITY_EDITOR")]
        public static void CheckLog()
        {
            //ブレーク
            UnityEngine.Debug.Log("check");
        }

        [Conditional("UNITY_EDITOR")]
        public static void StringLog(string str)
        {
            //いつもの
            UnityEngine.Debug.Log(str);
        }

        [Conditional("UNITY_EDITOR")]
        public static void StringLogWarning(string str)
        {
            //いつもの
            UnityEngine.Debug.LogWarning(str);
        }

        [Conditional("UNITY_EDITOR")]
        public static void StringLogError(string str)
        {
            //いつもの
            UnityEngine.Debug.LogError(str);
        }

        [Conditional("UNITY_EDITOR")]
        public static void ObjectLog(object o)
        {
            //使うとき違いそうなんで分離
            UnityEngine.Debug.Log(o);
        }

        [Conditional("UNITY_EDITOR")]
        public static void ObjectLog(params object[] o)
        {
            //複数で使うかと思って
            for(int i = 0;i < o.Length;++i)
            UnityEngine.Debug.Log(o[i]);
        }

        [Conditional("UNITY_EDITOR")]
        public static void LineLog()
        {
            //セクション分離に
            UnityEngine.Debug.Log("-------------------------------------");
        }

        //カラーリングの適応
        #region[Color]
        [Conditional("UNITY_EDITOR")]
        public static void CheckLog(StringExtensions.TextColor color)
        {
            UnityEngine.Debug.Log("check".Coloring(color));
        }

        [Conditional("UNITY_EDITOR")]
        public static void StringLog(string str, StringExtensions.TextColor color)
        {
            UnityEngine.Debug.Log(str.Coloring(color));
        }

        [Conditional("UNITY_EDITOR")]
        public static void ObjectLog(object o, StringExtensions.TextColor color)
        {
            UnityEngine.Debug.Log(o.ToString().Coloring(color));
        }

        [Conditional("UNITY_EDITOR")]
        public static void ObjectLog(StringExtensions.TextColor color,params object[] o)
        {
            for (int i = 0; i < o.Length; ++i)
                UnityEngine.Debug.Log(o[i].ToString().Coloring(color));
        }

        [Conditional("UNITY_EDITOR")]
        public static void LineLog(StringExtensions.TextColor color)
        {
            UnityEngine.Debug.Log("-------------------------------------".Coloring(color));
        }
        #endregion
    }

    //リッチテキスト用の拡張メソッド
    #region[Extension]
    public static class StringExtensions
    {
        //リッチテキストのカラー
        public enum  TextColor {red,blue,yellow,white,cyan,brown,darkblue,green,grey,magenta,lightblue,lime,maroon,navy,olive,orange,purple,silver,teal};

        //色
        public static string Coloring(this string str, TextColor color)
        {
            return string.Format("<color={0}>{1}</color>", color, str);
        }

        //サイジング
        public static string Resize(this string str, int size)
        {
            return string.Format("<size={0}>{1}</size>", size, str);
        }

        //中くらい
        public static string Medium(this string str)
        {
            return str.Resize(11);
        }

        //小さく
        public static string Small(this string str)
        {
            return str.Resize(9);
        }

        //大きく
        public static string Large(this string str)
        {
            return str.Resize(16);
        }

        //太く
        public static string Bold(this string str)
        {
            return string.Format("<b>{0}</b>", str);
        }

        //斜めに
        public static string Italic(this string str)
        {
            return string.Format("<i>{0}</i>", str);
        }
    }
    #endregion
}
