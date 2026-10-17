using UnityEngine;

public class FinalSequenceController : MonoBehaviour
{
    public BatteryManager batteryManager;
    public SecurityCameraManager cameraManager;
    public FinalPursuit pursuit;
    public CameraCut cameraCut;

    public void StartFinal()
    {
        batteryManager.DrainAll();
        // Disable phone and advanced camera
        pursuit.StartPursuit();
    }

    public void OnPlayerCaught()
    {
        cameraCut.CutAll();
    }
}
