using UnityEngine;

public class PhoneUI : MonoBehaviour
{
    public GameObject phonePanel;
    public GameObject menuPanel;
    public GameObject[] appPanels;

    public void OpenPhone()
    {
        phonePanel.SetActive(true);
        ShowMenu();
    }

    public void ClosePhone()
    {
        phonePanel.SetActive(false);
    }

    public void ShowMenu()
    {
        foreach (var panel in appPanels)
            panel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void OpenApp(int index)
    {
        if (index < 0 || index >= appPanels.Length) return;
        menuPanel.SetActive(false);
        appPanels[index].SetActive(true);
    }

    public void OnEscape()
    {
        // If in app, go to menu; if in menu, close phone
        bool anyAppOpen = false;
        foreach (var panel in appPanels)
            if (panel.activeSelf) anyAppOpen = true;

        if (anyAppOpen)
            ShowMenu();
        else
            ClosePhone();
    }
}
