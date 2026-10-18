using UnityEngine;
using UnityEditor;

public class PhysicsCleanup : EditorWindow
{
    [MenuItem("Expediente/Cleanup Bad Rigidbodies")]
    public static void Cleanup()
    {
        int removed = 0;

        var allRBs = Object.FindObjectsByType<Rigidbody>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var rb in allRBs)
        {
            var go = rb.gameObject;
            string path = GetFullPath(go).ToLower();

            bool isStructural = path.Contains("foundation") || path.Contains("floor") ||
                                path.Contains("wall") || path.Contains("ceiling") ||
                                path.Contains("celling") || path.Contains("terrain") ||
                                path.Contains("concrete") || path.Contains("plane") ||
                                path.Contains("stair") || path.Contains("player") ||
                                path.Contains("secam") || path.Contains("segcam") ||
                                path.Contains("alien") || path.Contains("huguito") ||
                                path.Contains("mutilation") || path.Contains("habitaciones") ||
                                path.Contains("telefono") || path.Contains("advanced camera") ||
                                path.Contains("rope") || path.Contains("vase") ||
                                path.Contains("handle") || path.Contains("glass") ||
                                path.Contains("window") || path.Contains("door") ||
                                path.Contains("button") || path.Contains("screw") ||
                                path.Contains("lidar") || path.Contains("usb") ||
                                path.Contains("mountain");

            // También revisar MeshCollider cóncavo
            var meshCol = go.GetComponent<MeshCollider>();
            bool badMesh = meshCol != null && !meshCol.convex && !rb.isKinematic;

            if (isStructural || badMesh)
            {
                Object.DestroyImmediate(rb);
                // También quitar PropImpact si se agregó
                var impact = go.GetComponent<PropImpact>();
                if (impact != null)
                    Object.DestroyImmediate(impact);

                removed++;
            }
        }

        Debug.Log($"[Expediente] Cleanup complete. Removed {removed} bad Rigidbodies.");
    }

    static string GetFullPath(GameObject go)
    {
        string path = go.name;
        Transform t = go.transform.parent;
        while (t != null)
        {
            path = t.name + "/" + path;
            t = t.parent;
        }
        return path;
    }
}
