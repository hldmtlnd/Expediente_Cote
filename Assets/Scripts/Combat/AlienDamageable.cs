using UnityEngine;

public class AlienDamageable : MonoBehaviour
{
    public float health = 100f;
    public bool isDead = false;

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        health -= amount;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        // Play death effect, notify systems
        gameObject.SetActive(false);
    }
}
