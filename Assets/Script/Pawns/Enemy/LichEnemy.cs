using Assets.Script.Commands;
using Assets.Script.Commands.Core;
using Assets.Script.Nodes.Core;
using UnityEngine;

namespace Assets.Script.Pawns.Enemy
{
    public class LichEnemy : EnemyBase
    {
        private LineRenderer line;

        protected override void Awake()
        {
            base.Awake();
            line = GetComponent<LineRenderer>();
        }

        protected override void OnStartTurn()
        {
            var value = IsPawnInSight(nodeLayerMask);

            var pos = line.GetPosition(1);
            pos.z = value.Item2.z;
            line.SetPosition(1, pos);

            ShootAction(value.Item1);

            gameManagerProxy.AddCommandPlaylist(commandPlayList);
        }
        private void ShootAction(BaseNode node)
        {
            ICommand command = new ShootCommand(node);
            commandPlayList.AddCommand(command);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.forward);
        }
    }
}