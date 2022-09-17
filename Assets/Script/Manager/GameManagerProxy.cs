using Assets.Script.Commands;
using Assets.Script.Pawns.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.Manager
{
    [CreateAssetMenu(fileName = "GameManagerProxy", menuName = "Managers/Game Manager Proxy")]
    public class GameManagerProxy : ScriptableObject, IGameManager
    {


        public GameManager GameManager { private get; set; }
        public bool GameWon { get => GameManager.GameWon; set => GameManager.GameWon = value; }
        public bool GameLost { get => GameManager.GameLost; set => GameManager.GameLost = value; }

        public event UnityAction startPlayerTurnEvent;
        public event UnityAction startEnemyTurnEvent;

        public void AddCommand(ICommand command) => GameManager.AddCommand(command);


        public void OnStartPlayerTurn() => startPlayerTurnEvent?.Invoke();
        public void OnStartEnemyTurn() => startEnemyTurnEvent?.Invoke();

        public void AddEnemy(Pawn enemy) => GameManager.AddEnemy(enemy);
        public void RemoveEnemy(Pawn enemy) => GameManager.RemoveEnemy(enemy);
    }
}
