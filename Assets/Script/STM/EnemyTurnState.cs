using Assets.Script.Manager;
using Assets.Script.STM.Core;
using System.Collections;
using UnityEngine;

namespace Assets.Script.STM
{
    public class EnemyTurnState : State
    {
        public EnemyTurnState(StateMachine stateMachine) : base(stateMachine) => StateType = StateTypeEnum.EnemyState;

        private GameManager _manager;

        public override IEnumerator OnEnterState()
        {
            Debug.Log($"Entrou{StateType}");


            _manager = StateMachine.GetComponent<GameManager>();

            if (_manager.enemyList.Count <= 0)
            {
                StateMachine.SetState(new PlayerTurnState(StateMachine));
                yield break;
            }

            //do something....
            yield break;
        }


    }
}