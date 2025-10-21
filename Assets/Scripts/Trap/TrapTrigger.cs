using System;
using UnityEngine;
using UnityEngine.Events;

public class TrapTrigger : MonoBehaviour
{
    public UnityEvent unityEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCollision>())
            return;

        unityEvent?.Invoke();
    }
}
