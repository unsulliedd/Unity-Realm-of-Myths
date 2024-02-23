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

        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeedInAir, rb.velocity.y);

        if (rb.velocity.y == 0 && isGrounded)
            stateMachine.ChangeState(player.IdleState);

        if (rb.velocity.y < 0 && isTouchingWall && !isGrounded)
            stateMachine.ChangeState(player.WallSlideState);

        if (jumpInput)
        {
            if (player.firstJumpCompleted && player.doubleJumpTimer < 0 && player.JumpState.CanDoubleJump())
            {
                player.doubleJumpTimer = player.doubleJumpCooldown;
                player.firstJumpCompleted = false;
                JumpState();
            }
            else if (player.coyoteJumpTimer > 0)
            {
                player.coyoteJumpTimer = player.coyoteTime;
                player.firstJumpCompleted = true;
                player.JumpState.ResetJumps();
                JumpState();
            }
            else
            {
                player.InputHandler.JumpInputHelper();
                return;
            }
        }

        if (dashInput && player.InputHandler.dashTimer < 0)
        {
            player.InputHandler.dashTimer = player.dashCooldown;
            player.InputHandler.DashInputHelper();
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    private void JumpState()
    {
        player.InputHandler.JumpInputHelper();
        stateMachine.ChangeState(player.JumpState);
    }
}
