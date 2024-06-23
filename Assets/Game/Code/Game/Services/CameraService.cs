using Cysharp.Threading.Tasks;
using Game.Code.Game.Scene;
using Game.Code.Extensions;
using VContainer.Unity;
using System.Threading;
using UnityEngine;
using System;

namespace Game.Code.Game.Services
{
    public class CameraService : IInitializable, IDisposable
    {
        private readonly SceneDependenciesProvider _dependenciesProvider;
        
        private CancellationTokenSource _cts;
        
        private Vector3 _leftBottomBorder;
        private Vector3 _rightTopBorder;
        
        private Transform _target;
        private Camera _camera;
        

        public CameraService(SceneDependenciesProvider dependenciesProvider)
        {
            _dependenciesProvider = dependenciesProvider;
            
            _cts = new CancellationTokenSource();
        }
        

        public void Initialize() =>
            _camera = _dependenciesProvider.MainCamera;

        public void SetTarget(Transform target)
        {
            SetCameraInPosition(target.position);
            StartFollowTarget(target);
        }

        public void SetLevelBorders(Vector2 leftBottomBorder, Vector2 rightTopBorder)
        {
            _leftBottomBorder = leftBottomBorder;
            _rightTopBorder = rightTopBorder;
        }

        public void CancelFollow() => 
            _cts?.Cancel();

        private void SetCameraInPosition(Vector3 pos)
        {
            pos.z = _camera.transform.position.z;
            _camera.transform.SetInPosition(pos);
        }

        private async void StartFollowTarget(Transform target)
        {
            while (!_cts.IsCancellationRequested)
            {
                await UniTask.Yield();
                
                Vector2 desiredPosition = target.position;

                var halfHeight = _camera.orthographicSize;
                var halfWidth = halfHeight * _camera.aspect;

                desiredPosition.x = Mathf.Clamp(desiredPosition.x, _leftBottomBorder.x + halfWidth, _rightTopBorder.x - halfWidth);
                desiredPosition.y = Mathf.Clamp(desiredPosition.y, _leftBottomBorder.y + halfHeight, _rightTopBorder.y - halfHeight);

                var adjustedPosition = Vector2.Lerp(_camera.transform.position, desiredPosition, Time.deltaTime);
                SetCameraInPosition(adjustedPosition);
            }
 
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
        }

        public void Dispose() =>
            _cts?.Dispose();
    }
}