using Assets.Script.STM.Core;
using System.Collections;
using UnityEngine;

namespace Assets.Script.STM
{
    public class LostState : State
    {
        public LostState(StateMachine stateMachine) : base(stateMachine) => StateType = StateTypeEnum.LoseState;

        public override IEnumerator OnEnterState()
        {
            Debug.Log("Win Lost");
            return base.OnEnterState();
        }
    }
}