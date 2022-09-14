using Assets.Script.A.NodeLogic;
using Assets.Script.A.PathFindingLogic;
using Assets.Script.Input;
using Assets.Script.Manager;
using Assets.Script.Pawns.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Pawns.Enemy
{
    public abstract class EnemyBase : Pawn
    {
        [SerializeField] private GameManagerProxy _managerProxy = default;

        public Transform target;
        public Vector3[] currentPath;
        public PathRequestManagerProxy pathProxy;
        public int currentWaypoint;

        protected virtual void OnEnable() =>
            _managerProxy.startEnemyTurnEvent += ThinkToAct;

        protected virtual void OnDisable() =>
            _managerProxy.startEnemyTurnEvent -= ThinkToAct;

        protected virtual void Start() => _managerProxy.AddEnemy(this);

        protected abstract void ThinkToAct();

        protected void CalculatePath(Vector3[] newPath, bool pathSuccessful)
        {
            if (pathSuccessful) currentPath = newPath;
        }

        private void OnTriggerEnter(Collider hit)
        {
            if (hit.tag != "Node") return;

            _nodeInteraction = hit.GetComponent<NodeInteraction>();
            _nodeInteraction.AddEnemyToList(this);
        }

        private void OnTriggerExit(Collider hit)
        {
            if (hit.tag != "Node") return;

            _nodeInteraction = hit.GetComponent<NodeInteraction>();
            _nodeInteraction.RemoveEnemyFromList(this);
        }
    }
}
