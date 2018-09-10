using UnityEngine;

namespace UdonLib.Commons.Extensions
{
    /// <summary>
    /// 整数型の拡張メソッド
    /// </summary>
    public static class IntExtension{

        /// <summary>
        /// 符号を反転した数を返します
        /// </summary>
        /// <param name="num">対象の数</param>
        /// <returns>対象の数の符号を反転した数</returns>
        public static int Reverse(this int num)
        {
            return num * -1;
        }

        /// <summary>
        /// 桁数を返します
        /// </summary>
        /// <param name="num">対象の数</param>
        /// <returns>対象の数の桁数</returns>
        public static int Digit(this int num)
        {
            return (num == 0) ? 1 : ((int)Mathf.Log10(num) + 1);
        }

        /// <summary>
        /// 指定した桁の数を返します、一の位が1です
        /// </summary>
        /// <param name="num"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int DigitNum(this int num, int index)
        {
            return (num >= (int)Mathf.Pow(10, index-1)) ?  (num / (int)Mathf.Pow(10, index-1)) % 10 : -1;
        }
    }
}
