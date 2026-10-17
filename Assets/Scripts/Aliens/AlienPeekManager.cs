using UnityEngine;
using System.Collections.Generic;

public class AlienPeekManager : MonoBehaviour
{
    public List<Transform> peekPoints = new List<Transform>();
    public GameObject alienPrefab;

    public void ActivatePeek(int index)
    {
        if (index < 0 || index >= peekPoints.Count) return;
        // Spawn or activate alien at peek point
    }

    public void ActivateMultiplePeeks(int count)
    {
        // For night phase
    }

    public void ClearAllPeeks()
    {
        // Remove active peeks
    }
}
