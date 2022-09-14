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
            Debug.Log($"Entrou{StateType}");

            _manager = StateMachine.GetComponent<GameManager>();

            switch (StateMachine.PreviousState.StateType)
            {
                case StateTypeEnum.BeginState:

                    StateMachine.SetState(new PlayerTurnState(StateMachine));
                    break;

                case StateTypeEnum.PlayerState:

                    _manager.ExecutePlayerCommands();
                    yield return new WaitForSeconds(1.1f);

                    if (_manager.GameWon) StateMachine.SetState(new WinState(StateMachine));
                    else StateMachine.SetState(new EnemyTurnState(StateMachine));

                    break;

                case StateTypeEnum.EnemyState:
                    Debug.Log($"Command {_manager.commandQueue.Count}");
                    Debug.Log($"Enemy List{_manager.enemyList.Count}");
                    yield return new WaitUntil(() =>
                    _manager.enemyList.Count == _manager.commandQueue.Count);

                    _manager.ExecuteEnemyCommands();
                    yield return new WaitForSeconds(1.1f);
                    StateMachine.SetState(new PlayerTurnState(StateMachine));
                    break;
            }
        }
    }
}
