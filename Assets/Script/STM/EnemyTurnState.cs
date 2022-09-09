using Assets.Script.STM.Core;

namespace Assets.Script.STM
{
    public class EnemyTurnState : State
    {
        public EnemyTurnState(StateMachine stateMachine) : base(stateMachine) => StateType = StateTypeEnum.EnemyState;

    }
}