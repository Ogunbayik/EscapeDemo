using System.Collections;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public enum MovementType
    {
        None,
        OneWay,
        Straight,
        PingPong,
        Follow
    }
    public enum MovementDirection
    {
        Forward,
        Up,
        Right,
        Down
    }

    [Header("Movement Settings")]
    [SerializeField] protected MovementDirection direction;
    [SerializeField] protected MovementType movementType;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float moveRange;
    [SerializeField] protected float waitTime;
    [SerializeField] protected bool isWaitable;

    private Vector3 movementDirection;
    private Vector3 movePosition;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private float waitTimer = 0;

    public virtual void Start()
    {
        InitialSetDirection();
    }
    private void InitialSetDirection()
    {
        startPosition = transform.position;
        switch (direction)
        {
            case MovementDirection.Forward:
                movementDirection = Vector3.forward;
                endPosition.Set(transform.position.x, transform.position.y, transform.position.z + moveRange);
                break;
            case MovementDirection.Up:
                movementDirection = Vector3.up;
                endPosition.Set(transform.position.x, transform.position.y + moveRange, transform.position.z);
                break;
            case MovementDirection.Right:
                movementDirection = Vector3.right;
                endPosition.Set(transform.position.x + moveRange, transform.position.y, transform.position.z);
                break;
            case MovementDirection.Down:
                movementDirection = Vector3.down;
                endPosition.Set(transform.position.x, transform.position.y - moveRange, transform.position.z);
                break;
        }

        movePosition = endPosition;
    }
    public virtual void Update()
    {
        HandleMovement();

    }
    protected virtual void HandleMovement()
    {
        switch(movementType)
        {
            case MovementType.None:
                //Object wait trigger for any different moves.
                break;
            case MovementType.OneWay:
                transform.position = Vector3.MoveTowards(transform.position, endPosition, movementSpeed * Time.deltaTime);
                break;
            case MovementType.Straight:
                transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
                break;
            case MovementType.PingPong:
                var distanceToPoint = Vector3.Distance(transform.position, movePosition);

                if(distanceToPoint <= 0)
                {
                    if (isWaitable)
                    {
                        //Wait and change direction
                        transform.position = movePosition;
                        waitTimer += Time.deltaTime;

                        if (waitTimer >= waitTime)
                        {
                            SetOnewayMovementPosition();
                            waitTimer = 0;
                        }
                    }
                    else
                        SetOnewayMovementPosition();

                }

                transform.position = Vector3.MoveTowards(transform.position, movePosition, movementSpeed * Time.deltaTime);
                break;
            case MovementType.Follow:
                break;

        }
    }

    private void SetOnewayMovementPosition()
    {
        if (movePosition == endPosition)
            movePosition = startPosition;
        else
            movePosition = endPosition;
    }

    //---------------------------------------------------------MOVEABLE BEHAVÝOURS----------------------------------------------------------
    protected virtual void StartStraightMovement()
    {
        movementType = MovementType.Straight;
    }
    protected virtual void StartPingPongMovement()
    {
        movementType = MovementType.PingPong;
    }
    protected virtual void StartOnewayMovement()
    {
        movementType = MovementType.OneWay;
    }

}
