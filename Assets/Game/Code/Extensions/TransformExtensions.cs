using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Extensions
{
    public static class TransformExtensions
    {
         public static IEnumerable<T> GetComponentsInChildrenOnly<T>(this Transform parent)
            => parent.GetComponentsInChildren<T>().ToList().Skip(1);

        public static void LookAt2DDirectionLocal(this Transform me, Vector2 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            me.localRotation = Quaternion.Euler(0f, 0f, angle);
        }

        public static void SetInPosition(this Transform t, Vector3 position)
        {
            Vector3 currentPos = t.position;
            currentPos.x = position.x;
            currentPos.y = position.y;
            currentPos.z = position.z;
            t.position = currentPos;
        }
        
        public static void SetRotation(this Transform t, Vector3 rotation)
            => t.rotation = Quaternion.Euler(rotation);

        public static void SetInLocalPosition(this Transform t, Vector3 position)
        {
            Vector3 currentPos = t.localPosition;
            currentPos.x = position.x;
            currentPos.y = position.y;
            currentPos.z = position.z;
            t.localPosition = currentPos;
        }
        
        public static void SetX(this Transform t, float value)
        {
            Vector3 v = t.position;
            v.x = value;
            t.position = v;
        }

        public static void SetY(this Transform t, float value)
        {
            Vector3 v = t.position;
            v.y = value;
            t.position = v;
        }

        public static void SetZ(this Transform t, float value)
        {
            Vector3 v = t.position;
            v.z = value;
            t.position = v;
        }

        public static void SetXY(this Transform t, float xValue, float yValue)
        {
            Vector3 v = t.position;
            v.x = xValue;
            v.y = yValue;
            t.position = v;
        }

        public static void SetLocalX(this Transform t, float value)
        {
            Vector3 v = t.localPosition;
            v.x = value;
            t.localPosition = v;
        }

        public static void SetLocalY(this Transform t, float value)
        {
            Vector3 v = t.localPosition;
            v.y = value;
            t.localPosition = v;
        }

        public static void SetLocalZ(this Transform t, float value)
        {
            Vector3 v = t.localPosition;
            v.z = value;
            t.localPosition = v;
        }

        public static void SetLocalScale(this Transform t, float value)
        {
            Vector3 v = t.localScale;
            v.Set(value, value, value);
            t.localScale = v;
        }

        public static void SetLocalScaleY(this Transform t, float value)
        {
            Vector3 v = t.localScale;
            v.y = value;
            t.localScale = v;
        }

        public static void SetLocalScale(this Transform t, Vector3 value)
        {
            t.localScale = value;
        }

        public static void ResetScale(this Transform t)
        {
            t.localScale = Vector3.one;
        }
    }
}