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
            if (_manager.GameLost)
            {
                StateMachine.SetState(new WinState(StateMachine));
                return base.OnEnterState();
            }
            _manager = StateMachine.GetComponent<GameManager>();
            _manager.InvokeOnStartPlayerTurn();
            _manager.SetPlayerInput(true);

            return base.OnEnterState();
        }

        public override IEnumerator OnExitState()
        {
            _manager.SetPlayerInput(false);
            return base.OnExitState();
        }
    }
}