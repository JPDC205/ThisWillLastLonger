using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public enum CharacterState
{
    Idle,
    Walking,
    Running,
    ExecutingAction
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
        inputActions.Player.Action.performed += OnAction;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Action.performed -= OnAction;
        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (!CanMove()) return;
        
        if (moveInput != Vector2.zero)
        {
            stateMachine.GoWalking();
        }
        else
        {
            stateMachine.GoIdle();
        }
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        // Handle action input (e.g., attack, interact)
        Debug.Log("Action performed!");
        stateMachine.GoExecutingAction();
        Tile tileUnderMouse = GetTileUnderMouse();
        if (tileUnderMouse != null)
        {
            tileUnderMouse.Interact();
        }
    }

    // Update is called once per frame
    void Update()
    {
       switch (stateMachine.GetState)
       {
                
            case CharacterState.Walking:
             ManageMovement();
                    break;
            case CharacterState.ExecutingAction:
            case CharacterState.Idle:
            case CharacterState.Running:
                 break;
        }
    }

    void ManageMovement()
    {
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0) * moveSpeed * Time.deltaTime;
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
        }
    }

    private bool CanMove()
    {
        return stateMachine.GetState == CharacterState.Idle || stateMachine.GetState == CharacterState.Walking;
    }

    public void FinishAction()
    {
        // This method can be called by animation events to signal the end of an action
        Debug.Log("Action finished!");
        stateMachine.GoIdle();
    }

    public Tile GetTileUnderMouse()
    {
        // Get mouse position using Input System
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        // Convert to world coordinates - try different approaches
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, Camera.main.transform.position.z * -1));
        mouseWorldPos.z = 0;

        Tile tile = MapManager._instance.GetTileAtWorldPosition(mouseWorldPos);

        return tile;
    }
}
