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
    public float jumpForce = 10f;
    [SerializeField] private bool _isFacingRight = true;

    [Header("Collision Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallCheckDistance = 0.2f;

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

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);
        Flip();
    }

    private void Flip()
    {
        if (InputHandler.horizontalInput.x < 0 && _isFacingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            _isFacingRight = !_isFacingRight;
        }
        else if (InputHandler.horizontalInput.x > 0 && !_isFacingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            _isFacingRight = !_isFacingRight;
        }
    }
    public bool CheckIfGrounded() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    public bool CheckIfTouchingWall() => Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, wallLayer);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}