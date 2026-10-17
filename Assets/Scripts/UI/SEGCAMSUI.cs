using UnityEngine;
using UnityEngine.UI;

public class SEGCAMSUI : MonoBehaviour
{
    public SecurityCameraManager cameraManager;
    public RawImage mainFeed;
    public GameObject gridPanel;
    public GameObject fullViewPanel;

    public void ShowGrid()
    {
        gridPanel.SetActive(true);
        fullViewPanel.SetActive(false);
    }

    public void SelectCamera(int index)
    {
        var cams = cameraManager.GetAllCameras();
        if (index < 0 || index >= cams.Count) return;

        gridPanel.SetActive(false);
        fullViewPanel.SetActive(true);
        // Assign camera render texture to mainFeed
        cameraManager.SwitchToCamera(cams[index]);
    }

    public void ControlSelectedCamera()
    {
        // WASD control while in full view
    }
}
