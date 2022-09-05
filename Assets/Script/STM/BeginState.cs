using Assets.Script.STM.Core;
using System.Collections;
using UnityEngine;

namespace Assets.Script.STM

{
    public class BeginState : State
    {
        public BeginState(StateMachine stateMachine) : base(stateMachine) { }

        public override IEnumerator OnEnterState()
        {
            Debug.Log("Begin State");
            StateMachine.SetState(new PlayerTurnState(StateMachine));
            return base.OnEnterState();
        }

        public override IEnumerator OnExitState()
        {
            Debug.Log("Exit  Begin");
            return base.OnExitState();
        }
    }
}