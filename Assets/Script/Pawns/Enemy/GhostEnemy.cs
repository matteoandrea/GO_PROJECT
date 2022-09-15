using Assets.Script.A.PathFindingLogic;
using System.Collections;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace Assets.Script.Pawns.Enemy
{
    public class GhostEnemy : EnemyBase
    {
        protected override void Start()
        {
            base.Start();

        }

        protected override IEnumerator Act()
        {
            if (currentPath == null)
            {
                _pathProxy.RequestPath(currentNode, _currentNodetarget, CalculatePath);
                yield return null;
            }
            if (_startNodeTarget == null) _startNodeTarget = currentNode;


            if (currentWaypoint > currentPath.Length - 1)
            {
                if (currentNode == _startNodeTarget) _currentNodetarget = _endNodeTarget;
                else _currentNodetarget = _startNodeTarget;

                _pathProxy.RequestPath(currentNode, _currentNodetarget, CalculatePath);
                currentWaypoint = 0;
                yield return null;
            }

            _targetPosition = currentPath[currentWaypoint];
            MoveAction();
            currentWaypoint++;

        }
    }
}