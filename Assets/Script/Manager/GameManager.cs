using Assets.Script.Input;
using Assets.Script.STM;
using Assets.Script.STM.Core;
using Assets.Script.Commands;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Assets.Script.Pawns.Core;

namespace Assets.Script.Manager
{
    public class GameManager : StateMachine, IGameManager
    {
        [SerializeField] private InputReader _inputReader;
        public GameManagerProxy _proxy;

        public bool GameWon { get; set; }
        public bool GameLost { get; set; }
        public Queue<ICommand> commandQueue;

        public List<Pawn> enemyList;

        private void Awake()
        {
            commandQueue = new Queue<ICommand>();
            _proxy.GameManager = this;
        }

        private void Start()
        {
            SetState(new BeginState(this));
        }

        public void InvokeOnStartPlayerTurn() => _proxy.OnStartPlayerTurn();

        public void InvokeOnStartEnemyTurn() => _proxy.OnStartEnemyTurn();

        public void SetPlayerInput(bool e)
        {
            if (e) _inputReader.EnableGameplayInput();
            else _inputReader.DisableAll();
        }

        public void AddCommand(ICommand command)
        {
            commandQueue.Enqueue(command);

            if (State.StateType != StateTypeEnum.ProcessState)
                SetState(new ProcessState(this));
        }

        public void ExecutePlayerCommands() => commandQueue.Dequeue().Execute();

        public void ExecuteEnemyCommands()
        {
            foreach (var item in commandQueue)
            {
                item.Execute();
            }

            commandQueue.Clear();
        }

        public void AddEnemy(Pawn enemy) => enemyList.Add(enemy);

        public void RemoveEnemy(Pawn enemy) => enemyList.Remove(enemy);

    }
}