using System;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public UnityEvent triggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCollision>())
            return;

        triggerEvent?.Invoke();
        SetTriggerDeactive();
    }
    public void SetTriggerDeactive()
    {
        this.gameObject.SetActive(false);
    }
}
