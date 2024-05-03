using Cysharp.Threading.Tasks;
using Fusion;
using Game.Code.Infrastructure.SceneManaging;

namespace Game.Code.Game
{
    public class NetworkSceneLoader
    {
        private readonly TransitionHandler _transitionHandler;

        public NetworkSceneLoader(TransitionHandler transitionHandler)
        {
            _transitionHandler = transitionHandler;
        }

        public async UniTask LoadSceneNetwork(NetworkRunner runner, Scenes scene)
        {
            if (!runner.IsServer)
                return;

            await _transitionHandler.PlayFadeInAnimation();
            await runner.LoadScene(scene.ToString());
            await _transitionHandler.PlayFadeOutAnimation();
        }
    }
}