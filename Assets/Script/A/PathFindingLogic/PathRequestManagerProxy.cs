using Assets.Script.Manager;
using System;
using UnityEngine;

namespace Assets.Script.A.PathFindingLogic
{
    [CreateAssetMenu(fileName = "PathRequestManagerProxy", menuName = "Managers/PathRequest Manager Proxy")]
    public class PathRequestManagerProxy : ScriptableObject, IPathRequestManager
    {
        public PathRequestManager PathRequestManager { private get; set; }

        public void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
            => PathRequestManager.RequestPath(pathStart, pathEnd, callback);
    }
}