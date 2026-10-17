using UnityEngine;
using System.Collections.Generic;

public class HouseLightingController : MonoBehaviour
{
    public List<Light> houseLights = new List<Light>();
    public bool powerOn = true;

    public void CutPower()
    {
        powerOn = false;
        foreach (var light in houseLights)
        {
            if (light != null)
                light.enabled = false;
        }
    }

    public void RestorePower()
    {
        powerOn = true;
        // Optionally turn lights back on
    }

    public void ToggleLight(Light light)
    {
        if (!powerOn || light == null) return;
        light.enabled = !light.enabled;
    }
}
