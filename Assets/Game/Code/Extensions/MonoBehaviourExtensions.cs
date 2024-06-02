using Game.Code.Common.CoroutineRunner;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static bool IsNull(this Object obj) =>
            (object)obj == null;

        public static bool IsNotNull(this Object obj) =>
            !IsNull(obj);

        public static void TryToStopCoroutine(this MonoBehaviour mono, Coroutine coroutine)
        {
            if (coroutine != null)
                mono.StopCoroutine(coroutine);
        }
        
        public static void TryToStopCoroutine(this ICoroutineRunner runner, IEnumerator coroutine)
        {
            if (coroutine != null)
                runner.StopRunningCoroutine(coroutine);
        }

        public static T ValidateInstanceName<T>(this T instance) where T : Object
        {
            instance.name = instance.name.Replace("(Clone)", string.Empty);
            return instance;
        }

        public static bool IsActive(this MonoBehaviour monoBehaviour) =>
            monoBehaviour.gameObject.activeInHierarchy;

        public static bool IsInactive(this MonoBehaviour monoBehaviour) =>
            !monoBehaviour.gameObject.activeInHierarchy;
    }
}