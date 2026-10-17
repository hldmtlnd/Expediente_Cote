using UnityEngine;

public class RevolverController : MonoBehaviour
{
    public int chamberSize = 6;
    public int reserveAmmo = 50;
    public int currentChamber = 6;

    public float damage = 100f;
    public float range = 20f;

    public bool isEquipped = false;

    void Update()
    {
        if (!isEquipped) return;

        if (Input.GetMouseButtonDown(0) && currentChamber > 0)
            Shoot();

        if (Input.GetKeyDown(KeyCode.R))
            Reload();
    }

    void Shoot()
    {
        currentChamber--;
        // Raycast for damage
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            var damageable = hit.collider.GetComponent<AlienDamageable>();
            damageable?.TakeDamage(damage);
        }
        // Play sound and muzzle flash
    }

    void Reload()
    {
        int needed = chamberSize - currentChamber;
        int toLoad = Mathf.Min(needed, reserveAmmo);
        currentChamber += toLoad;
        reserveAmmo -= toLoad;
    }
}
