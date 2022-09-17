using Assets.Script.Commands;
using Assets.Script.Nodes.Core;
using UnityEngine;
using static PlasticPipe.PlasticProtocol.Messages.NegotiationCommand;

namespace Assets.Script.Pawns.Enemy
{
    public class GhostEnemy : EnemyBase
    {
        protected override void OnStartTurn()
        {
            if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity,
                                 _incluedlayerMask))
            {
                ICommand command = new RotateCommand(transform, _rotationSpeed);
                _gameManagerProxy.AddCommand(command);
            }
            else
            {
                var script = hit.collider.GetComponent<BaseNode>();
                if (script != null)
                    MoveAction(hit.transform.position);
            }
        }
    }
}