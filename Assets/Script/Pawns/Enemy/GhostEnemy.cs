using Assets.Script.A.PathFindingLogic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Script.Pawns.Enemy
{
    public class GhostEnemy : EnemyBase
    {
        protected override void Start()
        {
            base.Start();
            pathProxy.RequestPath(transform.position, target.position, CalculatePath);
        }

        protected override void ThinkToAct()
        {
            if (currentWaypoint >= currentPath.Length - 1)
            {
                currentPath.Reverse();
                currentWaypoint = 0;
            }

            currentWaypoint++;
            _targetPosition = currentPath[currentWaypoint];

            MoveAction();
        }
    }
}