using UnityEngine;

public class HidingSpot : MonoBehaviour, IInteractable
{
    public Transform hidePosition;
    public bool isOccupied = false;

    public void Interact(PlayerInteraction player)
    {
        if (!isOccupied)
            Enter(player.transform);
        else
            Exit(player.transform);
    }

    public void Enter(Transform player)
    {
        isOccupied = true;
        player.position = hidePosition.position;
        // Disable normal movement, notify sensors
    }

    public void Exit(Transform player)
    {
        isOccupied = false;
        // Restore movement
    }
}
