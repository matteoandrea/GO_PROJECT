using Assets.Script.Manager;
using Assets.Script.STM.Core;
using System.Collections;
using UnityEngine;

namespace Assets.Script.STM
{
    public class EnemyTurnState : State
    {
        public EnemyTurnState(StateMachine stateMachine) : base(stateMachine) => StateType = StateTypeEnum.EnemyState;

        private GameManager manager;

        public override IEnumerator OnEnterState()
        {
            manager = StateMachine.GetComponent<GameManager>();

            if (manager.EnemyList.Count > 0)
            {
                manager.InvokeOnStartEnemyTurn();
                yield break;
            }
            else
            {
                StateMachine.SetState(new PlayerTurnState(StateMachine));
                yield break;
            }
        }
    }
}