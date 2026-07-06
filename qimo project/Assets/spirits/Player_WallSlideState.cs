public class Player_WallSlideState : EntityState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();

        if (player.wallDetected == false)
            StateMachine.changeState(player.fallstate);

        if (player.groundDetected)
        {
            StateMachine.changeState(player.idlestate);
            player.Flip();
        }
    }

    private void HandleWallSlide()
    {
        if (player.moveinput.y < 0)
            player.SetVelocity(player.moveinput.x, rb.velocity.y);
        else
            player.SetVelocity(player.moveinput.x, rb.velocity.y * player.wallSlideSlowMultiplier);
    }
}