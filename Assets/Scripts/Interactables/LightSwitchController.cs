using UnityEngine;

public class LightSwitchController : MonoBehaviour, IInteractable
{
    public Light targetLight;
    public bool isOn = true;

    public void Interact(PlayerInteraction player)
    {
        Toggle();
    }

    public void Toggle()
    {
        isOn = !isOn;
        if (targetLight != null)
            targetLight.enabled = isOn;
    }

    public void SetOn(bool value)
    {
        isOn = value;
        if (targetLight != null)
            targetLight.enabled = isOn;
    }
}
