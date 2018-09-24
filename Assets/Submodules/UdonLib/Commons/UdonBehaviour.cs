using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdonLib.Commons {
    public class UdonBehaviour : MonoBehaviour {

        //Cached Transform
        public Transform CachedTransform
        {
            get { return _transform ?? (_transform = GetComponent<Transform>()); }
            set
            {
                _transform = value;
            }
        }

        //Cached RigitBody
        public Rigidbody CachedRigitbody
        {
            get
            {
                if (null != _rigitbody) return _rigitbody;
                if(GetComponent<Rigidbody>() == null)
                {
                    AddComponent<Rigidbody>();
                }
                _rigitbody = GetComponent<Rigidbody>();
                return _rigitbody;
            }
            set
            {
                _rigitbody = value;
            }
        }

        //内部のキャッシュ
        private Transform _transform;
        private Rigidbody _rigitbody;

        //コールバック的な
        public delegate void callback();
        private callback _onEnable;
        private callback _onDisable;
        private callback _onDestroy;
        protected void SetOnEnable(callback method) { _onEnable += method; }
        protected void SetOnDisable(callback method) { _onDisable += method; }
        protected void SetOnDestroy(callback method) { _onDestroy += method; }
        //nullじゃないので
        public bool IsDestroyed { get; private set; }

        //Transformへのアクセスをやりやすく
        #region[Property of Extending Transform Access]
        public Vector3 position { get { return transform.position; } }
        public Vector3 localPosition { get { return transform.localPosition; } }

        public float posX { get { return transform.localPosition.x; } set { SetLocalPosition(value, posY, posZ); } }
        public float posY { get { return transform.localPosition.y; } set { SetLocalPosition(posX, value, posZ); } }
        public float posZ { get { return transform.localPosition.z; } set { SetLocalPosition(posX, posY, value); } }

        public float RotX { get { return transform.localEulerAngles.x; } set { SetLocalEulerAngles(value, RotY, RotZ); } }
        public float RotY { get { return transform.localEulerAngles.y; } set { SetLocalEulerAngles(RotX, value, RotZ); } }
        public float RotZ { get { return transform.localEulerAngles.z; } set { SetLocalEulerAngles(RotX, RotY, value); } }

        public float ScaleX { get { return transform.localScale.x; } set { SetLocalPosition(value, ScaleY, ScaleZ); } }
        public float ScaleY { get { return transform.localScale.y; } set { SetLocalPosition(ScaleX, value, ScaleZ); } }
        public float ScaleZ { get { return transform.localScale.z; } set { SetLocalPosition(ScaleX, ScaleY, value); } }

        public void SetPosition(Vector3 vec) { transform.position = vec; }
        public void SetLocalPosition(Vector3 vec) { transform.localPosition = vec; }
        public void SetPosition(float x, float y, float z) { transform.position = new Vector3(x, y, z); }
        public void SetLocalPosition(float x, float y, float z) { transform.localPosition = new Vector3(x, y, z); }

        public void SetRotation(Quaternion qu) { transform.rotation = qu; }
        public void SetLocalRotation(Quaternion qu) { transform.localRotation = qu; }
        public void SetLocalEulerAngles(Vector3 vec) { transform.localEulerAngles = vec; }
        public void SetLocalEulerAngles(float x, float y, float z) { transform.localEulerAngles = new Vector3(x,y,z); }
        public void SetScale(Vector3 vec) { transform.localScale = vec; }
        public void SetScale(float x, float y, float z) { transform.localScale = new Vector3(x, y, z); }
        #endregion

        public virtual void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        //Component単位のactive
        public virtual void SetEnable<T>(bool able) where T : Component
        {
            T component = GetComponent<T>();
            if (component == null)
            {
                InstantLog.StringLogError("This object doesn't have any designed components.");
                return;
            }
            
           (GetComponent<T>() as MonoBehaviour).enabled = able;
        }

        //コンポーネント指向としておかしい気がしないでもないけどアクセス短縮
        private void AddComponent<T>() where T : Component
        {
            gameObject.AddComponent<T>();
        }

        //フラグ立てて消える
        public void Destroy()
        {
            if (_onDestroy != null)
            {
                _onDestroy();
            }
            Destroy(gameObject);
            IsDestroyed = true;
        }

        #region[MonoBehaviour Method]
        protected virtual void Awake()
        {

        }

        protected virtual void Start() {
            IsDestroyed = false;
        }

        protected virtual void Update() {

        }

        protected virtual void OnEnable()
        {
            if (_onEnable != null) _onEnable();
        }

        protected virtual void OnDisable()
        {
            if(_onDisable != null) _onDisable();
        }
        #endregion
    }
}