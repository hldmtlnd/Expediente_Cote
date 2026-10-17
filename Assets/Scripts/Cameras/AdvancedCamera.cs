using UnityEngine;

public class AdvancedCamera : MonoBehaviour
{
    public Camera cam;
    public bool nightVisionGreen = false;
    public bool infraredFlash = false;

    public DeviceBattery battery;

    public void Activate()
    {
        gameObject.SetActive(true);
        // Switch render
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void ToggleNightVision()
    {
        if (battery.GetPercentage() <= 10f) return;
        nightVisionGreen = !nightVisionGreen;
        // Apply green night vision post-process
    }

    public void ToggleInfrared()
    {
        infraredFlash = !infraredFlash;
    }
}
