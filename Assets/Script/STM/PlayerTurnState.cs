using Assets.Script.Manager;
using Assets.Script.STM.Core;
using System.Collections;
using UnityEngine;

namespace Assets.Script.STM
{
    public class PlayerTurnState : State
    {
        public PlayerTurnState(StateMachine stateMachine) : base(stateMachine) => StateType = StateTypeEnum.PlayerState;

        private GameManager manager;

        public override IEnumerator OnEnterState()
        {
            manager = StateMachine.GetComponent<GameManager>();

            manager.InvokeOnStartPlayerTurn();
            manager.SetPlayerInput(true);

            return base.OnEnterState();
        }

        public override IEnumerator OnExitState()
        {
            manager.SetPlayerInput(false);
            return base.OnExitState();
        }
    }
}