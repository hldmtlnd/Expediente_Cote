using UnityEngine;
using System.Collections.Generic;

public class EventDirector : MonoBehaviour
{
    public List<string> dayEvents = new List<string> { "Selfie", "ExteriorLights", "AlienPeek", "StrangeLight" };
    public List<string> nightEvents = new List<string> { "PowerCut", "OrbsPursuit", "AlienInside", "MorePeeks", "Final" };

    public int currentDayIndex = 0;
    public int currentNightIndex = 0;
    public bool isNight = false;

    public DayNightController dayNightController;
    public AlienAI alienAI;
    public AlienPeekManager peekManager;
    public SecurityCameraManager cameraManager;
    public FinalSequenceController finalSequence;

    public void CompleteCurrentEvent()
    {
        if (!isNight)
        {
            currentDayIndex++;
            if (currentDayIndex >= dayEvents.Count)
                StartNight();
            else
                ActivateEvent(dayEvents[currentDayIndex]);
        }
        else
        {
            currentNightIndex++;
            if (currentNightIndex >= nightEvents.Count)
                finalSequence.StartFinal();
            else
                ActivateEvent(nightEvents[currentNightIndex]);
        }
    }

    void StartNight()
    {
        isNight = true;
        dayNightController.TriggerNightTransition();
        cameraManager.SetAllNightVision(true);
        ActivateEvent(nightEvents[0]);
    }

    void ActivateEvent(string eventName)
    {
        Debug.Log("Activating event: " + eventName);
        // Notify relevant systems
    }
}
