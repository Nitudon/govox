using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UdonLib.Commons
{
    public static class NearObject{

        /// <summary>
        /// 最も近いオブジェクトを返す
        /// </summary>
        /// <typeparam name="T">任意の型</typeparam>
        /// <param name="obj">基準となるオブジェクト</param>
        /// <returns>最も近いオブジェクトT</returns>
        public static T NearestObject<T>(this T obj) where T : MonoBehaviour
        {
            var list = Object.FindObjectsOfType<T>();
            return list.OrderByDescending(x => (x.gameObject.transform.position - obj.gameObject.transform.position).sqrMagnitude).FirstOrDefault();
        }

        /// <summary>
        /// 最も近いオブジェクトを返す
        /// </summary>
        /// <typeparam name="T">任意の型</typeparam>
        /// <param name="obj">基準となるオブジェクト</param>
        /// <param name="list">比較するオブジェクトの集合</param>
        /// <returns>最も近いオブジェクトT</returns>
        public static T NearestObject<T>(this T obj, IEnumerable<T> list) where T : MonoBehaviour
        {
            return list.OrderByDescending(x => (x.gameObject.transform.position - obj.gameObject.transform.position).sqrMagnitude).FirstOrDefault();
        }

        /// <summary>
        /// 最も近いオブジェクトを返す
        /// </summary>
        /// <typeparam name="T">任意の型</typeparam>
        /// <param name="obj">基準となるオブジェクト</param>
        /// <param name="range">対象の範囲内</param>
        /// <returns>最も近いオブジェクトT</returns>
        public static T NearestObject<T>(this T obj, float range) where T : MonoBehaviour
        {
            return obj.NearObjects(range).OrderByDescending(x => (x.gameObject.transform.position - obj.gameObject.transform.position).sqrMagnitude).FirstOrDefault();
        }

        /// <summary>
        /// 最も近いオブジェクトを返す
        /// </summary>
        /// <typeparam name="T">任意の型</typeparam>
        /// <param name="obj">基準となるオブジェクト</param>
        /// <param name="list">比較するオブジェクトの集合</param>
        /// <param name="range">対象の範囲内</param>
        /// <returns>最も近いオブジェクトT</returns>
        public static T NearestObject<T>(this T obj, IEnumerable<T> list, float range) where T : MonoBehaviour
        {
             return obj.NearObjects(list,range).OrderByDescending(x => (x.gameObject.transform.position - obj.gameObject.transform.position).sqrMagnitude).FirstOrDefault();
        }

        /// <summary>
        /// 近いオブジェクト群を返す
        /// </summary>
        /// <typeparam name="T">任意の型</typeparam>
        /// <param name="obj">基準となるオブジェクト</param>
        /// <param name="range">対象の範囲内</param>
        /// <returns>近いオブジェクト群</returns>
        public static IEnumerable<T> NearObjects<T>(this T obj, float range) where T : MonoBehaviour
        {
            var list = Object.FindObjectsOfType<T>();
            return list.Where(x => (x.gameObject.transform.position - obj.gameObject.transform.position).sqrMagnitude < range * range);
        }

        /// <summary>
        /// 近いオブジェクト群を返す
        /// </summary>
        /// <typeparam name="T">任意の型</typeparam>
        /// <param name="obj">基準となるオブジェクト</param>
        /// <param name="list">比較するオブジェクトの集合</param>
        /// <param name="range">対象の範囲内</param>
        /// <returns>近いオブジェクト群</returns>
        public static IEnumerable<T> NearObjects<T>(this T obj, IEnumerable<T> list, float range) where T : MonoBehaviour
        {
            return list.Where(x => (x.gameObject.transform.position - obj.gameObject.transform.position).sqrMagnitude < range * range);
        }

    }
}
