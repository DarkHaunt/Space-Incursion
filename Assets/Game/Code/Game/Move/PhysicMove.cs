using UnityEngine;

namespace Game.Code.Game
{
    public class PhysicMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        private float _speed;

        public Vector2 Position
            => _rigidbody.position;

        public void SetMoveSpeed(float speed) =>
            _speed = speed;

        public void RotateToFace(Vector2 direction)
        {
            var angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
            _rigidbody.MoveRotation(Quaternion.Euler(new Vector3(0f, 0f, angle)));
        }

        public void Move(Vector2 direction) =>
            _rigidbody.velocity = direction * _speed;
    }
}