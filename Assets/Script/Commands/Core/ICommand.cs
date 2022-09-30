using Cysharp.Threading.Tasks;
using System.Collections;

namespace Assets.Script.Commands.Core
{
    public interface ICommand
    {
        IEnumerator Execute();
    }
}