using Assets.Script.Manager;
using Assets.Script.Nodes.Core;
using UnityEngine;

namespace Assets.Script.Pawns.Core
{
    public abstract class Pawn : MonoBehaviour
    {
        [SerializeField]
        protected float _walkSpeed, _rotationSpeed;
        [SerializeField]
        protected GameManagerProxy _gameManagerProxy;
        [Space(10)]
 

        [SerializeField]
        protected BaseNode _currentNode;
        protected Animator _animator { get; set; }

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected abstract void OnEnterNode(BaseNode node);
        protected abstract void OnExitNode(BaseNode node);
        protected abstract void OnStartTurn();

        public virtual void Die() => _animator.SetTrigger("Die");
        public void Attack() => _animator.SetTrigger("Attack");

        private void OnTriggerEnter(Collider hit)
        {
            if (!hit.CompareTag("Node")) return;
            var node = hit.GetComponent<BaseNode>();
            _currentNode = node;
            OnEnterNode(node);
        }

        private void OnTriggerExit(Collider hit)
        {
            if (!hit.CompareTag("Node")) return;
            var node = hit.GetComponent<BaseNode>(); ;
            OnExitNode(node);
            _currentNode = null;
        }
    }
}