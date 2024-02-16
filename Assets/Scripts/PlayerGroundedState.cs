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

        if (jumpInput && player.CheckIfGrounded())
        {            
            player.InputHandler.JumpInputHelper();
            stateMachine.ChangeState(player.jumpState);
        }

        if (dashInput && player.InputHandler.DashTimer < 0)
        {
            player.InputHandler.DashTimer = player.dashCooldown;
            player.InputHandler.DashInputHelper();
            stateMachine.ChangeState(player.dashState);
        }

        if (slideInput && player.InputHandler.SlideTimer < 0 && player.CheckIfGrounded())
        {
            player.InputHandler.SlideTimer = player.slideCooldown;
            stateMachine.ChangeState(player.slideState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
