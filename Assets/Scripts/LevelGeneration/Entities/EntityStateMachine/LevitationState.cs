using System.Collections;
using UnityEngine;

namespace LevelGeneration.Entities.EntityStateMachine
{
    public class LevitationState : EntityBaseState
    {
        private Rigidbody _rigidbody;
        private IEntityStateSwitcher _stateSwitcher;
        private EntityBehaviour _entity;
        private Coroutine _mainCoroutine;
        private Coroutine _movingCoroutine;
        private float _levitationHeight;
        private float _levitationHalfCycleTime;

        public LevitationState(IEntityStateSwitcher stateSwitcher, EntityBehaviour entity, Rigidbody rigidbody,
            float levitationHeight, float levitationHalfCycleTime) : base(stateSwitcher)
        {
            _rigidbody = rigidbody;
            _stateSwitcher = stateSwitcher;
            _entity = entity;
            _levitationHeight = levitationHeight;
            _levitationHalfCycleTime = levitationHalfCycleTime;
        }

        public override void Start()
        {
            _rigidbody.isKinematic = true;

            Move();
        }

        public override void Move()
        {
            _mainCoroutine = _entity.StartCoroutine(Levitate());
        }

        public override void ReactOnEntryVehicleCatchZone()
        {
            _stateSwitcher.SwitchState<KnockedState>();
        }

        public override void ReactOnEntryVehicleTossZone()
        {
            _stateSwitcher.SwitchState<KnockedState>();
        }

        public override void Stop()
        {
            _entity.StopCoroutine(_movingCoroutine);
            _entity.StopCoroutine(_mainCoroutine);
        }

        private IEnumerator Levitate()
        {
            Vector3 position = _entity.transform.position;
            Vector3 endPoint;
            bool isOnHighestPoint = false;

            float delayBeforeLevitate = Random.Range(0, _levitationHalfCycleTime);

            yield return new WaitForSeconds(delayBeforeLevitate);

            while (true)
            {
                if (isOnHighestPoint)
                {
                    endPoint = new Vector3(position.x, position.y, position.z);
                    isOnHighestPoint = false;
                }
                else
                {
                    endPoint = new Vector3(position.x, position.y + _levitationHeight, position.z);
                    isOnHighestPoint = true;
                }

                _movingCoroutine = _entity.StartCoroutine(Moving(endPoint));

                yield return _movingCoroutine;
            }
        }

        private IEnumerator Moving(Vector3 endPoint)
        {
            float startTime = Time.time;
            float time;
            Vector3 position = _entity.transform.position;
            Vector3 startPosition = new Vector3(position.x, position.y, position.z);


            while (Time.time - startTime < _levitationHalfCycleTime)
            {
                time = (Time.time - startTime) / _levitationHalfCycleTime;
                _entity.transform.position = Vector3.Lerp(startPosition, endPoint, time);
                yield return null;
            }
        }
    }
}
