public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animationBool) : base(_player, _stateMachine, _animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.firstJumpCompleted = false;
        player.JumpState.ResetJumps();
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
            player.firstJumpCompleted = true;
            player.InputHandler.JumpInputHelper();
            stateMachine.ChangeState(player.JumpState);
        }

        else if (attackInput)
        {
            player.InputHandler.AttackInputHelper();
            stateMachine.ChangeState(player.PrimaryAttackState);
        }

        else if (dashInput && player.InputHandler.dashTimer < 0)
        {
            player.InputHandler.dashTimer = player.dashCooldown;
            player.InputHandler.DashInputHelper();
            stateMachine.ChangeState(player.DashState);
        }

        else if (slideInput && player.InputHandler.slideTimer < 0)
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
