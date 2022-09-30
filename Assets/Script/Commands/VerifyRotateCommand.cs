using Assets.Script.Commands.Core;
using Assets.Script.Nodes.Core;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Commands
{
    internal class VerifyRotateCommand : ICommand
    {
        private Transform transform;
        private float rotationSpeed;
        private LayerMask incluedLayerMask;

        public VerifyRotateCommand(Transform transform, float rotationSpeed, LayerMask incluedLayerMask)
        {
            this.transform = transform;
            this.rotationSpeed = rotationSpeed;
            this.incluedLayerMask = incluedLayerMask;
        }

        public IEnumerator Execute()
        {
            if (IsNecessaryRotate())
            {
                ICommand command = new RotateCommand(
                    transform,
                    rotationSpeed);
                yield return command.Execute();
            }
            else
                yield break;
        }

        private bool IsNecessaryRotate()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, incluedLayerMask))
            {
                BaseNode script = hit.collider.GetComponent<BaseNode>();

                if (script != null)
                    return false;
                else
                    return true;
            }
            else
            {
                return true;
            }
        }
    }
}
