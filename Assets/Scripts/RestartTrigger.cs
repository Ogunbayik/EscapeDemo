using UnityEngine;

public class RestartTrigger : MonoBehaviour
{
    public EventChannel restartEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<PlayerController>())
            return;

        restartEvent.Invoke(new Empty());
    }
}
