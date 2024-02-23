using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float timeBetweenAttacks = 0.5f;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animationBool) : base(_player, _stateMachine, _animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (comboCounter >= player.comboCount || Time.time >= lastTimeAttacked + timeBetweenAttacks)
            comboCounter = 0;
        player.Animator.SetInteger("ComboCounter", comboCounter);
        stateTimer = 0.1f;
    }

    public override void Exit()
    {
        base.Exit();

        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateTimer < 0)
            player.SetZeroVelocity();

        if (animationTriggered)            
            stateMachine.ChangeState(player.IdleState);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
