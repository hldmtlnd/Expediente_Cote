using UnityEngine;

public class AlienInteraction : MonoBehaviour
{
    public float interactRange = 2f;

    public void InteractWithNearby()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactRange);
        foreach (var hit in hits)
        {
            var door = hit.GetComponent<DoorController>();
            if (door != null)
            {
                door.Toggle();
                return;
            }

            var window = hit.GetComponent<WindowController>();
            if (window != null)
            {
                window.SetOpen(!window.isOpen);
                return;
            }

            var lightSwitch = hit.GetComponent<LightSwitchController>();
            if (lightSwitch != null)
            {
                lightSwitch.Toggle();
                return;
            }
        }
    }

    public void CloseRecentlyOpened()
    {
        // Logic to reverse player actions
    }
}
