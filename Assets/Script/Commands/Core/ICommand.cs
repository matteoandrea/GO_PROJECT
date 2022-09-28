using Cysharp.Threading.Tasks;

namespace Assets.Script.Commands.Core
{
    public interface ICommand
    {
        UniTaskVoid Execute();
    }
}