using System.Collections;
using System.Collections.Generic;
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
        if (player.CheckIfGrounded())
            stateMachine.ChangeState(player.idleState);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
