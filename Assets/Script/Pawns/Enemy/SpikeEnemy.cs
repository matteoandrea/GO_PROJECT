using Assets.Script.Pawns.Enemy;

public class SpikeEnemy : EnemyBase
{
    protected override void OnStartTurn()
    {
        var value = IsPLayerNear();

        if (value.Item1)
            MoveAction(value.Item2);
        else
            Rotate();

        gameManagerProxy.AddCommandPlaylist(commandPlayList);
    }
}
