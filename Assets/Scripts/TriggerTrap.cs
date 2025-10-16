using UnityEngine;

public class TriggerTrap : MonoBehaviour
{
    public EventChannel trapEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<PlayerController>())
            return;

        trapEvent.Invoke(new Empty());
    }

    public void DestroyTrigger()
    {
        gameObject.SetActive(false);
    }
}
