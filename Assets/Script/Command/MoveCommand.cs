using Assets.Script.A.NodeLogic;
using Assets.Script.Manager;
using Assets.Script.Pawns;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.Command
{
    public class MoveCommand : ICommand
    {
        Transform transform;
        Node node;
        Animator animator;

        private float _speed = 1;

        public MoveCommand(Node node, Transform transform, Animator animator)
        {
            this.transform = transform;
            this.node = node;
            this.animator = animator;
        }

        public void Execute()
        {
            animator.SetBool("Walking", true);

            var sequence = DOTween.Sequence();
            sequence
                .Join(transform.DOLookAt(node.transform.position, .5f))
                .Join(transform.DOMove(node.transform.position, _speed))
                .OnComplete(() =>
                {
                    animator.SetBool("Walking", false);
                });
        }
    }
}


