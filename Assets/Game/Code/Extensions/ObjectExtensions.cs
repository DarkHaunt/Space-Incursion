using UnityEngine;

namespace Game.Scripts.Extensions
{
    public static class ObjectExtensions
    {
        public static T ValidateInstanceName<T>(this T instance) where T : Object
        {
            instance.name = instance.name.Replace("(Clone)", string.Empty);
            return instance;
        }   
        
        public static bool IsNull(this Object obj) =>
            (object)obj == null;

        public static bool IsNotNull(this Object obj) =>
            !IsNull(obj);
    }
}