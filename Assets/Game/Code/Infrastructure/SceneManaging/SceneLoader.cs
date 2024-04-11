using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure
{
    public class SceneLoader
    {
        public UniTask Load(Scenes scene, Action onLoaded = null) =>
            LoadSceneAsync(scene.ToString(), LoadSceneMode.Single, onLoaded);

        public UniTask UnloadScene(Scenes scene, Action onUnloaded = null) =>
            UnloadSceneAsync(scene.ToString(), onUnloaded);

        public UniTask LoadInAdditiveMode(Scenes scene, Action onLoaded = null) =>
            LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive, onLoaded);

        private async UniTask LoadSceneAsync(string nextScene, LoadSceneMode loadMode, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                return;
            }
            
            await SceneManager
                .LoadSceneAsync(nextScene, loadMode)
                .ToUniTask();
            
            onLoaded?.Invoke();
        }

        private async UniTask UnloadSceneAsync(string sceneName, Action onUnloaded = null)
        {
            await SceneManager
                .UnloadSceneAsync(sceneName)
                .ToUniTask();
            
            onUnloaded?.Invoke();
        }
    }
}