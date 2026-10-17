using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public Light directionalLight;
    public Material skyboxDay;
    public Material skyboxNight;

    public void TriggerNightTransition()
    {
        // Gradually darken, then snap to night
        if (directionalLight != null)
            directionalLight.intensity = 0.05f;

        RenderSettings.skybox = skyboxNight;
        // Set clocks to 03:33:33
    }

    public void SetTime(float hour)
    {
        // Rotate sun based on time
    }
}
