using UnityEngine;

namespace Game.Code.Game.Scene
{
    public class SceneDependenciesProvider
    {
        public readonly Transform UIRoot;
        public readonly Camera MainCamera;

        public SceneDependenciesProvider(Transform uiRoot, Camera mainCamera)
        {
            UIRoot = uiRoot;
            MainCamera = mainCamera;
        }
    }
}