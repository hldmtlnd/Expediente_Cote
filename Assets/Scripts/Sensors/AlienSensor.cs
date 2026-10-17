using UnityEngine;

public class AlienSensor : MonoBehaviour
{
    public Transform player;
    public float awarenessRange = 15f;

    public bool playerIsHidden = false;
    public bool playerIsRecording = false;

    public float GetDistanceToPlayer()
    {
        if (player == null) return float.MaxValue;
        return Vector3.Distance(transform.position, player.position);
    }

    public bool CanSeePlayer()
    {
        if (playerIsHidden) return false;
        return GetDistanceToPlayer() < awarenessRange;
    }

    public void OnPlayerInteracted(Vector3 interactionPoint)
    {
        // Prioritize this location
    }
}
