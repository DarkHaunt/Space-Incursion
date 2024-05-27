using UnityEngine;

namespace Game.Code.Game
{
    public class PhysicMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        private float _speed;

        public Vector2 Position
            => _rigidbody.position;

        public void Construct(float speed) =>
            _speed = speed;

        public void RotateToFace(Vector2 direction)
        {
            var tan = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rigidbody.MoveRotation(Quaternion.Euler(new Vector3(0f, 0f, tan)));
        }

        public void Move(Vector2 direction, float timeStep)
        {
            var pos = (Vector2)transform.position + direction * (_speed * timeStep);
            _rigidbody.MovePosition(pos);
        }
    }
}