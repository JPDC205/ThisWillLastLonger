using UnityEngine;

// Remove this duplicate IdleState class definition if another exists elsewhere in your project.
// If you have another IdleState class in a different file, delete one of them or merge their contents as needed.
// Only one IdleState class definition should exist in the global namespace.

public abstract class State
{
    protected StateMachine stateMachine;
    protected CharacterController characterController;
    protected CharacterState characterState;
    protected Animator animator;

    public State(StateMachine stateMachine, CharacterController characterController, Animator animator)
    {
        this.stateMachine = stateMachine;
        this.characterController = characterController;
        this.animator = animator;
    }

    public abstract void Enter();
    public abstract void Exit();

    public CharacterState GetCharacterState()
    {
        return characterState;
    }
}

public class IdleState : State
{
        public IdleState(StateMachine stateMachine, CharacterController characterController, Animator animator)
        : base(stateMachine, characterController, animator)
    {
        characterState = CharacterState.Idle;
    }

    public override void Enter()
    {
        animator.SetBool("isIdle", true);
    }
    public override void Exit()
    {
        animator.SetBool("isIdle", false);
    }
}

public class WalkingState : State
{
    public WalkingState(StateMachine stateMachine, CharacterController characterController, Animator animator)
        : base(stateMachine, characterController, animator)
    {
        characterState = CharacterState.Walking;
    }

    public override void Enter()
    {
        animator.SetBool("isWalking", true);
    }
    public override void Exit()
    {
        animator.SetBool("isWalking", false);
    }
}

public class DefaultActionState : State
{
    public DefaultActionState(StateMachine stateMachine, CharacterController characterController, Animator animator)
        : base(stateMachine, characterController, animator)
    {
        characterState = CharacterState.ExecutingAction;
    }

    public override void Enter()
    {
        animator.SetBool("isExecutingAction", true);
    }
    public override void Exit()
    {
        animator.SetBool("isExecutingAction", false);
    }
}
