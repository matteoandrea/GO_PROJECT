using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

namespace Assets.Script
{
    public class Link : MonoBehaviour
    {
        private NavMeshLink nav;

        private void Awake() => nav = GetComponent<NavMeshLink>();

        public void SetEndPoint(Transform endPoint) => nav.endPoint = endPoint.position;
    }
}