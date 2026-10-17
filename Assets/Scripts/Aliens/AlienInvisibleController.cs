using UnityEngine;

public class AlienInvisibleController : MonoBehaviour
{
    public Transform player;
    public float minDistance = 2f;
    public float maxDistance = 8f;
    public float moveSpeed = 2f;

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist > maxDistance)
            MoveTowardsPlayer();
        else if (dist < minDistance)
            MoveAwayFromPlayer();
    }

    void MoveTowardsPlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void MoveAwayFromPlayer()
    {
        Vector3 dir = (transform.position - player.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    public void ProjectLight(Vector3 position)
    {
        // Spawn temporary light
    }

    public void PlayFootsteps()
    {
        // Audio call
    }
}
