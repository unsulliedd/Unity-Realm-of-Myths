using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Machine
    public PlayerStateMachine stateMachine;

    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerJumpState jumpState;
    public PlayerInAirState inAirState;
    #endregion

    #region Components
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    #endregion

    [Header("Move Info")]
    public float moveSpeed = 10f;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        inAirState = new PlayerInAirState(this, stateMachine, "Jump");

        Animator = GetComponentInChildren<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        InputHandler = GetComponent<PlayerInputHandler>();
    }

    void Start()
    {
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public void SetVelocityX(float xVelocity, float yVelocity)
    {
        Rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);
    }
}