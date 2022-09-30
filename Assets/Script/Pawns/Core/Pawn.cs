using Assets.Script.Commands;
using Assets.Script.Commands.Core;
using Assets.Script.Manager;
using Assets.Script.Nodes.Core;
using System.Collections;
using UnityEngine;

namespace Assets.Script.Pawns.Core
{
    public abstract class Pawn : MonoBehaviour
    {
        protected CommandPlayList commandPlayList = new();

        [SerializeField]
        protected float walkSpeed = .9f, rotationSpeed = .2f;

        [SerializeField]
        protected GameManagerProxy gameManagerProxy;

        [Space(10)]

        protected BaseNode currentNode;
        protected Animator animator { get; set; }

        private Collider Collider;

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            Collider = GetComponent<Collider>();
        }

        protected abstract void OnEnterNode(BaseNode node);
        protected abstract void OnExitNode(BaseNode node);
        protected abstract void OnStartTurn();
        protected void MoveAction(Vector3 targetPosition)
        {
            ICommand command = new MoveCommand(
           targetPosition,
           walkSpeed,
           rotationSpeed,
           transform,
           animator);

            commandPlayList.AddCommand(command);
        }

        public virtual IEnumerator Die()
        {
            animator.SetTrigger("Die");
            Collider.enabled = false;
            yield return new WaitForSeconds(1.1f);
        }

        public virtual void Attack() => animator.SetTrigger("Attack");

        private void OnTriggerEnter(Collider hit)
        {
            if (!hit.CompareTag("Node")) return;
            var node = hit.GetComponent<BaseNode>();
            currentNode = node;
            OnEnterNode(node);
        }

        private void OnTriggerExit(Collider hit)
        {
            if (!hit.CompareTag("Node")) return;
            var node = hit.GetComponent<BaseNode>(); ;
            OnExitNode(node);
            currentNode = null;
        }
    }
}