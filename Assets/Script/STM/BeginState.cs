﻿using Assets.Script.STM.Core;
using System.Collections;
using UnityEngine;

namespace Assets.Script.STM
{
    public class BeginState : State
    {
        public BeginState(StateMachine stateMachine) : base(stateMachine) => StateType = StateTypeEnum.BeginState;

        public override IEnumerator OnEnterState()
        {
            StateMachine.SetState(new PlayerTurnState(StateMachine));
            return base.OnEnterState();
        }
    }
}