using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animationBool) : base(_player, _stateMachine, _animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (rb.velocity.y != 0)
            stateMachine.ChangeState(player.inAirState);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}