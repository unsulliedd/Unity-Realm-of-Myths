public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animationBool) : base(_player, _stateMachine, _animationBool)
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


        if (jumpInput && xInput == -player.facingDirection)
        {
            player.InputHandler.JumpInputHelper();
            stateMachine.ChangeState(player.WallJumpState);
            return;
        }

        if (dashInput && xInput == -player.facingDirection && player.InputHandler.DashTimer < 0)
        {
            player.InputHandler.DashTimer = player.dashCooldown;
            player.InputHandler.DashInputHelper();
            stateMachine.ChangeState(player.DashState);
            return;
        }

        if (xInput != player.facingDirection && !isTouchingWall)
            stateMachine.ChangeState(player.InAirState);
        else if (player.CheckIfGrounded())
            stateMachine.ChangeState(player.IdleState);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        if (xInput == player.facingDirection && isTouchingWall)
            player.SetVelocity(xInput, 0);
        else
            player.SetVelocity(xInput, -player.moveSpeedOnWall);
    }
}
