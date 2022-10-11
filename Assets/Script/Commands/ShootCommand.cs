using Assets.Script.Commands.Core;
using UnityEngine;
using System.Collections;
using Assets.Script.Nodes.Core;

namespace Assets.Script.Commands
{
    internal class ShootCommand : ICommand
    {
        private BaseNode baseNode;


        public IEnumerator Execute()
        {
            baseNode.KillPlayer();
            yield break;
        }

        public ShootCommand(BaseNode baseNode)
        {
            this.baseNode = baseNode;
        }
    }
}