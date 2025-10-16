using UnityEngine;
using UnityEngine.Events;

public class EventListener<T> : MonoBehaviour
{
    public EventChannel<T> eventChannel;

    public UnityEvent<T> unityEvent;

    private void OnEnable()
    {
        eventChannel.RegisterListener(this);
    }
    private void OnDestroy()
    {
        eventChannel.UnRegisterListener(this);
    }
    public void RaiseEvent(T value)
    {
        unityEvent.Invoke(value);
    }
}

public class EventListener : EventListener<Empty>
{

}
