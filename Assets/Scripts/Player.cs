using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
    }
}