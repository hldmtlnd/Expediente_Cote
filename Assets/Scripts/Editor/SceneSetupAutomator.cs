using UnityEngine;
using UnityEditor;
using System.Linq;

public class SceneSetupAutomator : EditorWindow
{
    [MenuItem("Expediente/Setup Test Scene")]
    public static void SetupScene()
    {
        // Find or create managers
        CreateManager<EventDirector>("EventDirector");
        CreateManager<SecurityCameraManager>("SecurityCameraManager");
        CreateManager<BatteryManager>("BatteryManager");
        CreateManager<AlienAI>("AlienAI");

        // Setup Player
        var player = GameObject.Find("Player");
        if (player != null)
        {
            if (player.GetComponent<PlayerMovement>() == null)
                player.AddComponent<PlayerMovement>();
            if (player.GetComponent<PlayerInteraction>() == null)
                player.AddComponent<PlayerInteraction>();
            if (player.GetComponent<PlayerInventory>() == null)
                player.AddComponent<PlayerInventory>();
            if (player.GetComponent<CharacterController>() == null)
                player.AddComponent<CharacterController>();
        }

        // Setup cameras in hierarchy
        var secCams = GameObject.FindObjectsOfType<Camera>().Where(c => c.gameObject.name.Contains("SEC") || c.gameObject.name.Contains("Security"));
        foreach (var cam in secCams)
        {
            if (cam.GetComponent<SecurityCamera>() == null)
                cam.gameObject.AddComponent<SecurityCamera>();
        }

        Debug.Log("Test scene setup complete. Review components and assign references.");
    }

    static T CreateManager<T>(string name) where T : Component
    {
        var existing = GameObject.Find(name);
        if (existing == null)
        {
            var go = new GameObject(name);
            return go.AddComponent<T>();
        }
        return existing.GetComponent<T>() ?? existing.AddComponent<T>();
    }
}
