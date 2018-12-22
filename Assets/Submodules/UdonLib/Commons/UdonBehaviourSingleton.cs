using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdonLib.Commons
{
    public class UdonBehaviourSingleton<T> : InitializableMono where T : InitializableMono
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
                T instance = instanceObject.AddComponent<T>();
                _instance = instance;
                _instance.Initialize();
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
                T instance = instanceObject.AddComponent<T>();
                _instance = instance;
                _instance.Initialize();
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
                        CreateInstance();
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
        }

        public override void Initialize()
        {
            if(IsDontDestroy)
            {
                DontDestroyOnLoad(this);
            }
        }

        protected virtual bool IsDontDestroy
        {
            get
            {
                return false;
            }
        }
    }
}