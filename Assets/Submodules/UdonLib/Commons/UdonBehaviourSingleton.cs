using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdonLib.Commons {
    public class UdonBehaviourSingleton<T> : UdonBehaviour where T : UdonBehaviour
    {
        public static string ObjectName
        {
            get
            {
                return _instance.gameObject.name;
            }
        }

        public static string ComponentName
        {
            get
            {
                return Instance.GetType().FullName;
            }
        }

        public static GameObject InstanceObject
        {
            get
            {
                if (_instance == null)
                {
                    CreateInstance();
                }
                return _instance.gameObject;
            }
        }

        protected virtual void Init()
        {

        }

        public static void CreateInstance() 
        {
            if(null != _instance)
            {
                InstantLog.StringLogWarning("Instance has already maked");
                return;
            }
            else
            {
                GameObject instanceObject = new GameObject(ComponentName);
                instanceObject.AddComponent<T>();
            }
        }

        public static void CreateInstance(Transform parent)
        {
            if (null != _instance)
            {
                InstantLog.StringLogWarning("Instance has already maked");
                return;
            }
            else
            {
                GameObject instanceObject = new GameObject(ComponentName);
                instanceObject.transform.SetParent(parent);
                instanceObject.AddComponent<T>();
            }
        }

        public static bool HasInstance
        {
            get
            {
                return !(null == _instance);
            }
        }

        protected static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    Type t = typeof(T);

                    _instance = (T)FindObjectOfType(t);
                    if (_instance == null)
                    {
                        Debug.LogError("No Objects have component of " + t);
                    }
                }

                return _instance;
            }
        }

        protected sealed override void Awake()
        {
            if (this != Instance)
            {
                Destroy(this);
                //Destroy(this.gameObject);
                Debug.LogError(typeof(T) + " has already attached by" + Instance.gameObject.name);
                return;
            }

            Init();

        }
    }
}