using UnityEngine;
using UnityEditor;
using System.Linq;

public class PhysicsSetupAutomator : EditorWindow
{
    [MenuItem("Expediente/Setup Source Physics")]
    public static void SetupPhysics()
    {
        int count = 0;
        int skipped = 0;

        var allRenderers = Object.FindObjectsByType<MeshRenderer>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        foreach (var renderer in allRenderers)
        {
            var go = renderer.gameObject;

            if (IsStructural(go))
            {
                skipped++;
                continue;
            }

            // Revisar colliders existentes
            var meshCol = go.GetComponent<MeshCollider>();
            if (meshCol != null && !meshCol.convex)
            {
                // MeshCollider cóncavo + Rigidbody dinámico no es válido
                // Opción: hacerlo convex o saltarlo
                meshCol.convex = true;
            }

            // Agregar Rigidbody si no tiene
            var rb = go.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = go.AddComponent<Rigidbody>();
                rb.mass = EstimateMass(go);
                rb.linearDamping = 0.1f;
                rb.angularDamping = 0.5f;
                rb.interpolation = RigidbodyInterpolation.Interpolate;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }

            // Asegurar algún collider
            if (go.GetComponent<Collider>() == null)
            {
                go.AddComponent<BoxCollider>();
            }

            // Physic Material
            var collider = go.GetComponent<Collider>();
            if (collider != null && collider.sharedMaterial == null)
            {
                var mat = new PhysicMaterial("PropMaterial")
                {
                    dynamicFriction = 0.6f,
                    staticFriction = 0.6f,
                    bounciness = 0.1f,
                    frictionCombine = PhysicMaterialCombine.Average,
                    bounceCombine = PhysicMaterialCombine.Average
                };
                collider.sharedMaterial = mat;
            }

            if (go.GetComponent<PropImpact>() == null)
                go.AddComponent<PropImpact>();

            count++;
        }

        // PlayerPickup
        var player = GameObject.Find("Player");
        if (player != null && player.GetComponent<PlayerPickup>() == null)
            player.AddComponent<PlayerPickup>();

        Debug.Log($"[Expediente] Source Physics setup complete.\n" +
                  $"- Props configured: {count}\n" +
                  $"- Structural skipped: {skipped}\n" +
                  $"- PlayerPickup added");
    }

    static bool IsStructural(GameObject go)
    {
        string path = GetFullPath(go).ToLower();

        // Ignorar estructura de la casa, terreno, player, cámaras, alien
        if (path.Contains("foundation") || path.Contains("floor") || path.Contains("wall") ||
            path.Contains("ceiling") || path.Contains("celling") || path.Contains("terrain") ||
            path.Contains("player") || path.Contains("secam") || path.Contains("segcam") ||
            path.Contains("alien") || path.Contains("huguito") || path.Contains("mutilation") ||
            path.Contains("habitaciones") || path.Contains("staticlighting") ||
            path.Contains("probevolume") || path.Contains("debb"))
            return true;

        // Ignorar planos grandes
        if (go.name.ToLower().Contains("plane") || go.name.ToLower().Contains("concrete"))
            return true;

        return false;
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

    static float EstimateMass(GameObject go)
    {
        var renderer = go.GetComponent<Renderer>();
        if (renderer == null) return 1f;

        var size = renderer.bounds.size;
        float volume = size.x * size.y * size.z;
        return Mathf.Clamp(volume * 30f, 0.3f, 30f);
    }
}

public class PropImpact : MonoBehaviour
{
    public float minImpactForce = 2f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (rb == null) return;
        float force = collision.relativeVelocity.magnitude;
        if (force > minImpactForce)
        {
            // Audio impact placeholder
        }
    }
}

public class PlayerPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    public float holdDistance = 2f;
    public float throwForce = 8f;

    private Rigidbody heldObject;
    private Camera cam;

    void Start()
    {
        cam = Camera.main ?? Object.FindAnyObjectByType<Camera>();
    }

    void Update()
    {
        if (cam == null) return;

        if (Input.GetMouseButtonDown(0) && heldObject == null)
            TryPickup();

        if (Input.GetMouseButtonDown(1) && heldObject != null)
            Throw();

        if (heldObject != null)
            HoldObject();
    }

    void TryPickup()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            var rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null && !rb.isKinematic)
            {
                heldObject = rb;
                heldObject.useGravity = false;
                heldObject.linearDamping = 8f;
            }
        }
    }

    void HoldObject()
    {
        if (heldObject == null) return;
        Vector3 targetPos = cam.transform.position + cam.transform.forward * holdDistance;
        Vector3 dir = targetPos - heldObject.position;
        heldObject.linearVelocity = dir * 8f;
    }

    void Throw()
    {
        if (heldObject == null) return;
        heldObject.useGravity = true;
        heldObject.linearDamping = 0.1f;
        heldObject.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
        heldObject = null;
    }

    void OnDisable()
    {
        if (heldObject != null)
        {
            heldObject.useGravity = true;
            heldObject.linearDamping = 0.1f;
            heldObject = null;
        }
    }
}
