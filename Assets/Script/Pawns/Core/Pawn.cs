﻿using Assets.Script.A;
using Assets.Script.A.NodeLogic;
using Assets.Script.Command;
using Assets.Script.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.Pawns.Core
{
    public abstract class Pawn : MonoBehaviour
    {
        [HideInInspector] public Node currentNode;

        [SerializeField] protected GameManagerProxy _gameManagerProxy;

        protected Animator _animator;
        protected Vector3 _targetPosition;
        protected NodeInteraction _nodeInteraction;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected virtual void MoveAction()
        {
            ICommand command = new MoveCommand(_targetPosition, transform, _animator);
            _gameManagerProxy.AddCommand(command);
        }
    }
}