namespace Assets.Script.Pawns.Enemy
{
    public class GhostEnemy : EnemyBase
    {
        protected override void OnStartTurn()
        {
            var value = IsNodeInSight();

            if (value.Item1)
                MoveAction(value.Item2);
            else
                Rotate();

            VerifyRotate();

            gameManagerProxy.AddCommandPlaylist(commandPlayList);
        }
    }
}