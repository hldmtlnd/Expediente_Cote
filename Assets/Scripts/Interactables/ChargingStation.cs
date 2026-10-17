using UnityEngine;
using System.Collections;

public class ChargingStation : MonoBehaviour
{
    public float chargeTime = 213f; // 3 min 33 sec
    public bool isCharging = false;

    private DeviceBattery currentDevice;

    public void StartCharging(DeviceBattery device)
    {
        if (isCharging || device == null) return;
        currentDevice = device;
        StartCoroutine(ChargeRoutine());
    }

    IEnumerator ChargeRoutine()
    {
        isCharging = true;
        float elapsed = 0f;

        while (elapsed < chargeTime)
        {
            elapsed += Time.deltaTime;
            // Update progress UI if needed
            yield return null;
        }

        currentDevice.SetCharge(currentDevice.maxCapacity);
        currentDevice.onFullCharge?.Invoke();
        isCharging = false;
        currentDevice = null;
        // Play completion sound
    }
}
