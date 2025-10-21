using UnityEngine;

public class Platform : MonoBehaviour
{
    public enum Direction
    {
        None,
        Forward,
        Right,
        Up
    }
    [Header("Movement Settings")]
    [SerializeField] private bool isMoveable;
    [SerializeField] private Direction direction;
    [SerializeField] private float movementSpeed;
    
    private Vector3 movementDirection;
    void Start()
    {
        InitialDirection();
    }

    private void InitialDirection()
    {
        switch (direction)
        {
            case Direction.None:
                //Platform is not moving
                break;
            case Direction.Forward:
                movementDirection = Vector3.forward;
                break;
            case Direction.Right:
                movementDirection = Vector3.right;
                break;
            case Direction.Up:
                movementDirection = Vector3.up;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (isMoveable)
            HandleMovement();
    }
    private void HandleMovement()
    {
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
    }
    //-----------------------------------------------PLATFORM BEHAVÝOURS--------------------------------------------------
    public void StartMovement()
    {
        isMoveable = true;
    }
    public void ChangeDirection()
    {
        var reverseDirection = -movementDirection;
        movementDirection = reverseDirection;
    }
    public void ActivateSpikeTrap()
    {
        Debug.Log("Active Spike Trap");
    }



    
}
