using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerStateController stateController;

    private Rigidbody rb;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [Header("Visual Settings")]
    [SerializeField] private GameObject characterVisual;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 movementDirection;

    private void Awake()
    {
        stateController = GetComponent<PlayerStateController>();

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        SetStates();

        HandleMovement();
        HandleRotation();
    }

    private void SetStates()
    {
        if (IsMoving())
            stateController.SwitchState(PlayerStateController.States.Move);
        else
            stateController.SwitchState(PlayerStateController.States.Idle);
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis(Const.PlayerInput.HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(Const.PlayerInput.VERTICAL_INPUT);

        movementDirection.Set(horizontalInput, 0f, verticalInput);
        movementDirection.Normalize();

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
