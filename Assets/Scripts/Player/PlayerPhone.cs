using UnityEngine;

public class PlayerPhone : MonoBehaviour
{
    public GameObject phoneUI;
    public bool isPhoneOpen = false;

    public PlayerMovement playerMovement;
    public SecurityCameraManager cameraManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            TogglePhone();

        if (isPhoneOpen && Input.GetKeyDown(KeyCode.Escape))
            HandleEscape();
    }

    public void TogglePhone()
    {
        isPhoneOpen = !isPhoneOpen;
        phoneUI.SetActive(isPhoneOpen);

        if (isPhoneOpen)
        {
            playerMovement.SetFirstPerson(false);
            // Blur background handled by UI
        }
        else
        {
            cameraManager.ReturnToSecurityView();
        }
    }

    void HandleEscape()
    {
        // Close current app or close phone
        // Implemented in PhoneUI
    }

    public void OpenApp(string appName)
    {
        // Switch to specific app
    }
}
