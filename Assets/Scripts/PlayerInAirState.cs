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
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        if (stateTimer > 0.5f && rb.velocity.y == 0 && player.CheckIfGrounded())
            stateMachine.ChangeState(player.idleState);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
