using Game.Code.Game.Level;
using System.Collections;
using Game.Code.Extensions;
using UnityEngine;

namespace Game.Code.Game.Services
{
    [RequireComponent(typeof(Camera))]
    public class CameraService : MonoBehaviour
    {
        [SerializeField] private float _smooth = 1;
        
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

            SetCameraInPosition(_target.position);
            StartCoroutine(FollowTarget());
        }

        private IEnumerator FollowTarget()
        {
            while (true)
            {
                yield return null;
                
                Vector2 desiredPosition = _target.position;

                var halfHeight = _camera.orthographicSize;
                var halfWidth = halfHeight * _camera.aspect;

                desiredPosition.x = Mathf.Clamp(desiredPosition.x, _leftBottomBorder.x + halfWidth, _rightTopBorder.x - halfWidth);
                desiredPosition.y = Mathf.Clamp(desiredPosition.y, _leftBottomBorder.y + halfHeight, _rightTopBorder.y - halfHeight);

                var adjustedPosition = Vector2.Lerp(_camera.transform.position, desiredPosition, Time.deltaTime * _smooth);
                SetCameraInPosition(adjustedPosition);
            }
        }

        private void SetCameraInPosition(Vector3 pos)
        {
            pos.z = _camera.transform.position.z;
            _camera.transform.SetInPosition(pos);
        }
    }
}