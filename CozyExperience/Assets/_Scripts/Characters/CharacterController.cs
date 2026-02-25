using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CharacterState
{
    Idle,
    Walking,
    Running
}

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public Animator GetAnimator() => animator;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private @InputSystem_Actions inputActions;
    private Vector2 moveInput;

    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        inputActions = new @InputSystem_Actions();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        stateMachine.GoIdle();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            stateMachine.GoWalking();
        }
        else
        {
            stateMachine.GoIdle();
        }
    }

    // Update is called once per frame
    void Update()
    {
       switch (stateMachine.GetState)
            {
                case CharacterState.Idle:
                 // Idle behavior
                 break;
                case CharacterState.Walking:
                 ManageMovement();
                 break;
                case CharacterState.Running:
                 // Running behavior
                 break;
        }
    }

    void ManageMovement()
    {
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0) * moveSpeed * Time.deltaTime;
        Debug.Log($"MoveX: {movement.x}, MoveY: {movement.y}");
        transform.Translate(movement);

        if (moveInput != Vector2.zero)
        {
            if (moveInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (moveInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }

            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
