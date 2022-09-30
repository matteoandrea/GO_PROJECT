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
    internal class RotateCommand : ICommand
    {
        private Transform transform;
        private float rotationSpeed;

        public RotateCommand(Transform transform, float rotationSpeed)
        {
            this.transform = transform;
            this.rotationSpeed = rotationSpeed;
        }

        public IEnumerator Execute()
        {
            var y = transform.localRotation.eulerAngles.y;
            if (y == 180)
                transform.DORotate(Vector3.down
                       * 180, rotationSpeed, RotateMode.LocalAxisAdd);
            else transform.DORotate(Vector3.up
                        * 180, rotationSpeed, RotateMode.LocalAxisAdd);

            yield return new WaitForSeconds(rotationSpeed);
        }
    }
}
