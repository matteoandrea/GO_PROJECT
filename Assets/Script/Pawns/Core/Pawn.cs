using Assets.Script.A;
using Assets.Script.A.NodeLogic;
using Assets.Script.Command;
using Assets.Script.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.Pawns.Core
{
    public abstract class Pawn : MonoBehaviour
    {
        [SerializeField] public PawnType PawnType { get; protected set; }
        public Node currentNode;

        [SerializeField] protected GameManagerProxy _gameManagerProxy;

        protected Animator _animator;
        protected Node _nodeToMove;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected abstract void CalculatePath();

        protected virtual void MoveAction()
        {
            ICommand command = new MoveCommand(_nodeToMove, transform, _animator);
            _gameManagerProxy.AddCommand(command);
        }
    }
}