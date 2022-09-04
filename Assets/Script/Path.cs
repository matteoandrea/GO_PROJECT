using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class Path : MonoBehaviour
    {
        public Path[] directions;
        public int[] location;

        [SerializeField]
        private Link _linkPrefab;
        private List<Link> _links;

        public void Initit(int x, int y)
        {
            this.name = (x + "," + y);
            directions = new Path[4];
            location = new int[2];
            location[0] = x;
            location[1] = y;
        }

        public void CreateLink(Transform endPoint)
        {
            var link = Instantiate(_linkPrefab);
            link.transform.SetParent(this.transform);
            link.SetEndPoint(endPoint);
            _links.Add(link);
        }
    }
}