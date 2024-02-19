public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animationBool) : base(_player, _stateMachine, _animationBool)
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

        if (slideInput && player.InputHandler.slideTimer < 0 && isGrounded)
        {
            player.InputHandler.slideTimer = player.slideCooldown;
            stateMachine.ChangeState(player.SlideState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
