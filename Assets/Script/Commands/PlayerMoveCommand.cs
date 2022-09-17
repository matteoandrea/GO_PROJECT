using DG.Tweening;
using UnityEngine;
using System.Linq;

namespace Assets.Script.Commands
{
    public class PlayerMoveCommand : MoveCommand
    {
        public PlayerMoveCommand(
            Vector3 targetPosition,
            float speedWalk,
            float speedRotation,
            Transform transform,
            Animator animator)
            : base(targetPosition, speedWalk,
                  speedRotation, transform, animator)
        { }


        public override void Execute()
        {
            _animator.SetBool("Walking", true);

            var sequence = DOTween.Sequence();
            sequence
                .Join(_transform.DOLookAt(_targetPosition, _speedRotation))
                .Join(_transform.DOMove(_targetPosition, _speedWalk))
                .OnComplete(() =>
                {
                    _animator.SetBool("Walking", false);
                });
        }
    }
}


