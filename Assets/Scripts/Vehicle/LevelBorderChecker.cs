using Data;
using Infrastructure;
using Infrastructure.Services.Factories.ParticleSystemFactory;
using Infrastructure.Services.ScreenFader;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using LevelGeneration;
using Reflex.Attributes;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Vehicle
{
    public class LevelBorderChecker : MonoBehaviour
    {
        private bool _isFirstContactSucceed;
        private IUiStateMachine _uiStateMachine;
        private IParticleSystemFactory _particleSystemFactory;
        private IScreenFader _screenFader;
        
        [Inject]
        private void Construct(IUiStateMachine uiStateMachine, IParticleSystemFactory particleSystemFactory,
            IScreenFader screenFader)
        {
            _uiStateMachine = uiStateMachine;
            _particleSystemFactory = particleSystemFactory;
            _screenFader = screenFader;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out LevelBorder levelBorder) == false)
                return;

            if (_isFirstContactSucceed)
                return;

            _isFirstContactSucceed = true;

            Vector3 intersectionPoint =
                transform.GetComponent<Collider>().ClosestPointOnBounds(other.transform.position);

            _particleSystemFactory.ShowExplosionEffect(intersectionPoint);

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentSceneIndex == (int) SceneIndex.Tutorial)
            {
                TimePauserWithDelay timePauserWithDelay = new TimePauserWithDelay();

                _screenFader.FadeOutAndLoadScene((int) SceneIndex.Tutorial);

                _ = StartCoroutine(timePauserWithDelay.Pause());
            }
            else
            {
                _uiStateMachine.SetState<UiStateLevelFailed>();
            }
        }
    }
}
