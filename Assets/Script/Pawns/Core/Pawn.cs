using Assets.Script.Command;
using Assets.Script.Manager;
using Assets.Script.Nodes.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.Pawns.Core
{
    public abstract class Pawn : MonoBehaviour
    {
        [SerializeField] protected GameManagerProxy _gameManagerProxy;

        [SerializeField]protected NodeCore _currentNode;
        protected Animator _animator;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected abstract void OnEnterNode(NodeCore node);
        protected abstract void OnExitNode(NodeCore node);
        protected abstract void OnStartTurn();

        public virtual void Die() => _animator.SetTrigger("Die");
        public void Attack() => _animator.SetTrigger("Attack");

        private void OnTriggerEnter(Collider hit)
        {
            if (!hit.CompareTag("Node")) return;
            var node = hit.GetComponent<NodeCore>();
            _currentNode = node;
            OnEnterNode(node);
        }

        private void OnTriggerExit(Collider hit)
        {
            if (!hit.CompareTag("Node")) return;
            var node = hit.GetComponent<NodeCore>(); ;
            OnExitNode(node);
            _currentNode = null;
        }
    }
}