using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    private MeshRenderer meshRenderer;
    private Rigidbody rb;

    [Header("Settings")]
    [SerializeField] private Direction direction;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float changeDirectionTime;
    [SerializeField] private bool isMoveable;

    private Vector3 movementDirection;
    private void Start()
    {
        InvokeRepeating(nameof(ChangeDirection), changeDirectionTime, changeDirectionTime);

        InitializeRigidbody();

        if (isMoveable)
            InitialDirection();
    }
    private void InitializeRigidbody()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        
        rb.useGravity = false;
        rb.isKinematic = true;
    }
    public void InitialDirection()
    {
        switch (direction)
        {
            case Direction.Left:
                movementDirection = Vector3.left;
                break;
            case Direction.Right:
                movementDirection = Vector3.right;
                break;
            case Direction.Up:
                movementDirection = Vector3.up;
                break;
            case Direction.Down:
                movementDirection = Vector3.down;
                break;
        }
    }
    private void FixedUpdate()
    {
        if (IsMoveable())
        {
            HandleMovement();
        }
        
    }
    private void HandleMovement()
    {
        rb.MovePosition(transform.position + movementDirection * movementSpeed * Time.fixedDeltaTime);
    }
    private void ChangeDirection()
    {
        var inverseDirection = transform.InverseTransformDirection(movementDirection);
        movementDirection = inverseDirection;
    }
    public void ChangeColorSoftRed()
    {
        meshRenderer.material.color = Color.softRed;
    }
    public bool IsMoveable()
    {
        return isMoveable;
    }
    public void Fall()
    {
        rb.useGravity = true;
    }
}
