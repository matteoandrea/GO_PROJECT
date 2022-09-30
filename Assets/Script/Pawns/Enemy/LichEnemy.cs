namespace Assets.Script.Pawns.Enemy
{
    public class LichEnemy : EnemyBase
    {
        protected override void OnStartTurn()
        {
            var value = IsPlayerInSight();

            if (value.Item1)
                MoveAction(value.Item2);
            else
                Pass();

            gameManagerProxy.AddCommandPlaylist(commandPlayList);
        }
    }
}