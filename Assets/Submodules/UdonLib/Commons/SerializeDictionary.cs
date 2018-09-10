using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UdonLib.Commons
{ 

    [System.Serializable]
    public class SerializableDictionary<TKey, TValue, TPair> where TPair: KeyAndValue<TKey, TValue>
    {
        [SerializeField]
        private List<TPair> list;
        private Dictionary<TKey, TValue> table;

        public SerializableDictionary()
        {
            list = new List<TPair>();
        }

        public Dictionary<TKey, TValue> GetDictionary()
        {
            if (table == null)
            {
                table = ConvertListToDictionary(list);
            }
            return table;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return GetDictionary().TryGetValue(key,out value);
        }

        public void SetValue(TKey key,TValue value)
        {
            TValue judge;
            if (TryGetValue(key, out judge))
            {
                table[key] = value;
            }
            else
            {
                table.Add(key, value);
            }
        }

        static Dictionary<TKey, TValue> ConvertListToDictionary(List<TPair> list)
        {
            Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();
            foreach (KeyAndValue<TKey, TValue> pair in list)
            {
                dic.Add(pair.Key, pair.Value);
            }
            return dic;
        }
    }

    [System.Serializable]
    public class KeyAndValue<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
    }
}