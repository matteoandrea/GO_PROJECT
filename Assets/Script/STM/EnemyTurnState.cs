using Assets.Script.Manager;
using Assets.Script.STM.Core;
using System.Collections;

namespace Assets.Script.STM
{
    public class EnemyTurnState : State
    {
        public EnemyTurnState(StateMachine stateMachine) : base(stateMachine) => StateType = StateTypeEnum.EnemyState;

        private GameManager _manager;

        public override IEnumerator OnEnterState()
        {
            _manager = StateMachine.GetComponent<GameManager>();

            if (_manager.enemyList.Count <= 0)
            {
                StateMachine.SetState(new PlayerTurnState(StateMachine));
                return base.OnEnterState();
            }

            //do something....
            return base.OnEnterState();
        }


    }
}