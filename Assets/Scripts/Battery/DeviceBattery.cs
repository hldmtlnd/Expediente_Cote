using UnityEngine;
using UnityEngine.Events;

public class DeviceBattery : MonoBehaviour
{
    public float maxCapacity = 5000f;
    public float currentCharge = 5000f;

    public UnityEvent onLowBattery;
    public UnityEvent onCriticalBattery;
    public UnityEvent onFullCharge;

    public void Drain(float amount)
    {
        currentCharge = Mathf.Max(0, currentCharge - amount);
        CheckThresholds();
    }

    public void Charge(float amount)
    {
        currentCharge = Mathf.Min(maxCapacity, currentCharge + amount);
        if (currentCharge >= maxCapacity)
            onFullCharge?.Invoke();
    }

    public void SetCharge(float value)
    {
        currentCharge = Mathf.Clamp(value, 0, maxCapacity);
    }

    public float GetPercentage() => (currentCharge / maxCapacity) * 100f;

    void CheckThresholds()
    {
        float percent = GetPercentage();
        if (percent <= 10f)
            onCriticalBattery?.Invoke();
        else if (percent <= 25f)
            onLowBattery?.Invoke();
    }
}
