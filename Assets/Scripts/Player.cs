using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Machine
    public PlayerStateMachine stateMachine;

    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerJumpState jumpState;
    public PlayerInAirState inAirState;
    public PlayerDashState dashState;
    public PlayerWallSlideState wallSlideState;
    #endregion

    #region Components
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    #endregion

    #region Variables
    [Header("Move Info")]
    public float moveSpeed = 10f;
    public float moveSpeedInAir = 7f;
    public float moveSpeedOnWall = 8f;
    public int facingDirection = 1;
    [SerializeField] private bool _isFacingRight = true;

    [Header("Jump Info")]
    public float jumpForce = 25;

    [Header("Dash Info")]
    public float dashForce = 20;
    public float dashTime = 0.2f;
    public float dashCooldown = 4f;

    [Header("Collision Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.45f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallCheckDistance = 0.2f;
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        inAirState = new PlayerInAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");

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

    void FixedUpdate()
    {
        stateMachine.currentState.PhysicUpdate();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);
        FlipController();
    }

    private void FlipController()
    {
        if (InputHandler.HorizontalInput.x < 0 && _isFacingRight)
            Flip();
        else if (InputHandler.HorizontalInput.x > 0 && !_isFacingRight)
            Flip();
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        _isFacingRight = !_isFacingRight;
        facingDirection *= -1;
    }

    public bool CheckIfGrounded() => Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckDistance, groundLayer);
    public bool CheckIfTouchingWall() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, wallLayer);

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}