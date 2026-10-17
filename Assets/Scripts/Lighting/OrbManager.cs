using UnityEngine;
using System.Collections.Generic;

public class OrbManager : MonoBehaviour
{
    public GameObject orbPrefab;
    public List<Transform> spawnPoints = new List<Transform>();
    public int maxOrbs = 5;

    private List<GameObject> activeOrbs = new List<GameObject>();

    public void StartNightOrbs()
    {
        for (int i = 0; i < maxOrbs && i < spawnPoints.Count; i++)
        {
            SpawnOrb(spawnPoints[i].position);
        }
    }

    public void SpawnOrb(Vector3 position)
    {
        if (orbPrefab == null) return;
        var orb = Instantiate(orbPrefab, position, Quaternion.identity);
        activeOrbs.Add(orb);
    }

    public void ClearOrbs()
    {
        foreach (var orb in activeOrbs)
            if (orb != null) Destroy(orb);
        activeOrbs.Clear();
    }
}
