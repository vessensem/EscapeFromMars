using EscapeFromMars.Animations;
using EscapeFromMars.Controls;
using EscapeFromMars.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EscapeFromMars.AI
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private float _timePatrol = 2f;//Time for patrol to one point
        [SerializeField]
        private float _chaseMaxDistance = 4f;//Max distance to target for chase state
        [SerializeField]
        private float _attackRange = 0.5f;
        [SerializeField]
        private float _attackRate = 1f;

        private float _attackRateTimer;
        private EventManager _eventManager;
        private EnemyAnimations _enemyAnimations;
        private PersonMover _personMover;
        private Transform _target;

        private AIEnemyState _currentState = AIEnemyState.IDLE;

        private WaitForSeconds _pausePatrol;

        [Inject]
        private void Constructor(EventManager eventManager, EnemyAnimations enemyAnimations, PersonMover enemyMover)
        {
            _eventManager = eventManager;
            _enemyAnimations = enemyAnimations;
            _personMover = enemyMover;
        }

        private void Awake()
        {
            _pausePatrol = new WaitForSeconds(_timePatrol);
        }

        private void Start()
        {
            SetState(AIEnemyState.IDLE);
        }

        private IEnumerator IdleState()
        {
            _personMover.Stop();
            yield return null;
            SetState(AIEnemyState.PATROL);
        }

        private IEnumerator PatrolState()
        {
            _currentState = AIEnemyState.PATROL;
            while (_currentState == AIEnemyState.PATROL)
            {
                WalkRight();
                yield return _pausePatrol;
                _personMover.Stop();
                yield return _pausePatrol;
                WalkLeft();
                yield return _pausePatrol;
                _personMover.Stop();
                yield return _pausePatrol;
            }
        }

        private IEnumerator ChaseState()
        {
            _currentState = AIEnemyState.CHASE;
            while (_currentState == AIEnemyState.CHASE)
            {
                if (DistanceToTarget() < _chaseMaxDistance)
                    RunToTarget();
                else
                    SetState(AIEnemyState.IDLE);

                if (DistanceToTarget() <= _attackRange)
                    SetState(AIEnemyState.ATTACK);

                yield return null;
            }
        }

        private IEnumerator AttackState()
        {
            _currentState = AIEnemyState.ATTACK;
            _attackRateTimer = _attackRate;
            _personMover.Stop();

            while (_currentState == AIEnemyState.ATTACK)
            {
                if (DistanceToTarget() > _attackRange)
                    SetState(AIEnemyState.IDLE);

                _attackRateTimer += Time.deltaTime;
                if (_attackRateTimer >= _attackRate)
                {
                    _enemyAnimations.Attack();
                    _attackRateTimer = 0;
                }
                yield return null;
            }
        }

        private float DistanceToTarget()
        {
            return Vector2.Distance(_target.position, transform.position);
        }

        private void RunToTarget()
        {
            _personMover.Run((_target.position - transform.position).normalized);
        }

        private void WalkRight()
        {
            _personMover.Walk(Vector3.right);
        }

        private void WalkLeft()
        {
            _personMover.Walk(Vector3.left);
        }

        private void SetState(AIEnemyState aiEnemyState)
        {
            StopAllCoroutines();
            switch (aiEnemyState)
            {
                case AIEnemyState.IDLE:
                    StartCoroutine(IdleState());
                    break;
                case AIEnemyState.PATROL:
                    StartCoroutine(PatrolState());
                    break;
                case AIEnemyState.CHASE:
                    StartCoroutine(ChaseState());
                    break;
                case AIEnemyState.ATTACK:
                    StartCoroutine(AttackState());
                    break;
                default:
                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMover>() != null)
            {
                _target = other.transform;
                SetState(AIEnemyState.CHASE);
            }
        }

    }
}