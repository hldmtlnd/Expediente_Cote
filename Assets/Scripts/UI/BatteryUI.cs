using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{
    public Image batteryFill;
    public Text batteryText;
    public DeviceBattery targetBattery;

    void Update()
    {
        if (targetBattery == null) return;

        float percent = targetBattery.GetPercentage();
        if (batteryFill != null)
            batteryFill.fillAmount = percent / 100f;

        if (batteryText != null)
            batteryText.text = Mathf.RoundToInt(percent) + "%";

        // Change color at thresholds
        if (percent <= 10f)
            batteryFill.color = Color.red;
        else if (percent <= 25f)
            batteryFill.color = Color.yellow;
        else
            batteryFill.color = Color.green;
    }

    public void SetTarget(DeviceBattery battery)
    {
        targetBattery = battery;
    }
}
