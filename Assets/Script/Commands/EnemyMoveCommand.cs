using Assets.Script.Interactions;
using Assets.Script.Nodes.Core;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Script.Commands
{
    public class EnemyMoveCommand : MoveCommand
    {
        private LayerMask _incluedlayerMask;

        public EnemyMoveCommand(
            Vector3 targetPosition,
            float speedWalk,
            float speedRotation,
            Transform transform,
            Animator animator,
            LayerMask layerMask)
            : base(targetPosition,
                   speedWalk,
                   speedRotation,
                   transform,
                   animator) => _incluedlayerMask = layerMask;

        public override void Execute()
        {
            _animator.SetBool("Walking", true);

            var sequence = DOTween.Sequence();
            sequence
                .Join(_transform.DOLookAt(_targetPosition, _speedRotation))
                .Join(_transform.DOMove(_targetPosition, _speedWalk))
                .OnComplete(CompleteAnimaton);
        }

        private void CompleteAnimaton()
        {
            _animator.SetBool("Walking", false);

            if (!Physics.Raycast(_transform.position, _transform.forward, out RaycastHit hit, Mathf.Infinity, _incluedlayerMask))
            {
                ICommand command = new RotateCommand(_transform, _speedRotation);
                command.Execute();
            }
            else
            {
                var script = hit.collider.GetComponent<BaseNode>();
                if (script == null) return;

               // ICommand command = new RotateCommand(_transform, _speedRotation);
                //command.Execute();
            }
        }
    }
}