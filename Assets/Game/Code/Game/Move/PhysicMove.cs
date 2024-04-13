using UnityEngine;

namespace Game.Code.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicMove : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private float _speed;

        public void Construct(float speed)
        {
            _speed = speed;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction) =>
            _rigidbody.velocity = direction * _speed;

        public void Stop() =>
            _rigidbody.velocity = Vector2.zero;
    }
}