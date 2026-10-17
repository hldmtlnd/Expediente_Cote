using UnityEngine;
using System.Collections.Generic;

public class HidingManager : MonoBehaviour
{
    public List<HidingSpot> hidingSpots = new List<HidingSpot>();
    public bool playerIsHidden = false;
    public HidingSpot currentSpot;

    public void RegisterSpot(HidingSpot spot)
    {
        if (!hidingSpots.Contains(spot))
            hidingSpots.Add(spot);
    }

    public void SetPlayerHidden(HidingSpot spot, bool hidden)
    {
        playerIsHidden = hidden;
        currentSpot = hidden ? spot : null;
        // Notify AlienAI and cameras
    }
}
