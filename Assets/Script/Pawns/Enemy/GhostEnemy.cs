using Assets.Script.A.PathFindingLogic;
using System.Collections;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace Assets.Script.Pawns.Enemy
{
    public class GhostEnemy : EnemyBase
    {
        protected override IEnumerator Init()
        {
            yield return base.Init();
            if (_currentPath == null)
            {
                _pathProxy.RequestPath(currentNode, _currentNodetarget, CalculatePath);
                yield return null;
            }
        }
        protected override IEnumerator Act()
        {
            if (_currentWaypoint > _currentPath.Length - 1)
            {
                if (currentNode == _startNodeTarget) _currentNodetarget = _endNodeTarget;
                else _currentNodetarget = _startNodeTarget;

                _pathProxy.RequestPath(currentNode, _currentNodetarget, CalculatePath);
                _currentWaypoint = 0;
                yield return null;
            }

            _targetPosition = _currentPath[_currentWaypoint];
            MoveAction();
            _currentWaypoint++;

        }
    }
}