using UnityEngine;

public class SecurityCameraSensor : MonoBehaviour
{
    public float detectionRange = 10f;
    public float detectionAngle = 60f;
    public LayerMask detectionMask;

    public SecurityCamera securityCamera;

    void Update()
    {
        DetectTargets();
    }

    void DetectTargets()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange, detectionMask);
        foreach (var hit in hits)
        {
            Vector3 dir = (hit.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dir);

            if (angle < detectionAngle / 2f)
            {
                if (hit.CompareTag("Player"))
                {
                    securityCamera.SetLookTarget(hit.transform);
                }
                else if (hit.CompareTag("Alien") || hit.CompareTag("Orb"))
                {
                    securityCamera.SetLookTarget(hit.transform);
                    securityCamera.DoZoom(true);
                    // Trigger camera alert
                }
            }
        }
    }
}
