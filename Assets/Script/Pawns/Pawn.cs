using Assets.Script.Command;
using Assets.Script.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.Pawns
{
    public abstract class Pawn : MonoBehaviour
    {
        [SerializeField] protected GameManagerProxy _gameManagerProxy;

        protected Camera _cam;
        protected NavMeshAgent _agent;

        protected Vector3 _targetPosition;

        protected virtual void Awake()
        {
            _cam = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
        }

        protected virtual void MoveAction()
        {
            ICommand command = new MoveCommand(_targetPosition, _agent, transform);
            _gameManagerProxy.AddCommand(command);
        }
    }
}