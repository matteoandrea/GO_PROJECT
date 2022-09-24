using Assets.Script.Commands;
using Assets.Script.Input;
using Assets.Script.Manager;
using Assets.Script.Nodes.Core;
using Assets.Script.Pawns.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Pawns.Enemy
{
    public abstract class EnemyBase : Pawn
    {
        [SerializeField]
        protected LayerMask _incluedlayerMask;

        protected virtual void OnEnable()
            => _gameManagerProxy.startEnemyTurnEvent += OnStartTurn;

        protected virtual void OnDisable()
            => _gameManagerProxy.startEnemyTurnEvent -= OnStartTurn;

        private void Start() => _gameManagerProxy.AddEnemy(this);

        public override void MoveAction(Vector3 targetPosition)
        {
            ICommand command = new EnemyMoveCommand(
                targetPosition,
                _walkSpeed,
                _rotationSpeed,
                transform,
                _animator,
                _incluedlayerMask);
            _gameManagerProxy.AddCommand(command);
        }

        protected override void OnEnterNode(BaseNode node)
            => node.AddEnemy(this);

        protected override void OnExitNode(BaseNode node)
            => node.RemoveEnemy(this);

        public override void Die()
        {
            base.Die();
            _gameManagerProxy.RemoveEnemy(this);
        }
    }
}
