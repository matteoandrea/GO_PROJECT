using Assets.Script.Commands.Core;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Script.Commands
{
    public class MoveCommand : ICommand
    {
        private Transform transform { get; set; }
        private Vector3 targetPosition { get; set; }
        private Animator animator { get; set; }
        private float speedWalk { get; set; }
        private float speedRotation { get; set; }

        public MoveCommand(Vector3 targetPosition, float speedWalk, float speedRotation, Transform transform, Animator animator)
        {
            this.targetPosition = targetPosition;
            this.speedWalk = speedWalk;
            this.speedRotation = speedRotation;
            this.transform = transform;
            this.animator = animator;
        }

        public IEnumerator Execute()
        {
            animator.SetBool("Walking", true);

            var sequence = DOTween.Sequence();
            sequence
                .Join(transform.DOLookAt(targetPosition, speedRotation))
                .Join(transform.DOMove(targetPosition, speedWalk))
                .OnComplete(() =>
                {
                    animator.SetBool("Walking", false);
                });

            yield return new WaitForSeconds(sequence.Duration());
        }
    }
}
