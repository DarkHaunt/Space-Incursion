using System.Collections.Generic;
using Game.Code.Game.Services;
using UnityEngine;

namespace Game.Code.Game.Scene
{
    public class SceneDependenciesProvider
    {
        public readonly IReadOnlyList<Transform> PlayerSpawnPoints;
        public readonly Transform UIRoot;

        public readonly CameraService CameraService;
        public readonly Camera MainCamera;

        public SceneDependenciesProvider(Transform uiRoot, Camera mainCamera, List<Transform> playerSpawnPoints, CameraService cameraService)
        {
            PlayerSpawnPoints = playerSpawnPoints;
            CameraService = cameraService;
            MainCamera = mainCamera;
            UIRoot = uiRoot;
        }
    }
}