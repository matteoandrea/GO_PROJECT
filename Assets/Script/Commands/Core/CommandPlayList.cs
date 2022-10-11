using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assets.Script.Commands.Core
{
    public class CommandPlayList
    {
        private Queue<ICommand> commands = new();

        public void AddCommand(ICommand command) => commands.Enqueue(command);

        public IEnumerator Execute(Action callback)
        {
            foreach (var item in commands)
            {
                yield return item.Execute();
            }

            commands.Clear();
            callback?.Invoke();
        }
    }
}