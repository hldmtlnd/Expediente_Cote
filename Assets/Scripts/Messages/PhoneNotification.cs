using UnityEngine;

public class PhoneNotification : MonoBehaviour
{
    public GameObject notificationPrefab;
    public Transform notificationParent;

    public void ShowNotification(string title, string message, NotificationType type)
    {
        // Instantiate and show banner
        Debug.Log($"[{type}] {title}: {message}");
    }

    public void ShowCameraAlert(string roomName)
    {
        ShowNotification("Cámara", $"Movimiento detectado en {roomName}", NotificationType.Camera);
    }

    public void ShowLowBattery()
    {
        ShowNotification("Batería", "Batería baja", NotificationType.Battery);
    }
}

public enum NotificationType
{
    Message,
    Battery,
    Camera,
    System
}
