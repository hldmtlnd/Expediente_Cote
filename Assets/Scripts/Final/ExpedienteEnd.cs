using UnityEngine;

public class ExpedienteEnd : MonoBehaviour
{
    public GameObject endScreen;
    public SaveManager saveManager;

    public void CloseExpediente()
    {
        // Mark as complete
        if (endScreen != null)
            endScreen.SetActive(true);

        // Final save
        // saveManager.SaveGame(...);

        Debug.Log("Expediente: María José (Coté) — CERRADO");
    }
}
