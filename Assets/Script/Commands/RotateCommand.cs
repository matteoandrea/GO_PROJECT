using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Commands
{
    internal class RotateCommand : ICommand
    {
        private Transform _transform;
        private float _rotationSpeed;

        public RotateCommand(Transform transform, float rotationSpeed)
        {
            _transform = transform;
            _rotationSpeed = rotationSpeed;
        }

        public void Execute()
        {
            var y = _transform.localRotation.eulerAngles.y;
            if (y == 180)
                _transform.DORotate(Vector3.down
                       * 180, _rotationSpeed, RotateMode.LocalAxisAdd);
            else _transform.DORotate(Vector3.up
                        * 180, _rotationSpeed, RotateMode.LocalAxisAdd);
        }
    }
}
