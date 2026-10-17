using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public string eventName;
    public bool triggerOnce = true;
    public bool hasTriggered = false;

    public UnityEvent onTrigger;

    public EventDirector director;

    void OnTriggerEnter(Collider other)
    {
        if (hasTriggered && triggerOnce) return;
        if (!other.CompareTag("Player")) return;

        hasTriggered = true;
        onTrigger?.Invoke();
        director?.CompleteCurrentEvent();
    }

    public void ManualTrigger()
    {
        if (hasTriggered && triggerOnce) return;
        hasTriggered = true;
        onTrigger?.Invoke();
        director?.CompleteCurrentEvent();
    }
}
