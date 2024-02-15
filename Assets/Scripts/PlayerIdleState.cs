public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animationBool) : base(_player, _stateMachine, _animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xInput == player.facingDirection && player.CheckIfTouchingWall())
            return;

        if (rb.velocity.y == 0 && xInput != 0 && player.CheckIfGrounded())
            stateMachine.ChangeState(player.moveState);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
