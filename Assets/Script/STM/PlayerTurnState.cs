using Assets.Script.Manager;
using Assets.Script.STM.Core;
using System.Collections;
using UnityEngine;

namespace Assets.Script.STM
{
    public class PlayerTurnState : State
    {
        public PlayerTurnState(StateMachine stateMachine) : base(stateMachine) => StateType = StateTypeEnum.PlayerState;

        private GameManager _manager;

        public override IEnumerator OnEnterState()
        {
            _manager = StateMachine.GetComponent<GameManager>();
            _manager.SetPlayerInput(true);

            //_manager.proxy.playerActionEvent += ProcessMove;

            return base.OnEnterState();
        }

        private void ProcessMove()
        {
            //_manager.proxy.playerActionEvent -= ProcessMove;
            StateMachine.SetState(new EnemyTurnState(StateMachine));
        }

        public override IEnumerator OnExitState()
        {
            _manager.SetPlayerInput(false);
            return base.OnExitState();
        }
    }
}