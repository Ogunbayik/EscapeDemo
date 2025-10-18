using UnityEngine;
using System;

public class PlayerStateController : MonoBehaviour
{
    public event Action<States> OnStateChanged;

    public enum States
    {
        Idle,
        Move,
        Jump,
        Fall,
    }

    public States currentState;

    private void Start()
    {
        currentState = States.Idle;
    }

    public void SwitchState(States newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;
        OnStateChanged?.Invoke(newState);

        Debug.Log(currentState);
    }


}
