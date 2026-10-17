using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    public DeviceBattery phoneBattery;
    public DeviceBattery cameraBattery;

    public float phoneBaseDrain = 50f; // per minute
    public float cameraBaseDrain = 25f;

    void Update()
    {
        float delta = Time.deltaTime / 60f; // to minutes

        if (phoneBattery != null)
            phoneBattery.Drain(phoneBaseDrain * delta);

        if (cameraBattery != null)
            cameraBattery.Drain(cameraBaseDrain * delta);
    }

    public void DrainAll()
    {
        if (phoneBattery != null) phoneBattery.SetCharge(0);
        if (cameraBattery != null) cameraBattery.SetCharge(0);
    }
}
