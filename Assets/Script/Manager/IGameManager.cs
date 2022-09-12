using Assets.Script.Command;
using Assets.Script.Pawns.Core;

namespace Assets.Script.Manager
{
    public interface IGameManager
    {
        public void AddCommand(ICommand command);
        public bool GameWon { get; set; }
        public bool GameLost { get; set; }

        public void AddEnemy(Pawn enemy);
        public void RemoveEnemy(Pawn enemy);
    }
}