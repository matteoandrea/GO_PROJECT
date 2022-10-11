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
            var value = IsPawnInSight(pawLayerMask);

            var pos = line.GetPosition(1);
            pos.z = value.Item3.z;
            line.SetPosition(1, pos);
            
            if (value.Item1)
                ShootAction(value.Item2);
            else
                Pass();

            gameManagerProxy.AddCommandPlaylist(commandPlayList);
        }
        private void ShootAction(BaseNode node)
        {
            ICommand command = new ShootCommand(node);
            commandPlayList.AddCommand(command);
        }
    }
}