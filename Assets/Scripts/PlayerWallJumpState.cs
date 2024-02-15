using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animationBool) : base(_player, _stateMachine, _animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.wallJumpTime;
        player.SetVelocity(player.wallJumpLength * -player.facingDirection, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateTimer < 0)
            stateMachine.ChangeState(player.inAirState);

        if (player.CheckIfGrounded())
            stateMachine.ChangeState(player.idleState);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
