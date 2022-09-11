using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script.A.NodeLogic
{
    public class Arrow : MonoBehaviour
    {
        public Node node;
        public NodeInteraction nodeInteraction;

        public Node PlayerChoose()
        {
            nodeInteraction.DisableArrows();
            return node;
        }
    }
}