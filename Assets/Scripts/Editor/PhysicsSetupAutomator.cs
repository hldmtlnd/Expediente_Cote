using UnityEngine;
using UnityEditor;
using System.Linq;

public class PhysicsSetupAutomator : EditorWindow
{
    [MenuItem("Expediente/Setup Source Physics")]
    public static void SetupPhysics()
    {
        int count = 0;

        // Buscar objetos que puedan ser props (tienen MeshRenderer o MeshCollider y no son terreno/estructura)
        var allRenderers = Object.FindObjectsByType<MeshRenderer>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        foreach (var renderer in allRenderers)
        {
            var go = renderer.gameObject;

            // Ignorar si es parte de la estructura principal o del player
            if (IsStructural(go)) continue;

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

            // Asegurar collider
            if (go.GetComponent<Collider>() == null)
            {
                var col = go.AddComponent<BoxCollider>();
                // Ajustar tamaño aproximado
            }

            // Physic Material básico
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

            // Agregar script de impacto si no lo tiene
            if (go.GetComponent<PropImpact>() == null)
                go.AddComponent<PropImpact>();

            count++;
        }

        // Crear el sistema de pickup en el player si no existe
        var player = GameObject.Find("Player");
        if (player != null && player.GetComponent<PlayerPickup>() == null)
        {
            player.AddComponent<PlayerPickup>();
        }

        Debug.Log($"[Expediente] Source Physics setup complete.\n" +
                  $"- Props configured: {count}\n" +
                  $"- PlayerPickup added to Player");
    }

    static bool IsStructural(GameObject go)
    {
        string name = go.name.ToLower();
        string parentName = go.transform.parent != null ? go.transform.parent.name.ToLower() : "";

        // Ignorar pisos, paredes, techos, terreno, player, cámaras
        if (name.Contains("floor") || name.Contains("wall") || name.Contains("ceiling") ||
            name.Contains("celling") || name.Contains("terrain") || name.Contains("foundation") ||
            name.Contains("player") || name.Contains("secam") || name.Contains("segcam") ||
            name.Contains("camera") || parentName.Contains("foundation") || parentName.Contains("walls"))
            return true;

        return false;
    }

    static float EstimateMass(GameObject go)
    {
        var renderer = go.GetComponent<Renderer>();
        if (renderer == null) return 1f;

        var size = renderer.bounds.size;
        float volume = size.x * size.y * size.z;
        return Mathf.Clamp(volume * 50f, 0.5f, 50f); // masa aproximada
    }
}

// Script de impacto para props
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
            // Aquí se puede reproducir sonido de impacto
            // AudioManager.Instance?.PlaySFX(impactClip);
        }
    }
}

// Sistema de pickup tipo Source
public class PlayerPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    public float holdDistance = 2f;
    public float throwForce = 10f;
    public LayerMask propLayer;

    private Rigidbody heldObject;
    private Camera cam;

    void Start()
    {
        cam = Camera.main ?? FindAnyObjectByType<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && heldObject == null)
            TryPickup();

        if (Input.GetMouseButtonDown(1) && heldObject != null)
            Throw();

        if (heldObject != null)
            HoldObject();
    }

    void TryPickup()
    {
        if (cam == null) return;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            var rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null && !rb.isKinematic)
            {
                heldObject = rb;
                heldObject.useGravity = false;
                heldObject.linearDamping = 10f;
            }
        }
    }

    void HoldObject()
    {
        if (cam == null || heldObject == null) return;

        Vector3 targetPos = cam.transform.position + cam.transform.forward * holdDistance;
        Vector3 dir = targetPos - heldObject.position;
        heldObject.linearVelocity = dir * 10f;
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
