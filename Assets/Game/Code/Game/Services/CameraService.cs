using Game.Code.Game.Level;
using System.Collections;
using UnityEngine;

namespace Game.Code.Game.Services
{
    [RequireComponent(typeof(Camera))]
    public class CameraService : MonoBehaviour
    {
        private Vector3 _leftBottomBorder;
        private Vector3 _rightTopBorder;
        
        private Transform _target;
        private Camera _camera;

        public void Init(LevelModel levelModel)
        {
            _camera = GetComponent<Camera>();

            _leftBottomBorder = levelModel.LeftBottomCameraBorder.position;
            _rightTopBorder = levelModel.RightTopCameraBorder.position;
        }

        public void SetFollowTarget(Transform target)
        {
            _target = target;
            StartCoroutine(FollowTarget());
        }

        private IEnumerator FollowTarget()
        {
            while (true)
            {
                yield return null;
                
                var desiredPosition = _target.position;

                // Calculate the world space bounds of the camera's viewport
                var halfHeight = _camera.orthographicSize;
                var halfWidth = halfHeight * _camera.aspect;

                // Clamp the camera position to ensure its viewport does not exceed the borders
                desiredPosition.x = Mathf.Clamp(desiredPosition.x, _leftBottomBorder.x + halfWidth, _rightTopBorder.x - halfWidth);
                desiredPosition.y = Mathf.Clamp(desiredPosition.y, _leftBottomBorder.y + halfHeight, _rightTopBorder.y - halfHeight);

                // Maintain camera's z position
                desiredPosition.z = transform.position.z;

                // Smoothly move the camera to the clamped position
                transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
            }
        }
    }
}