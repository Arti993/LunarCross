using System;
using System.Collections;
using System.Collections.Generic;
using Ami.BroAudio;
using Infrastructure.Services.AudioPlayback;
using Infrastructure.Services.Factories.ParticleSystemFactory;
using Infrastructure.Services.Factories.UiFactory;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using LevelGeneration.Entities.EntityStateMachine.Astronaut;
using Reflex.Extensions;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vehicle;
using Vehicle.BindPoints;

namespace LevelGeneration
{
    public class GravityRay : MonoBehaviour
    {
        private const float AttractionTime = 2;

        [SerializeField] private Transform _evacuationPoint;

        private Transform _vehicleTransform;
        private IReadOnlyList<BindPoint> _vehicleBindPoints;
        private LevelCompleteWindow _levelCompleteWindow;
        private IAudioPlayback _audioPlayback;
        private IUiStateMachine _uiStateMachine;
        private IParticleSystemFactory _particleSystemFactory;
        private IUiWindowFactory _uiWindowFactory;

        private void Construct()
        {
            _audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
            _uiStateMachine = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IUiStateMachine>();
            _particleSystemFactory = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IParticleSystemFactory>();
            _uiWindowFactory = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IUiWindowFactory>();
        }

        private void Awake()
        {
            Construct();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out VehicleCatchBehaviour vehicle))
            {
                if (vehicle.TryGetComponent(out Rigidbody vehicleRigidbody) == false)
                    throw new InvalidOperationException();

                vehicleRigidbody.velocity = Vector3.zero;
                vehicleRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                vehicleRigidbody.useGravity = false;

                _vehicleBindPoints = vehicle.GetEntitiesBindPoints();
                _vehicleTransform = vehicle.transform;

                if (vehicle.TryGetComponent(out VehicleSpeedLimit vehicleSpeedLimit) == false)
                    throw new InvalidOperationException();

                vehicleSpeedLimit.enabled = false;

                if (vehicle.TryGetComponent(out VehicleRotationLimit vehicleRotationLimit) == false)
                    throw new InvalidOperationException();

                vehicleRotationLimit.enabled = false;

                SoundID raySound = _audioPlayback.SoundsContainer.Ray;

                _audioPlayback.PlaySound(raySound);

                _ = StartCoroutine(MoveVehicleToCenter(_evacuationPoint.position, AttractionTime));
            }
        }

        private IEnumerator MoveVehicleToCenter(Vector3 target, float duration)
        {
            float time = 0;
            Vector3 startPosition = _vehicleTransform.position;

            while (time < duration)
            {
                _vehicleTransform.position = Vector3.Lerp(startPosition, target, time / duration);
                time += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }

            ShowLevelCompleteWindow();

            _ = StartCoroutine(EjectEntitiesByGravityRay());
        }

        private void ShowLevelCompleteWindow()
        {
            _uiStateMachine.SetState<UIStateLevelComplete>();

            GameObject uiRoot = _uiWindowFactory.GetUIRoot();

            GameObject window = _uiWindowFactory.GetLevelCompleteWindow(uiRoot);

            if (window.TryGetComponent(out LevelCompleteWindow levelCompleteWindow))
                _levelCompleteWindow = levelCompleteWindow;
        }

        private IEnumerator EjectEntitiesByGravityRay()
        {
            foreach (var bindPoint in _vehicleBindPoints)
            {
                if (bindPoint.IsFree == false)
                    yield return StartCoroutine(MoveEntityToGravityRay(bindPoint));
            }

            _levelCompleteWindow.EvaluatePassage();
        }

        private IEnumerator MoveEntityToGravityRay(BindPoint bindPoint)
        {
            Vector3 entityPosition = bindPoint.BindedEntity.transform.position;

            _particleSystemFactory.ShowGreenCollectEffect(entityPosition);

            _levelCompleteWindow.CollectPoint();

            bindPoint.BindedEntity.SwitchState<RisingByGravityRayState>();
            bindPoint.Exempt();

            yield return new WaitForSeconds(0.5f);
        }
    }
}