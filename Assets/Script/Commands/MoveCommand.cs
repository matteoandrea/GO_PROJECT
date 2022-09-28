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
        protected Transform transform { get; set; }
        protected Vector3 targetPosition { get; set; }
        protected Animator animator { get; set; }
        protected float speedWalk { get; set; }
        protected float speedRotation { get; set; }

        public MoveCommand(Vector3 targetPosition, float speedWalk, float speedRotation, Transform transform, Animator animator)
        {
            this.targetPosition = targetPosition;
            this.speedWalk = speedWalk;
            this.speedRotation = speedRotation;
            this.transform = transform;
            this.animator = animator;
        }

        public async UniTaskVoid Execute()
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

            await UniTask.Delay(TimeSpan.FromSeconds(sequence.Duration()));
            //yield return new WaitForSeconds(sequence.Duration());
        }
    }
}
