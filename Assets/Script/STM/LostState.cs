using Assets.Script.STM.Core;
using System.Collections;
using UnityEngine;

namespace Assets.Script.STM
{
    public class LostState : State
    {
        public LostState(StateMachine stateMachine) : base(stateMachine) => StateType = StateTypeEnum.LostState;

        public override IEnumerator OnEnterState()
        {
            return base.OnEnterState();
        }
    }
}