using Assets.Script.Manager;
using Assets.Script.Pawns;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.Commands
{
    public class PlayerMoveCommand : ICommand
    {
        private Transform _transform { get; set; }
        private Vector3 _targetPosition { get; set; }
        private Animator _animator { get; set; }
        private float _speedWalk { get; set; }
        private float _speedRotation { get; set; }

        public PlayerMoveCommand(Vector3 targetPosition, float speedWalk, float speedRotation, Transform transform, Animator animator)
        {
            _transform = transform;
            _targetPosition = targetPosition;
            _animator = animator;
            _speedWalk = speedWalk;
            _speedRotation = speedRotation;
        }

        public void Execute()
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


