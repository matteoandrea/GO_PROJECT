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

        public void AddCommand(ICommand command) => GameManager.AddCommand(command);
    }
}
