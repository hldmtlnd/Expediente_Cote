using UnityEngine;

public class AlienEntry : MonoBehaviour
{
    public Transform entryPoint;
    public Transform[] patrolPoints;
    public float moveSpeed = 1.5f;

    public bool isInside = false;
    public AlienDamageable damageable;

    public void EnterHouse()
    {
        isInside = true;
        transform.position = entryPoint.position;
        // Start patrol or pursue
    }

    public void PursuePlayer(Transform player)
    {
        if (!isInside || damageable.isDead) return;
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.LookAt(player);
    }

    public void OnDeath()
    {
        isInside = false;
        // Notify EventDirector
    }
}
