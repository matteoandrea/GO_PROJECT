using Assets.Script.Nodes.Core;
using System.Collections;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace Assets.Script.Pawns.Enemy
{
    public class GhostEnemy : EnemyBase
    {
        protected override IEnumerator Act()
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerator Init()
        {
            yield return base.Init();
            if (_currentPath == null)
            {
                //_pathProxy.RequestPath(currentNode, _currentNodetarget, CalculatePath);
                yield return null;
            }
        }

        protected override void OnEnterNode(NodeCore node)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnExitNode(NodeCore node)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnStartTurn()
        {
            throw new System.NotImplementedException();
        }
    }
}