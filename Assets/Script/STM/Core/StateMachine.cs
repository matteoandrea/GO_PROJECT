using System.Collections;
using UnityEngine;

namespace Assets.Script.STM.Core
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State State;
        public State PreviousState { get; protected set; }

        public void SetState(State state)
        {
            if (State != null) 
                StartCoroutine(State?.OnExitState());

            PreviousState = State;
            State = state;

            if (State != null) 
                StartCoroutine(State?.OnEnterState());
        }
    }
}