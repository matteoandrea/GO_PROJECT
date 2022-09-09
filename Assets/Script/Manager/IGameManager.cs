using Assets.Script.Command;

namespace Assets.Script.Manager
{
    public interface IGameManager
    {
        public void AddCommand(ICommand command);
    }
}