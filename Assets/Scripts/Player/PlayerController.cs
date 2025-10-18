using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerCollision collision;
    private PlayerStateController stateController;

    private Rigidbody rb;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [Header("Visual Settings")]
    [SerializeField] private GameObject characterVisual;

    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityMultiplier;

    private float horizontalInput;
    private float verticalInput;
    private float gravityMagnitude;

    private Vector3 movementDirection;

    private bool canSecondJump = false;
    private void Awake()
    {
        collision = GetComponent<PlayerCollision>();
        stateController = GetComponent<PlayerStateController>();

        rb = GetComponent<Rigidbody>();
        gravityMagnitude = Physics.gravity.y;
    }
    private void Update()
    {
        SetStates();
        HandleJump();
        SetMovementDirection();
    }
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && collision.IsGround())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canSecondJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canSecondJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canSecondJump = false;
        }
    }
    private void SetStates()
    {
        if (collision.IsGround())
        {
            if (IsMoving())
                stateController.SwitchState(PlayerStateController.States.Move);
            else
                stateController.SwitchState(PlayerStateController.States.Idle);
        }
        else
        {
            if (rb.linearVelocity.y > 0)
                stateController.SwitchState(PlayerStateController.States.Jump);
            else if (rb.linearVelocity.y < 0)
                stateController.SwitchState(PlayerStateController.States.Fall);

        }
    }
    void FixedUpdate()
    {
        if (stateController.currentState == PlayerStateController.States.Move 
            || stateController.currentState == PlayerStateController.States.Jump
            || stateController.currentState == PlayerStateController.States.Fall)
        {
            HandleMovement();
            HandleRotation();
        }
    }
    private void SetMovementDirection()
    {
        horizontalInput = Input.GetAxis(Const.PlayerInput.HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(Const.PlayerInput.VERTICAL_INPUT);

        movementDirection.Set(horizontalInput, 0f, verticalInput);
        movementDirection.Normalize();
    }
    private void HandleMovement()
    {
        rb.MovePosition(transform.position + movementDirection * movementSpeed * Time.fixedDeltaTime);
    }
    private void HandleRotation()
    {
        if (IsMoving())
        {
            var desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            characterVisual.transform.rotation = Quaternion.RotateTowards(characterVisual.transform.rotation, desiredRotation, rotationSpeed);
        }
    }
    private bool IsMoving()
    {
        return movementDirection != Vector3.zero;
    }
    
}
