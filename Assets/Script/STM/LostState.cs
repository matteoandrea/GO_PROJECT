using Assets.Script.STM.Core;

namespace Assets.Script.STM
{
    public class LostState : State
    {
        public LostState(StateMachine stateMachine) : base(stateMachine) => StateType = StateTypeEnum.LoseState;
    }
}