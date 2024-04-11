using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Extensions
{
    public static class Yields
    {
        private static readonly WaitForFixedUpdate WaitForEndOfFixedUpdate = new WaitForFixedUpdate();
        private static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
        private static readonly Dictionary<float, WaitForSeconds> WaitForSecondsDictionary = new Dictionary<float, WaitForSeconds>();

        public static WaitForEndOfFrame GetWaitForEndOfFrame()
            => WaitForEndOfFrame;

        public static WaitForFixedUpdate GetWaitForFixedUpdate()
            => WaitForEndOfFixedUpdate;

        public static WaitForSeconds GetWait(float time)
        {
            if (WaitForSecondsDictionary.TryGetValue(time, out WaitForSeconds wait))
                return wait;

            WaitForSecondsDictionary[time] = new WaitForSeconds(time);
            return WaitForSecondsDictionary[time];
        }
    }
}