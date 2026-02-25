using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField]
    private State currentState;
    [SerializeField]
    private CharacterController characterController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CharacterState GetState => currentState.GetCharacterState();

    public void ChangeState(State newState)
    {
        if(currentState == newState)
        {
            return;
        }
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void GoIdle()
    {
        ChangeState(new IdleState(this, characterController, characterController.GetAnimator()));
    }

    public void GoWalking()
    {
        ChangeState(new WalkingState(this, characterController, characterController.GetAnimator()));
    }
}
