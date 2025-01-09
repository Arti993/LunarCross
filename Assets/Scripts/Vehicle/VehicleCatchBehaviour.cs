using System;
using System.Collections.Generic;
using Ami.BroAudio;
using Infrastructure;
using Infrastructure.Services.AudioPlayback;
using Infrastructure.Services.Factories.ParticleSystemFactory;
using LevelGeneration.Entities.EntityStateMachine;
using Reflex.Attributes;
using UnityEngine;
using Vehicle.BindPoints;

namespace Vehicle
{
    public class VehicleCatchBehaviour : MonoBehaviour
    {
        [SerializeField] private List<LeftSideBindPoint> _leftSideBindPoints;
        [SerializeField] private List<RightSideBindPoint> _rightSideBindPoints;
        [SerializeField] private List<InsideBindPoint> _insideBindPoints;

        private List<BindPoint> _allBindPoints = new List<BindPoint>();
        private IAudioPlayback _audioPlayback;
        private IParticleSystemFactory _particleSystemFactory;
        
        [Inject]
        private void Construct(IAudioPlayback audioPlayback, IParticleSystemFactory particleSystemFactory)
        {
            _audioPlayback = audioPlayback;
            _particleSystemFactory = particleSystemFactory;
        }

        private void Start()
        {
            PutAllPointsInOneList();
        }

        public bool TryFillInsideBindPoint(EntityBehaviour entity)
        {
            bool isFillingSuccess = false;

            BindPoint previousBindPoint = entity.GetComponentInParent<BindPoint>();

            if (previousBindPoint is null)
                throw new InvalidOperationException();

            if (TryFillBindPoint(entity, _insideBindPoints))
            {
                isFillingSuccess = true;

                PlayAstronautPickUpSound();

                previousBindPoint.Exempt();
            }

            return isFillingSuccess;
        }

        public bool TryFillLeftSideBindPoint(EntityBehaviour entity)
        {
            if (TryFillBindPoint(entity, _leftSideBindPoints))
            {
                PlayAstronautPickUpSound();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryFillRightSideBindPoint(EntityBehaviour entity)
        {
            if (TryFillBindPoint(entity, _rightSideBindPoints))
            {
                PlayAstronautPickUpSound();

                return true;
            }
            else
            {
                return false;
            }
        }

        public IReadOnlyList<BindPoint> GetEntitiesBindPoints()
        {
            return _allBindPoints;
        }

        private bool TryFillBindPoint(EntityBehaviour entity, IReadOnlyList<BindPoint> bindPoints)
        {
            bool isFillingSuccess = false;

            for (int i = 0; i < bindPoints.Count; i++)
            {
                if (bindPoints[i].IsFree)
                {
                    _particleSystemFactory.ShowCollectEffect(entity.transform.position);

                    bindPoints[i].Fill(entity);
                    isFillingSuccess = true;
                    break;
                }
            }

            return isFillingSuccess;
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
