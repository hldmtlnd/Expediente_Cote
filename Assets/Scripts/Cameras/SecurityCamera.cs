using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [Header("Angle Limits")]
    public float minPan = -180f;
    public float maxPan = 180f;
    public float minTilt = -45f;
    public float maxTilt = 45f;

    [Header("Movement")]
    public float rotationSpeed = 2f;
    public float zoomFOV = 30f;
    public float normalFOV = 60f;

    public bool isNightVision = false;
    public Transform lookTarget;

    private Camera cam;
    private float currentPan;
    private float currentTilt;

    void Start()
    {
        cam = GetComponent<Camera>();
        currentPan = transform.eulerAngles.y;
        currentTilt = transform.eulerAngles.x;
    }

    void Update()
    {
        if (lookTarget != null)
            LookAtTarget();
    }

    public void LookAtTarget()
    {
        Vector3 direction = lookTarget.position - transform.position;
        Quaternion targetRot = Quaternion.LookRotation(direction);

        float targetPan = targetRot.eulerAngles.y;
        float targetTilt = targetRot.eulerAngles.x;

        currentPan = Mathf.Clamp(Mathf.LerpAngle(currentPan, targetPan, Time.deltaTime * rotationSpeed), minPan, maxPan);
        currentTilt = Mathf.Clamp(Mathf.LerpAngle(currentTilt, targetTilt, Time.deltaTime * rotationSpeed), minTilt, maxTilt);

        transform.rotation = Quaternion.Euler(currentTilt, currentPan, 0f);
    }

    public void SetNightVision(bool active)
    {
        isNightVision = active;
        // Apply post-processing or material change
    }

    public void DoZoom(bool zoom)
    {
        cam.fieldOfView = zoom ? zoomFOV : normalFOV;
    }

    public void SetLookTarget(Transform target)
    {
        lookTarget = target;
    }
}
