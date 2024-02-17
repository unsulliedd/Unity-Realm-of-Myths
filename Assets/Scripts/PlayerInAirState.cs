public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(Player _player, PlayerStateMachine _stateMachine, string _animationBool) : base(_player, _stateMachine, _animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (stateTimer < -0.5f && rb.velocity.y == 0 && isGrounded)
            stateMachine.ChangeState(player.IdleState);

        if (dashInput && player.InputHandler.DashTimer < 0)
        {
            player.InputHandler.DashTimer = player.dashCooldown;
            player.InputHandler.DashInputHelper();
            stateMachine.ChangeState(player.DashState);
        }

        if (rb.velocity.y < 0 && isTouchingWall && !isGrounded)
            stateMachine.ChangeState(player.WallSlideState);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeedInAir, rb.velocity.y);
    }
}
