using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.A.PathFindingLogic
{
    public class PathRequestManager : MonoBehaviour, IPathRequestManager
    {
        Queue<PathRequest> pathRequestsQueue = new Queue<PathRequest>();
        private PathRequest _currentRequest;
        private PathFinding _pathFinding;

        [SerializeField] private PathRequestManagerProxy _pathProxy;
        bool _isProcessingPath;

        private void Awake()
        {
            _pathProxy.PathRequestManager = this;
            _pathFinding = GetComponent<PathFinding>();
        }

        public void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
        {
            PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
            pathRequestsQueue.Enqueue(newRequest);
            TryProcessNext();
        }

        public void FinishProcessPath(Vector3[] path, bool success)
        {
            _currentRequest.callback(path, success);
            _isProcessingPath = false;
            TryProcessNext();
        }

        private void TryProcessNext()
        {
            if (!_isProcessingPath && pathRequestsQueue.Count > 0)
            {
                _currentRequest = pathRequestsQueue.Dequeue();
                _isProcessingPath = true;
                _pathFinding.StartFindPath(_currentRequest.pathStart, _currentRequest.pathEnd);
            }
        }

        struct PathRequest
        {
            public Vector3 pathStart;
            public Vector3 pathEnd;
            public Action<Vector3[], bool> callback;

            public PathRequest(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
            {
                this.pathStart = pathStart;
                this.pathEnd = pathEnd;
                this.callback = callback;
            }
        }
    }
}
