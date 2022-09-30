namespace Assets.Script.Pawns.Enemy
{
    public class GuardEnemy : EnemyBase
    {
        protected override void OnStartTurn()
        {
            var values = IsPLayerNear();

            if (values.Item1)
                MoveAction(values.Item2);
            else
                Pass();

            gameManagerProxy.AddCommandPlaylist(commandPlayList);
        }
    }
}