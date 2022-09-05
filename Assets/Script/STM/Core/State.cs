using System.Collections;
using UnityEngine;

namespace Assets.Script.STM.Core
{
    public abstract class State
    {
        protected StateMachine StateMachine;

        public State(StateMachine stateMachine) => StateMachine = stateMachine;

        public virtual IEnumerator OnEnterState() { yield break; }
        public virtual IEnumerator OnUpdateState() { yield break; }
        public virtual IEnumerator OnExitState() { yield break; }
    }
}