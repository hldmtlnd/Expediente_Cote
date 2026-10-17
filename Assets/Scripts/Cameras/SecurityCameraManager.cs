using UnityEngine;
using System.Collections.Generic;

public class SecurityCameraManager : MonoBehaviour
{
    public List<SecurityCamera> cameras = new List<SecurityCamera>();
    public SecurityCamera activeCamera;

    public void SwitchToCamera(SecurityCamera cam)
    {
        if (activeCamera != null)
            activeCamera.gameObject.SetActive(false);

        activeCamera = cam;
        if (activeCamera != null)
            activeCamera.gameObject.SetActive(true);
    }

    public void SwitchToRoom(string roomName)
    {
        // Find camera by room and switch
    }

    public void ReturnToSecurityView()
    {
        // Return to nearest or current room camera
        if (activeCamera != null)
            SwitchToCamera(activeCamera);
    }

    public void SetAllNightVision(bool active)
    {
        foreach (var cam in cameras)
            cam.SetNightVision(active);
    }

    public List<SecurityCamera> GetAllCameras() => cameras;
}
