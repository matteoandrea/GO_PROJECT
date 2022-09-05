using Assets.Script.Events;
using Assets.Script.Input;
using Assets.Script.STM;
using Assets.Script.STM.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Manager
{
    public class GameManager : StateMachine
    {
        public InputReader inputReader;
        public VoidEventSO playerMovedEvent;

        private void Start()
        {
            SetState(new BeginState(this));
        }
    }
}