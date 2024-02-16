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
            stateMachine.ChangeState(player.idleState);

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
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        player.SetVelocity(player.facingDirection * player.slideSpeed, rb.velocity.y);
    }
}
