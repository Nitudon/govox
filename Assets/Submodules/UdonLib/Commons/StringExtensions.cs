using UnityEngine;

namespace UdonLib.Commons.Extensions
{
    /// <summary>
    /// stringの拡張メソッド
    /// </summary>
    public static class StringExtensions
    {
        // 空文字判定
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return System.String.IsNullOrWhiteSpace(str);
        }
    }
}
