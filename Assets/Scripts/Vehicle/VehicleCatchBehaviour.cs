using System;
using System.Collections.Generic;
using Ami.BroAudio;
using Infrastructure.Services.AudioPlayback;
using Infrastructure.Services.Factories.ParticleSystemFactory;
using LevelGeneration.Entities.EntityStateMachine;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vehicle.BindPoints;

namespace Vehicle
{
    public class VehicleCatchBehaviour : MonoBehaviour
    {
        [SerializeField] private List<LeftSideBindPoint> _leftSideBindPoints;
        [SerializeField] private List<RightSideBindPoint> _rightSideBindPoints;
        [SerializeField] private List<InsideBindPoint> _insideBindPoints;

        private List<BindPoint> _allBindPoints = new List<BindPoint>();
        private IParticleSystemFactory _particleSystemFactory;
        private IAudioPlayback _audioPlayback;
        
        
        private void Construct()
        {
            _particleSystemFactory = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IParticleSystemFactory>();
            _audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
        }

        private void Awake()
        {
            Construct();
        }

        private void Start()
        {
            PutAllPointsInOneList();
        }

        public bool TryFillInsideBindPoint(EntityBehaviour entity)
        {
            BindPoint previousBindPoint = entity.GetComponentInParent<BindPoint>();

            if (previousBindPoint is null)
                throw new InvalidOperationException();

            if (TryFillBindPoint(entity, _insideBindPoints))
            {
                previousBindPoint.Exempt();

                return true;
            }
            
            return false;
        }

        public bool TryFillLeftSideBindPoint(EntityBehaviour entity)
        {
            return TryFillBindPoint(entity, _leftSideBindPoints);
        }

        public bool TryFillRightSideBindPoint(EntityBehaviour entity)
        {
            return TryFillBindPoint(entity, _rightSideBindPoints);
        }

        public IReadOnlyList<BindPoint> GetEntitiesBindPoints()
        {
            return _allBindPoints;
        }

        private bool TryFillBindPoint(EntityBehaviour entity, IReadOnlyList<BindPoint> bindPoints)
        {
            foreach (var bindPoint in bindPoints)
            {
                if (bindPoint.IsFree)
                {
                    _particleSystemFactory.ShowCollectEffect(entity.transform.position);

                    bindPoint.Fill(entity);
                    
                    PlayAstronautPickUpSound();

                    return true;
                }
            }

            return false;
        }

        private void PutAllPointsInOneList()
        {
            foreach (var bindPoint in _leftSideBindPoints)
            {
                _allBindPoints.Add(bindPoint);
            }

            foreach (var bindPoint in _rightSideBindPoints)
            {
                _allBindPoints.Add(bindPoint);
            }

            foreach (var bindPoint in _insideBindPoints)
            {
                _allBindPoints.Add(bindPoint);
            }
        }
        
        private void PlayAstronautPickUpSound()
        {
            SoundID astronautPickUp = _audioPlayback.SoundsContainer.AstronautPickUp;

            _audioPlayback.PlaySound(astronautPickUp);
        }
    }
}
