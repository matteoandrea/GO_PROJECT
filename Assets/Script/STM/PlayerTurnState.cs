using Assets.Script.Manager;
using Assets.Script.STM.Core;
using System.Collections;
using UnityEngine;

namespace Assets.Script.STM
{
    public class PlayerTurnState : State
    {
        public PlayerTurnState(StateMachine stateMachine) : base(stateMachine) { }

        private GameManager _manager;

        public override IEnumerator OnEnterState()
        {
            Debug.Log("player  State");
            _manager = StateMachine.GetComponent<GameManager>();
            _manager.inputReader.EnableGameplayInput();

            _manager.playerMovedEvent.OnEventRaised += ProcessMove;

            return base.OnEnterState();
        }

        private void ProcessMove()
        {
            _manager.playerMovedEvent.OnEventRaised -= ProcessMove;
            StateMachine.SetState(new EnemyTurnState(StateMachine));
        }

        public override IEnumerator OnExitState()
        {
            Debug.Log("Exit Player  State");
            _manager.inputReader.DisableAll();
            return base.OnExitState();
        }
    }
}