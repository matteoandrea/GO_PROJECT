using System;
using UnityEngine;

namespace Assets.Script.A.PathFindingLogic
{
    public interface IPathRequestManager
    {
        public void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback);
    }
}