using UnityEngine;

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
        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeedInAir, rb.velocity.y);

        if (stateTimer < -0.5f && rb.velocity.y == 0 && player.CheckIfGrounded())
            stateMachine.ChangeState(player.idleState);

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
    }
}
