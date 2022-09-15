using Assets.Script.Manager;
using Assets.Script.Pawns;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.Command
{
    public class PlayerMoveCommand : ICommand
    {
        private Transform transform;
        private Vector3 targetPosition;
        private Animator animator;

        private float _speed = .9f;

        public PlayerMoveCommand(Vector3 targetPosition, Transform transform, Animator animator)
        {
            this.transform = transform;
            this.targetPosition = targetPosition;
            this.animator = animator;
        }

        public void Execute()
        {
            animator.SetBool("Walking", true);

            var sequence = DOTween.Sequence();
            sequence
                .Join(transform.DOLookAt(targetPosition, .5f))
                .Join(transform.DOMove(targetPosition, _speed))
                .OnComplete(() =>
                {
                    animator.SetBool("Walking", false);
                });
        }
    }
}


