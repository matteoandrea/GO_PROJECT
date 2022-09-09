using Assets.Script.Manager;
using Assets.Script.Pawns;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.Command
{
    public class MoveCommand : ICommand
    {
        Vector3 targetPositon;
        Transform transform;
        NavMeshAgent agent;

        public MoveCommand(Vector3 targetPositon, NavMeshAgent agent, Transform transform)
        {
            this.transform = transform;
            this.agent = agent;
            this.targetPositon = targetPositon;
        }

        public void Execute()
        {
            var path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, targetPositon, NavMesh.AllAreas, path);
            agent.SetPath(path);
            var link = agent.nextOffMeshLinkData.endPos;
            NavMesh.CalculatePath(transform.position, link, NavMesh.AllAreas, path);
            agent.SetPath(path);
        }
    }
}