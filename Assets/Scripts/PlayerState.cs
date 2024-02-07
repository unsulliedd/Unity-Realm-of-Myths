public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

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
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicUpdate()
    {

    }

    public virtual void Exit()
    {
        player.Animator.SetBool(animationBool, false);
    }
}