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

        private EventManager _eventManager;
        private EnemyAnimations _enemyAnimations;
        private PersonMover _personMover;

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
            StartCoroutine(IdleState());
        }

        private IEnumerator IdleState()
        {
            _personMover.Stop();
            yield return null;
            StartCoroutine(PatrolState());
        }

        private IEnumerator PatrolState()
        {
            _currentState = AIEnemyState.PATROL;
            while (_currentState == AIEnemyState.PATROL)
            {
                MoveRight();
                yield return _pausePatrol;
                MoveLeft();
                yield return _pausePatrol;
            }
        }

        private IEnumerator ChaseState()
        {
            yield return null;
        }

        private IEnumerator AttackState()
        {
            yield return null;
        }

        private void MoveRight()
        {
            _personMover.SetMoveDirection(Vector3.right);
        }

        private void MoveLeft()
        {
            _personMover.SetMoveDirection(Vector3.left);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMover>() != null)
                _currentState = AIEnemyState.CHASE;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerMover>() != null)
                _currentState = AIEnemyState.IDLE;
        }
    }
}