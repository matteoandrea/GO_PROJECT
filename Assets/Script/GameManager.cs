using Assets.Script.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private VoidEventSO _startPlayerTurn;
        [SerializeField] private VoidEventSO _finishPlayerTurn;

        [SerializeField] private VoidEventSO _startEnemyTurn;
        [SerializeField] private VoidEventSO _finishEnemyTurn;

        [SerializeField] private Turn _turn = Turn.Player;


    }

    public enum Turn
    {
        Player,
        Enemy
    }
}