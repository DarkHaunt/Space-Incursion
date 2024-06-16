using System.Collections.Generic;
using UnityEngine;

namespace Game.Code.Game.Scene
{
    public class SceneDependenciesProvider
    {
        public readonly IReadOnlyList<Transform> PlayerSpawnPoints;
        public readonly Transform UIRoot;

        public readonly Camera MainCamera;

        public SceneDependenciesProvider(Transform uiRoot, Camera mainCamera, List<Transform> playerSpawnPoints)
        {
            PlayerSpawnPoints = playerSpawnPoints;
            MainCamera = mainCamera;
            UIRoot = uiRoot;
        }
    }
}