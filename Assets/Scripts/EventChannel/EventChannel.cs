using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EventChannel<T> : ScriptableObject
{
    public HashSet<EventListener<T>> eventListeners = new HashSet<EventListener<T>>();

    public void Invoke(T value)
    {
        var listenerCopy = eventListeners.ToArray();

        foreach (var listener in listenerCopy)
        {
            listener.RaiseEvent(value);
        }
    }
    public void RegisterListener(EventListener<T> listener)
    {
        eventListeners.Add(listener);
    }
    public void UnRegisterListener(EventListener<T> listener)
    {
        eventListeners.Remove(listener);
    }
}
public struct Empty { }
[CreateAssetMenu(menuName = "ScriptableObject/Event Empty")]
public class EventChannel : EventChannel<Empty> { };