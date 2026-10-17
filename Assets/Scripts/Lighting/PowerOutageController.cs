using UnityEngine;

public class PowerOutageController : MonoBehaviour
{
    public HouseLightingController lighting;
    public SecurityCameraManager cameraManager;
    public ChargingStation chargingStation;

    public void ExecuteOutage()
    {
        lighting?.CutPower();
        cameraManager?.SetAllNightVision(true);

        if (chargingStation != null)
            chargingStation.enabled = false;

        // Flicker effect optional
        Debug.Log("Corte de luz ejecutado.");
    }
}
