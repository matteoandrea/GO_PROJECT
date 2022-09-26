using System.Collections;
using UnityEngine;

namespace Assets.Script.STM.Core
{
    public abstract class State
    {
        protected StateMachine StateMachine;
        public StateTypeEnum StateType;

        public State(StateMachine stateMachine) => StateMachine = stateMachine;

        public virtual IEnumerator OnEnterState() { Debug.Log($"Enter State: {StateType}"); yield break; }
        public virtual IEnumerator OnUpdateState() { yield break; }
        public virtual IEnumerator OnExitState() { Debug.Log($"Exit State: {StateType}");  yield break; }
    }
}