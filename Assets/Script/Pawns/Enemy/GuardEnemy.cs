using Assets.Script.Commands;
using Assets.Script.Nodes.Core;
using System.Collections;
using UnityEngine;

namespace Assets.Script.Pawns.Enemy
{
    public class GuardEnemy : EnemyBase
    {
        [SerializeField]
        private LayerMask _playerLayer;

        protected override void OnStartTurn()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity,
                                 _playerLayer))
            {
                MoveAction(hit.transform.position);
            }
            else
            {
              // ICommand command = new RotateCommand(transform, _rotationSpeed);
              //  _gameManagerProxy.AddCommand(command);
            }
        }
    }
}