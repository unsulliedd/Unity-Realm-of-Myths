using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Machine
    public PlayerStateMachine stateMachine;

    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    #endregion

    #region Components
    public Animator Animator { get; private set; }

    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");

        Animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }
}