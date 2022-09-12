using Assets.Script.Manager;
using Assets.Script.STM.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Script.STM
{
    public class ProcessState : State
    {
        public ProcessState(StateMachine stateMachine) : base(stateMachine) => StateType = StateTypeEnum.ProcessState;

        private GameManager _manager;

        public override IEnumerator OnEnterState()
        {
            _manager = StateMachine.GetComponent<GameManager>();

            switch (StateMachine.PreviousState.StateType)
            {
                case StateTypeEnum.BeginState:
                    StateMachine.SetState(new PlayerTurnState(StateMachine));
                    break;

                case StateTypeEnum.PlayerState:
                    _manager.ExecuteCommands();
                    yield return new WaitForSeconds(1.1f);
                    if (_manager.GameWon) StateMachine.SetState(new WinState(StateMachine));
                    else StateMachine.SetState(new EnemyTurnState(StateMachine));
                    break;

                case StateTypeEnum.EnemyState:
                    yield return new WaitUntil(() => _manager.enemyList.Count
                       == _manager.commandQueue.Count);

                    _manager.ExecuteCommands();
                    yield return new WaitForSeconds(1.1f);
                    StateMachine.SetState(new PlayerTurnState(StateMachine));
                    break;
            }
        }
    }
}
