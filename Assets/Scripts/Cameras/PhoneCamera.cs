using UnityEngine;

public class PhoneCamera : MonoBehaviour
{
    public Camera phoneCam;
    public float minZoom = 0.5f;
    public float maxZoom = 5f;
    public bool isRecording = false;
    public bool flashOn = false;

    public DeviceBattery battery;

    void Start()
    {
        if (phoneCam == null)
            phoneCam = GetComponent<Camera>();
    }

    void Update()
    {
        if (!enabled) return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
            AdjustZoom(scroll);

        if (Input.GetMouseButtonDown(0))
            ToggleRecording();

        if (Input.GetMouseButtonDown(1))
            ToggleFlash();
    }

    void AdjustZoom(float delta)
    {
        if (phoneCam == null) return;
        phoneCam.fieldOfView = Mathf.Clamp(phoneCam.fieldOfView - delta * 10f, 10f, 80f);
    }

    void ToggleRecording()
    {
        isRecording = !isRecording;
        // Start/stop recording logic
    }

    void ToggleFlash()
    {
        if (battery == null) return;
        if (battery.GetPercentage() <= 10f) return;
        flashOn = !flashOn;
        // Enable/disable light
    }
}
