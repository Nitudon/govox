using UnityEngine;
using UniRx;
using UdonObservable.InputRx.GamePad;
using System;
using System.Collections;

public static class GamepadStickInput
{

    static Vector2 inputVector = new Vector2();
    static Vector3 rotateVector = new Vector3();

    public class StickInfo: IComparable<StickInfo>, IEquatable<StickInfo>
    {
        public float vert;
        public float hori;
        public float size;
        public Vector3 movePosition;
        public Vector3 eulerAngle;
        private bool enable;

        public StickInfo(bool _enable = false)
        {
            hori = 0;
            vert = 0;
            size = 0;
            movePosition = new Vector3();
            eulerAngle = new Vector3();
            enable = _enable;
        }

        public StickInfo(float _hori,float _vert,float _size,Vector3 _movePosition,Vector3 _eulerAngle,bool _enable = true)
        {
            hori = _hori;
            vert = _vert;
            size = _size;
            movePosition = _movePosition;
            eulerAngle = _eulerAngle;
            enable = _enable;
        }

        public void MovePosition(Transform transform,float speed = 0.1f)
        {
            if (enable)
            {
                transform.position += movePosition * speed;
            }
        }

        public void RotatePosition(Transform transform)
        {
            if (enable)
            {
                transform.eulerAngles = eulerAngle;
            }
        }

        public void CharacterControll(Transform transform,float speed = 0.1f,Action onMethod = null)
        {
            MovePosition(transform,speed);
            RotatePosition(transform);
            if(onMethod != null && enable)
            {
                onMethod.Invoke();
            }
        }

        public static bool operator >(StickInfo t, StickInfo t2) { return t.size > t2.size; }
        public static bool operator <(StickInfo t, StickInfo t2) { return !(t > t2) && !(t.Equals(t2)); }
        public static bool operator <=(StickInfo t, StickInfo t2) { return !(t > t2); }
        public static bool operator >=(StickInfo t, StickInfo t2) { return t > t2 || t.Equals(t2); }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(obj, null))
            {
                return false;
            }
            if (object.ReferenceEquals(obj, this))
            {
                return true;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            return this.Equals(obj as Timing);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(StickInfo other)
        {
            return (this.hori == other.hori && this.vert == other.vert && this.size == other.size);
        }

        public int CompareTo(StickInfo tother)
        {
            if (this.Equals(tother)) return 0;
            else if (this > tother) return 1;
            else return -1;
        }

        public override string ToString()
        {
            return String.Format("hori = {0},vert = {1},size = {2}",hori,vert,size);
        }
    }

    public static StickInfo GamePadStick(float hori, float vert)
    {
        StickInfo _inputStick;
        Vector3 _movePosition = new Vector3(hori, 0, -vert);

        if (hori != 0 || vert != 0)
        {
            inputVector.Set(hori,vert);
            inputVector.Normalize();
            rotateVector = InputAxisEulerAngle(inputVector);

            _inputStick = new StickInfo(hori,vert,inputVector.magnitude,_movePosition,rotateVector);
            return _inputStick;
        }

        else
        {
            return new StickInfo();
        }

    }

    private static Func<Vector2,Vector3> InputAxisEulerAngle = vec => new Vector3(0f, (Mathf.Atan2(vec.y, vec.x) / Mathf.PI * 180) + 90f, 0f);

}
