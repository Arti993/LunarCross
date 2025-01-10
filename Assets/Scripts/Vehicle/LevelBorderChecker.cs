using Data;
using Infrastructure.Services.Factories.ParticleSystemFactory;
using Infrastructure.Services.ScreenFader;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using LevelGeneration;
using Reflex.Extensions;
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

        private void Construct()
        {
            _uiStateMachine = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IUiStateMachine>();

            _particleSystemFactory = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IParticleSystemFactory>();

            _screenFader = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IScreenFader>();
        }

        private void Awake()
        {
            Construct();
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
