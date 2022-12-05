using Assets.Script.Commands.Core;
using Assets.Script.Pawns.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.Manager
{
    [CreateAssetMenu(fileName = "GameManagerProxy", menuName = "Managers/Game Manager Proxy")]
    public class GameManagerProxy : ScriptableObject, IGameManager
    {
        public GameManager GameManager { private get; set; }

        public event UnityAction startPlayerTurnEvent;
        public event UnityAction startEnemyTurnEvent;
        public event UnityAction FinishEnemyTurnEvent;

        public void OnStartPlayerTurn() => startPlayerTurnEvent?.Invoke();
        public void OnStartEnemyTurn() => startEnemyTurnEvent?.Invoke();
        public void OnFinishEnemyTurn() => FinishEnemyTurnEvent?.Invoke();

        public void AddEnemy(Pawn enemy) => GameManager.AddEnemy(enemy);
        public void RemoveEnemy(Pawn enemy) => GameManager.RemoveEnemy(enemy);

        public void SetGameStatus(GameStatus gameStatus) => GameManager.SetGameStatus(gameStatus);

        public void AddCommandPlaylist(CommandPlayList commandPlaylist) => GameManager.AddCommandPlaylist(commandPlaylist);
    }
}
