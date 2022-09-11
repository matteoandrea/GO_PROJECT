using System;
using System.Collections;
using Assets.Script.Command;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Assets.Script.Manager
{
    [CreateAssetMenu(fileName = "GameManagerProxy", menuName = "Managers/Game Manager Proxy")]
    public class GameManagerProxy : ScriptableObject, IGameManager
    {
        public GameManager GameManager { private get; set; }

        public event UnityAction startPlayerTurnEvent;
        public event UnityAction startEnemyTurnEvent;

        public void AddCommand(ICommand command) => GameManager.AddCommand(command);

        public void OnStartPlayerTurn() => startPlayerTurnEvent?.Invoke();
        public void OnStartEnemyTurn() => startEnemyTurnEvent?.Invoke();
    }
}
