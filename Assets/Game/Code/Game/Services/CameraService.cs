using Game.Code.Extensions;
using Game.Code.Game.Level;
using System.Collections;
using UnityEngine;
using Fusion;

namespace Game.Code.Game.Services
{
    [RequireComponent(typeof(Camera))]
    [ScriptHelp(BackColor = ScriptHeaderBackColor.Olive)]
    public class CameraService : NetworkBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _smooth = 1;
        
        private Transform _target;

        [Networked] private Vector3 LeftBottomBorder { get; set; }
        [Networked] private Vector3 RightTopBorder { get; set; }

        public void SetTarget(Transform target)
        {
            SetCameraInPosition(target.position);
            StartCoroutine(FollowTarget(target));
        }

        public override void Spawned()
        {
            Debug.Log($"<color=white>Spawned</color>");
        }

        public void SetLevelBorders(LevelModel levelModel)
        {
            Spawned();
            
            Debug.Log($"<color=white>Set level borders</color>");
            LeftBottomBorder = levelModel.LeftBottomCameraBorder.position;
            RightTopBorder = levelModel.RightTopCameraBorder.position;
        }

        private IEnumerator FollowTarget(Transform target)
        {
            while (true)
            {
                yield return null;
                
                Vector2 desiredPosition = target.position;

                var halfHeight = _camera.orthographicSize;
                var halfWidth = halfHeight * _camera.aspect;

                desiredPosition.x = Mathf.Clamp(desiredPosition.x, LeftBottomBorder.x + halfWidth, RightTopBorder.x - halfWidth);
                desiredPosition.y = Mathf.Clamp(desiredPosition.y, LeftBottomBorder.y + halfHeight, RightTopBorder.y - halfHeight);

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