public class PlayerSlideState : PlayerGroundedState
{
    public PlayerSlideState(Player _player, PlayerStateMachine _stateMachine, string _animationBool) : base(_player, _stateMachine, _animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.slideTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateTimer < 0 || !slideInput)
            stateMachine.ChangeState(player.IdleState);

        if (jumpInput && isGrounded)
        {
            player.InputHandler.JumpInputHelper();
            stateMachine.ChangeState(player.JumpState);
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

        player.SetVelocity(player.facingDirection * player.slideSpeed, rb.velocity.y);
    }
}
