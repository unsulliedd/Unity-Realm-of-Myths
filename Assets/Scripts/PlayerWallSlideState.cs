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


        if (jumpInput)
        {
            player.InputHandler.JumpInputHelper();
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if (xInput == player.facingDirection && player.CheckIfTouchingWall())
            player.SetVelocity(xInput, 0);
        else
            player.SetVelocity(xInput, -player.moveSpeedOnWall);

        if (xInput != player.facingDirection && !player.CheckIfTouchingWall())
            stateMachine.ChangeState(player.inAirState);
        else if (player.CheckIfGrounded())
            stateMachine.ChangeState(player.idleState);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
