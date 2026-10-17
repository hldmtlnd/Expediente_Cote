using UnityEngine;

public class CameraCut : MonoBehaviour
{
    public Camera[] allCameras;
    public GameObject blackScreen;

    public void CutAll()
    {
        foreach (var cam in allCameras)
        {
            if (cam != null)
                cam.enabled = false;
        }

        if (blackScreen != null)
            blackScreen.SetActive(true);

        // Stop audio
        AudioManager.Instance?.StopAll();

        // End game
        Invoke(nameof(EndExpediente), 2f);
    }

    void EndExpediente()
    {
        // Show final screen or quit
        Debug.Log("Expediente cerrado.");
    }
}
