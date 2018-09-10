using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//nullとかみるあれ
namespace UdonLib.Commons
{
    public static class ExtensionGameObject
    {
        public static bool HasComponent<T>(this GameObject go) where T : Component
        {
            if(go == null)
            {
                return false;
            }

            return go.GetComponent<T>() != null;
        }

        public static bool HasComponent<T>(this GameObject go, out T component) where T : Component
        {
            if (go == null)
            {
                component = null;
                return false;
            }
            component = go.GetComponent<T>();

            return component != null;
        }

        public static void DestroyComponents<T>() where T : Component
        {
            var obj = Object.FindObjectsOfType<T>();
            obj.ForEach<T>(Object.DestroyImmediate);
        }

        public static T AddComponentSafe<T>(this UdonBehaviour obj) where T : Component
        {
            if (null == obj || obj.IsDestroyed)
            {
                InstantLog.StringLogWarning("unsafe AddComponent call");
                return null;
            }

            return obj.gameObject.AddComponent<T>();
        }

        public static T GetComponentAddSafe<T>(this GameObject obj) where T : Component
        {
            if (null == obj.GetComponent<T>())
            {
                InstantLog.StringLogWarning("unsafe GetComponent call");
                obj.AddComponent<T>();
                return null;
            }

            return obj.GetComponent<T>();
        }

        public static T GetComponentSafe<T>(this GameObject obj) where T : Component
        {
            if (null == obj.GetComponent<T>())
            {
                InstantLog.StringLogWarning("unsafe GetComponent call");
                return null;
            }

            return obj.GetComponent<T>();
        }

        public static T GetComponentInChildrenSafe<T>(this GameObject obj) where T : Component
        {
            if (null == obj.GetComponentInChildren<T>())
            {
                InstantLog.StringLogWarning("unsafe GetComponent call");
                return null;
            }

            return obj.GetComponentInChildren<T>();
        }

        public static T GetComponentInChildrenAddSafe<T>(this GameObject obj) where T : Component
        {
            if (null == obj.GetComponentInChildren<T>())
            {
                InstantLog.StringLogWarning("unsafe GetComponent call");
                GameObject _obj = new GameObject();
                _obj.transform.SetParent(obj.transform);
                _obj.AddComponent<T>();
                return null;
            }

            return obj.GetComponentInChildren<T>();
        }

        public static T GetComponentInParentSafe<T>(this GameObject obj) where T : Component
        {
            if (null == obj.GetComponentInParent<T>())
            {
                InstantLog.StringLogWarning("unsafe GetComponent call");
                return null;
            }

            return obj.GetComponentInParent<T>();
        }

        public static T GetComponentInParentAddSafe<T>(this GameObject obj) where T : Component
        {
            if (null == obj.GetComponentInParent<T>())
            {
                InstantLog.StringLogWarning("unsafe GetComponent call");
                GameObject _obj = new GameObject();
                obj.transform.SetParent(_obj.transform);
                _obj.AddComponent<T>();
                return null;
            }

            return obj.GetComponentInParent<T>();
        }



    }
}
