using Assets.Script.Commands.Core;
using Assets.Script.Pawns.Core;

namespace Assets.Script.Manager
{
    public interface IGameManager
    {
        public void AddCommandPlaylist(CommandPlayList commandPlaylist);
        public void AddEnemy(Pawn enemy);
        public void RemoveEnemy(Pawn enemy);
        public void SetGameStatus(GameStatus gameStatus);
    }
}