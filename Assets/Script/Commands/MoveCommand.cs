using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Commands
{
    public abstract class MoveCommand:ICommand
    {
        protected Transform _transform { get; set; }
        protected Vector3 _targetPosition { get; set; }
        protected Animator _animator { get; set; }
        protected float _speedWalk { get; set; }
        protected float _speedRotation { get; set; }

        protected MoveCommand(Vector3 targetPosition, float speedWalk, float speedRotation, Transform transform, Animator animator)
        {
            this._targetPosition = targetPosition;
            this._speedWalk = speedWalk;
            this._speedRotation = speedRotation;
            this._transform = transform;
            this._animator = animator;
        }

        public abstract void Execute();
    }
}
