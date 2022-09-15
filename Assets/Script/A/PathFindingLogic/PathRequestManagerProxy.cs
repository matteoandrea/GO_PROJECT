using Assets.Script.A.NodeLogic;
using Assets.Script.Manager;
using System;
using UnityEngine;

namespace Assets.Script.A.PathFindingLogic
{
    [CreateAssetMenu(fileName = "PathRequestManagerProxy", menuName = "Managers/PathRequest Manager Proxy")]
    public class PathRequestManagerProxy : ScriptableObject, IPathRequestManager
    {
        public PathRequestManager PathRequestManager { private get; set; }

        public void RequestPath(Node pathStart, Node pathEnd, Action<Vector3[], bool> callback)
            => PathRequestManager.RequestPath(pathStart, pathEnd, callback);
    }
}