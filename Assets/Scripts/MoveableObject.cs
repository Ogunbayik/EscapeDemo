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
    [SerializeField] protected MovementType movementType;
    [SerializeField] protected MovementDirection direction;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float moveRange;
    [SerializeField] protected float waitTime;
    [SerializeField] protected bool isWaitable;
    [Header("Event Trigger")]
    [SerializeField] private Transform eventTrigger;

    private MovementType initialType;
    private bool initialTriggerActivate;
    private Vector3 initialPosition;
    private Vector3 movementDirection;
    private Vector3 movePosition;
    private Vector3 endPosition;

    private float waitTimer = 0;

    public virtual void Start()
    {
        InitialSetDirection();
        InitialStateSaver();
    }
    private void InitialSetDirection()
    {
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
    }
    private void InitialStateSaver()
    {
        initialPosition = transform.position;
        initialType = movementType;
        initialTriggerActivate = eventTrigger.gameObject.activeInHierarchy;
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
            movePosition = initialPosition;
        else
            movePosition = endPosition;
    }

    public void Restart()
    {
        transform.position = initialPosition;
        movementType = initialType;
        eventTrigger.gameObject.SetActive(initialTriggerActivate);
    }
    //---------------------------------------------------------MOVEABLE BEHAVÝOURS----------------------------------------------------------
    public void StartStraightMovement()
    {
        movementType = MovementType.Straight;
    }
    public void StartPingPongMovement()
    {
        movementType = MovementType.PingPong;
    }
    public void StartOnewayMovement()
    {
        //Use that for activate traps
        movementType = MovementType.OneWay;
    }

}
