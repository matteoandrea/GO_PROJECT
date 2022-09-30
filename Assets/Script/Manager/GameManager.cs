using Assets.Script.Input;
using Assets.Script.STM;
using Assets.Script.STM.Core;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Pawns.Core;
using Assets.Script.Nodes.Core;
using System.Collections;
using Assets.Script.Commands.Core;

namespace Assets.Script.Manager
{
    public class GameManager : StateMachine, IGameManager
    {
        [SerializeField]
        private InputReader inputReader;

        [SerializeField]
        private NodeManager nodeManager;

        public GameManagerProxy ProxyManager;

        private GameStatus gameStatus = GameStatus.Running;

        public Queue<CommandPlayList> CommandQueue = new();
        public List<Pawn> EnemyList;

        private void Start()
        {
            ProxyManager.GameManager = this;
            StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            yield return nodeManager.Inicialization();
            SetState(new BeginState(this));
        }

        public void InvokeOnStartPlayerTurn() => ProxyManager.OnStartPlayerTurn();

        public void InvokeOnStartEnemyTurn() => ProxyManager.OnStartEnemyTurn();

        public void SetPlayerInput(bool e)
        {
            if (e)
                inputReader.EnableGameplayInput();
            else
                inputReader.DisableAll();
        }

        public void AddCommandPlaylist(CommandPlayList commands)
        {
            CommandQueue.Enqueue(commands);

            switch (State.StateType)
            {
                case StateTypeEnum.PlayerState:
                    StartCoroutine(ExecutePlayerCommands());
                    break;

                case StateTypeEnum.EnemyState:
                    if (EnemyList.Count == CommandQueue.Count)
                        StartCoroutine(ExecuteEnemyCommands());
                    break;
            }
        }

        private IEnumerator ExecutePlayerCommands()
        {
            yield return CommandQueue.Dequeue().Execute();

            switch (gameStatus)
            {
                case GameStatus.Running:
                    SetState(new EnemyTurnState(this));
                    break;
                case GameStatus.Won:
                    SetState(new WinState(this));
                    break;
            }
        }

        private IEnumerator ExecuteEnemyCommands()
        {
            foreach (var item in CommandQueue)
            {
                StartCoroutine(item.Execute());
            }

            CommandQueue.Clear();

            yield return new WaitForSeconds(1.2f);

            switch (gameStatus)
            {
                case GameStatus.Running:
                    SetState(new PlayerTurnState(this));
                    break;
                case GameStatus.Lost:
                    SetState(new LostState(this));
                    break;
            }
        }

        public void AddEnemy(Pawn enemy) => EnemyList.Add(enemy);

        public void RemoveEnemy(Pawn enemy) => EnemyList.Remove(enemy);

        public void SetGameStatus(GameStatus gameStatus) => this.gameStatus = gameStatus;
    }
}