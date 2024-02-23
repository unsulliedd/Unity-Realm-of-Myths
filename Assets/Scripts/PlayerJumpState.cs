using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private int amountOfJumpsLeft;

    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animationBool) : base(_player, _stateMachine, _animationBool)
    {
        amountOfJumpsLeft = player.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(rb.velocity.x, player.jumpForce);
        amountOfJumpsLeft--;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (rb.velocity.y != 0)
            stateMachine.ChangeState(player.InAirState);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public bool CanDoubleJump() => amountOfJumpsLeft > 0;
    public void ResetJumps() => amountOfJumpsLeft = player.amountOfJumps;
}