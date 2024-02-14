using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected float xInput;
    protected bool dashInput;
    protected Rigidbody2D rb;
    protected float stateTimer;
    private readonly string animationBool;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animationBool)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animationBool = _animationBool;
    }

    public virtual void Enter()
    {
        player.Animator.SetBool(animationBool, true);
        rb = player.Rigidbody2D;
    }

    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;
        xInput = player.InputHandler.HorizontalInput.x;
        dashInput = player.InputHandler.DashInput;
        player.Animator.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void PhysicUpdate()
    {
        if (dashInput && !player.CheckIfGrounded())
            player.stateMachine.ChangeState(player.inAirState);
    }

    public virtual void Exit()
    {
        player.Animator.SetBool(animationBool, false);
    }
}