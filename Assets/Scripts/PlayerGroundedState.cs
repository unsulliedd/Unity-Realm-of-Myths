using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected bool jumpInput;
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animationBool) : base(_player, _stateMachine, _animationBool)
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

        jumpInput = player.InputHandler.JumpInput;
        if (jumpInput && player.CheckIfGrounded())
        {
            player.InputHandler.JumpInputHelper();
            stateMachine.ChangeState(player.jumpState);
        }

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
