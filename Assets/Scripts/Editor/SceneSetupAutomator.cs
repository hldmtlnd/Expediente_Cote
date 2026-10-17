using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class SceneSetupAutomator : EditorWindow
{
    [MenuItem("Expediente/Setup Test Scene")]
    public static void SetupScene()
    {
        // === MANAGERS ===
        var eventDirector = CreateManager<EventDirector>("EventDirector");
        var cameraManager = CreateManager<SecurityCameraManager>("SecurityCameraManager");
        var batteryManager = CreateManager<BatteryManager>("BatteryManager");
        var audioManager = CreateManager<AudioManager>("AudioManager");

        // === PLAYER ===
        var player = GameObject.Find("Player");
        if (player == null)
        {
            player = new GameObject("Player");
            player.tag = "Player";
        }

        // Core components
        var cc = GetOrAdd<CharacterController>(player);
        cc.height = 1.8f;
        cc.radius = 0.3f;
        cc.center = new Vector3(0, 0.9f, 0);

        var movement = GetOrAdd<PlayerMovement>(player);
        var interaction = GetOrAdd<PlayerInteraction>(player);
        var inventory = GetOrAdd<PlayerInventory>(player);
        var phone = GetOrAdd<PlayerPhone>(player);

        // First person camera for phone
        var fpCamObj = player.transform.Find("FirstPersonCamera");
        if (fpCamObj == null)
        {
            var fpCamGO = new GameObject("FirstPersonCamera");
            fpCamGO.transform.SetParent(player.transform);
            fpCamGO.transform.localPosition = new Vector3(0, 1.6f, 0);
            var fpCam = fpCamGO.AddComponent<Camera>();
            fpCam.enabled = false;
            fpCamObj = fpCamGO.transform;
        }
        movement.firstPersonCamera = fpCamObj;

        // Phone UI placeholder
        var phoneUI = GameObject.Find("PhoneUI");
        if (phoneUI == null)
        {
            phoneUI = new GameObject("PhoneUI");
            phoneUI.AddComponent<PhoneUI>();
        }
        phone.phoneUI = phoneUI;
        phone.playerMovement = movement;
        phone.cameraManager = cameraManager;

        // === SECURITY CAMERAS ===
        var allCams = Object.FindObjectsOfType<Camera>();
        var securityCams = new List<SecurityCamera>();

        foreach (var cam in allCams)
        {
            string n = cam.gameObject.name.ToUpper();
            if (n.Contains("SEC") || n.Contains("SECURITY") || n.Contains("CAM"))
            {
                var secCam = GetOrAdd<SecurityCamera>(cam.gameObject);
                var sensor = GetOrAdd<SecurityCameraSensor>(cam.gameObject);
                sensor.securityCamera = secCam;

                // Default angle limits
                secCam.minPan = -90f;
                secCam.maxPan = 90f;
                secCam.minTilt = -30f;
                secCam.maxTilt = 45f;

                securityCams.Add(secCam);

                // Disable by default (manager will activate)
                cam.enabled = false;
            }
        }

        cameraManager.cameras = securityCams;
        if (securityCams.Count > 0)
        {
            cameraManager.activeCamera = securityCams[0];
            securityCams[0].GetComponent<Camera>().enabled = true;
        }

        // === PHONE CAMERA ===
        var phoneCamObj = GameObject.Find("PhoneCamera");
        if (phoneCamObj == null)
        {
            phoneCamObj = new GameObject("PhoneCamera");
            var pc = phoneCamObj.AddComponent<Camera>();
            pc.enabled = false;
            phoneCamObj.AddComponent<PhoneCamera>();
        }

        // === BATTERY ===
        var phoneBatteryObj = GameObject.Find("PhoneBattery");
        if (phoneBatteryObj == null)
        {
            phoneBatteryObj = new GameObject("PhoneBattery");
            var db = phoneBatteryObj.AddComponent<DeviceBattery>();
            db.maxCapacity = 5000f;
            db.currentCharge = 5000f;
        }
        batteryManager.phoneBattery = phoneBatteryObj.GetComponent<DeviceBattery>();

        // === AUDIO ===
        if (audioManager.sfxSource == null)
        {
            var sfx = new GameObject("SFXSource");
            sfx.transform.SetParent(audioManager.transform);
            audioManager.sfxSource = sfx.AddComponent<AudioSource>();
        }

        Debug.Log($"[Expediente] Setup complete.\n" +
                  $"- Player configured with CharacterController + Movement + Interaction + Inventory + Phone\n" +
                  $"- {securityCams.Count} security cameras found and configured\n" +
                  $"- Managers created: EventDirector, SecurityCameraManager, BatteryManager, AudioManager\n" +
                  $"- Review and assign remaining references in Inspector.");
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

    static T GetOrAdd<T>(GameObject go) where T : Component
    {
        var comp = go.GetComponent<T>();
        if (comp == null)
            comp = go.AddComponent<T>();
        return comp;
    }
}
