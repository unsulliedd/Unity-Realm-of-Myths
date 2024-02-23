public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animationBool) : base(_player, _stateMachine, _animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashTime;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateTimer < 0 && isTouchingWall && !isGrounded)
            stateMachine.ChangeState(player.WallSlideState);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.IdleState);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        player.SetVelocity(player.facingDirection * player.dashForce, 0);
    }
}
