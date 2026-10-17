using UnityEngine;

public class WindowController : MonoBehaviour, IInteractable
{
    public bool isOpen = false;
    public Transform peekPoint;

    public void Interact(PlayerInteraction player)
    {
        isOpen = !isOpen;
        // Animate or move window part
    }

    public void SetOpen(bool open)
    {
        isOpen = open;
    }
}
