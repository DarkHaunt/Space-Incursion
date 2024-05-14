using System;
using UnityEngine;

namespace Game.Code.Common.Physic
{
    public class CollisionObserver : MonoBehaviour
    {
        public event Action<Collision2D> OnCollideEnter;
        public event Action<Collision2D> OnCollideExit;


        private void OnCollisionEnter2D(Collision2D other)
            => OnCollideEnter?.Invoke(other);
        
        private void OnCollisionExit2D(Collision2D other)
            => OnCollideExit?.Invoke(other);
    }
}