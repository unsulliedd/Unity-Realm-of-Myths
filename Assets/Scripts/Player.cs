using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Machine
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerSlideState SlideState { get; private set; }
    public PlayerPrimaryAttackState PrimaryAttackState { get; private set; }
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
    public float wallJumpTime = 0.25f;
    public float wallJumpLength = 10;
    public int amountOfJumps = 2;
    public bool firstJumpCompleted;
    public float doubleJumpCooldown = 2.5f;
    public float doubleJumpTimer;
    public float coyoteTime = 0.2f;
    public float coyoteJumpTimer;

    [Header("Dash Info")]
    public float dashForce = 20;
    public float dashTime = 0.2f;
    public float dashCooldown = 4f;

    [Header("Slide Info")]
    public float slideSpeed = 7f;
    public float slideTime = 0.75f;
    public float slideCooldown = 1.5f;

    [Header("Collision Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.45f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallCheckDistance = 0.25f;
    #endregion

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        InAirState = new PlayerInAirState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "Jump");
        SlideState = new PlayerSlideState(this, StateMachine, "Slide");
        PrimaryAttackState = new PlayerPrimaryAttackState(this, StateMachine, "Attack");

        Animator = GetComponentInChildren<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        InputHandler = GetComponent<PlayerInputHandler>();
    }

    void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        StateMachine.currentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        StateMachine.currentState.PhysicUpdate();
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

    public void UpdateJumpCounters()
    {
        if (CheckIfGrounded())
            coyoteJumpTimer = coyoteTime;
        else
            coyoteJumpTimer -= Time.deltaTime;

        doubleJumpTimer -= Time.deltaTime;
    }

    public void AnimationTrigger() => StateMachine.currentState.AnimationFinishTrigger();

    public bool CheckIfGrounded() => Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckDistance, groundLayer);
    public bool CheckIfTouchingWall() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, wallLayer);

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}