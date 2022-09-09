using Assets.Script.Input;
using Assets.Script.STM;
using Assets.Script.STM.Core;
using Assets.Script.Command;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Manager
{
    public class GameManager : StateMachine, IGameManager
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private GameManagerProxy _proxy;

        public Queue<ICommand> commandQueue;

        private void Awake()
        {
            commandQueue = new Queue<ICommand>();
            _proxy.GameManager = this;
        }

        private void Start()
        {
            SetState(new BeginState(this));
        }

        public void SetPlayerInput(bool e)
        {
            if (e) _inputReader.EnableGameplayInput();
            else _inputReader.DisableAll();
        }

        public void AddCommand(ICommand command)
        {
            commandQueue.Enqueue(command);
            ExecuteCommands();
        }

        private void ExecuteCommands()
        {
            if (State.StateType == StateTypeEnum.PlayerState)
            {
                commandQueue.Dequeue().Execute();
                SetState(new EnemyTurnState(this));
            }
            else
            {
                for (int i = 0; i < commandQueue.Count; i++)
                {
                    commandQueue.Dequeue().Execute();
                }

                SetState(new PlayerTurnState(this));
            }
        }
    }
}