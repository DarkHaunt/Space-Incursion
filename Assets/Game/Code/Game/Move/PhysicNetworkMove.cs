using Fusion;
using Fusion.Addons.Physics;
using UnityEngine;

namespace Game.Code.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicNetworkMove : MonoBehaviour
    {
        [SerializeField] private NetworkRigidbody2D _networkRigidbody;

        private float _speed;

        public void Construct(float speed)
        {
            _speed = speed;
        }

        public void RotateToFace(Vector2 direction)
        {
            var tan = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, tan));
        }

        public void Move(Vector3 direction, float timeStep) =>
            transform.position += direction * (_speed * timeStep);
    }
}