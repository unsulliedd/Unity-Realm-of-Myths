public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animationBool) : base(_player, _stateMachine, _animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xInput == player.facingDirection && isTouchingWall)
            return;

        if (rb.velocity.y == 0 && xInput != 0 && isGrounded)
            stateMachine.ChangeState(player.MoveState);

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
