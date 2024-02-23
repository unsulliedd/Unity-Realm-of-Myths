using UnityEngine;

public class PlayerState
{
    #region References
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;
    #endregion
    #region Input Variables
    protected float xInput;
    protected bool jumpInput;
    protected bool dashInput;
    protected bool slideInput;
    #endregion
    #region Variables
    protected float stateTimer;
    protected bool isGrounded;
    protected bool isTouchingWall;
    private readonly string animationBool;
    #endregion

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animationBool)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animationBool = _animationBool;
    }

    public virtual void CollisionChecks() 
    { 
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
    }

    public virtual void Enter()
    {
        CollisionChecks();
        player.Animator.SetBool(animationBool, true);
        rb = player.Rigidbody2D;
    }

    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;

        player.UpdateJumpCounters();

        xInput = player.InputHandler.HorizontalInput.x;
        jumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput;
        slideInput = player.InputHandler.SlideInput;

        player.Animator.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void PhysicUpdate()
    {
        CollisionChecks();
        if (!isGrounded && !isTouchingWall && stateMachine.currentState != player.DashState)
            player.StateMachine.ChangeState(player.InAirState);
    }

    public virtual void Exit()
    {
        player.Animator.SetBool(animationBool, false);
    }
}