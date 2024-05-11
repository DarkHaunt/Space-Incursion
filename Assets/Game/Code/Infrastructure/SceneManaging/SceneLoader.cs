using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Game.Code.Infrastructure.SceneManaging
{
    public class SceneLoader
    {
        private readonly TransitionHandler _transitionHandler;

        public SceneLoader(TransitionHandler transitionHandler)
        {
            _transitionHandler = transitionHandler;
        }
        
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
            
            await _transitionHandler.PlayFadeInAnimation();
            
            await SceneManager
                .LoadSceneAsync(nextScene, loadMode)
                .ToUniTask();
            
            onLoaded?.Invoke();
            
            await _transitionHandler.PlayFadeOutAnimation();
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