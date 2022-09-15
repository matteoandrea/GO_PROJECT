using Assets.Script.A.NodeLogic;
using System;
using UnityEngine;

namespace Assets.Script.A.PathFindingLogic
{
    public interface IPathRequestManager
    {
        public void RequestPath(Node startNode, Node endNode, Action<Vector3[], bool> callback);
    }
}