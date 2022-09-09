using Assets.Script.STM.Core;
using System.Collections;
using UnityEngine;

namespace Assets.Script.STM
{
    public class WinState : State
    {
        public WinState(StateMachine stateMachine) : base(stateMachine) => StateType = StateTypeEnum.WinState;

        public override IEnumerator OnEnterState()
        {
            Debug.Log("Win State");
            return base.OnEnterState();
        }
    }
}