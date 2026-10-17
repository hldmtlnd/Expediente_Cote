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
        var hidingManager = CreateManager<HidingManager>("HidingManager");

        // === PLAYER ===
        var player = GameObject.Find("Player");
        if (player != null)
        {
            player.tag = "Player";

            var cc = GetOrAdd<CharacterController>(player);
            cc.height = 1.8f;
            cc.radius = 0.3f;
            cc.center = new Vector3(0, 0.9f, 0);

            var movement = GetOrAdd<PlayerMovement>(player);
            var interaction = GetOrAdd<PlayerInteraction>(player);
            var inventory = GetOrAdd<PlayerInventory>(player);
            var phone = GetOrAdd<PlayerPhone>(player);

            // First person camera
            var fpCam = player.transform.Find("FirstPersonCamera");
            if (fpCam == null)
            {
                var go = new GameObject("FirstPersonCamera");
                go.transform.SetParent(player.transform);
                go.transform.localPosition = new Vector3(0, 1.6f, 0);
                var cam = go.AddComponent<Camera>();
                cam.enabled = false;
                fpCam = go.transform;
            }
            movement.firstPersonCamera = fpCam;

            // Phone UI
            var phoneUI = GameObject.Find("PhoneUI") ?? new GameObject("PhoneUI");
            if (phoneUI.GetComponent<PhoneUI>() == null)
                phoneUI.AddComponent<PhoneUI>();
            phone.phoneUI = phoneUI;
            phone.playerMovement = movement;
            phone.cameraManager = cameraManager;
        }

        // === SECURITY CAMERAS ===
        var securityCams = new List<SecurityCamera>();
        var secamRoot = GameObject.Find("SECAM (1)");
        if (secamRoot != null)
        {
            var segcam = secamRoot.transform.Find("SEGCAM (1)");
            if (segcam != null)
            {
                var cam = segcam.GetComponent<Camera>();
                if (cam != null)
                {
                    var secCam = GetOrAdd<SecurityCamera>(segcam.gameObject);
                    var sensor = GetOrAdd<SecurityCameraSensor>(segcam.gameObject);
                    sensor.securityCamera = secCam;

                    secCam.minPan = -90f;
                    secCam.maxPan = 90f;
                    secCam.minTilt = -30f;
                    secCam.maxTilt = 45f;

                    securityCams.Add(secCam);
                    cam.enabled = true;
                }
            }
        }

        // Also search any other cameras with SECAM/SEGCAM in name
        foreach (var cam in Object.FindObjectsOfType<Camera>())
        {
            string n = cam.gameObject.name.ToUpper();
            if ((n.Contains("SECAM") || n.Contains("SEGCAM")) && !securityCams.Any(s => s.gameObject == cam.gameObject))
            {
                var secCam = GetOrAdd<SecurityCamera>(cam.gameObject);
                GetOrAdd<SecurityCameraSensor>(cam.gameObject).securityCamera = secCam;
                securityCams.Add(secCam);
            }
        }

        cameraManager.cameras = securityCams;
        if (securityCams.Count > 0)
            cameraManager.activeCamera = securityCams[0];

        // === ROOM TRIGGERS ===
        var habitaciones = GameObject.Find("habitaciones");
        if (habitaciones != null)
        {
            foreach (Transform room in habitaciones.transform)
            {
                var col = room.GetComponent<BoxCollider>();
                if (col != null)
                    col.isTrigger = true;

                // Add a simple room trigger component if needed later
            }
        }

        // === ALIEN INVISIBLE ===
        var alienInv = GameObject.Find("Alien invisible");
        if (alienInv != null)
        {
            GetOrAdd<AlienInvisibleController>(alienInv);
            GetOrAdd<AlienInteraction>(alienInv);
            GetOrAdd<AlienAI>(alienInv);
            GetOrAdd<AlienSensor>(alienInv);
            GetOrAdd<AlienAudio>(alienInv);

            var ai = alienInv.GetComponent<AlienAI>();
            if (ai != null && player != null)
                ai.GetComponent<AlienSensor>().player = player.transform;
        }

        // === PHONE ===
        var telefono = GameObject.Find("Telefono");
        if (telefono != null)
        {
            var backCam = telefono.GetComponentInChildren<Camera>();
            if (backCam != null && backCam.gameObject.name.Contains("BACK"))
            {
                GetOrAdd<PhoneCamera>(backCam.gameObject);
            }
        }

        // === ADVANCED CAMERA ===
        var advCamRoot = GameObject.Find("Advanced Camera");
        if (advCamRoot != null)
        {
            var advcam = advCamRoot.transform.Find("ADVCAM");
            if (advcam != null)
                GetOrAdd<AdvancedCamera>(advcam.gameObject);
        }

        // === MUTILATION (disabled by default) ===
        var mutilation = GameObject.Find("Mutilation");
        if (mutilation != null)
            mutilation.SetActive(false);

        // === BATTERY ===
        var phoneBattery = GameObject.Find("PhoneBattery") ?? new GameObject("PhoneBattery");
        var db = GetOrAdd<DeviceBattery>(phoneBattery);
        db.maxCapacity = 5000f;
        db.currentCharge = 5000f;
        batteryManager.phoneBattery = db;

        // === AUDIO ===
        if (audioManager.sfxSource == null)
        {
            var sfx = new GameObject("SFXSource");
            sfx.transform.SetParent(audioManager.transform);
            audioManager.sfxSource = sfx.AddComponent<AudioSource>();
        }

        Debug.Log($"[Expediente] Setup complete.\n" +
                  $"- Player: {(player != null ? "OK" : "NOT FOUND")}\n" +
                  $"- Security Cameras: {securityCams.Count}\n" +
                  $"- Rooms: {(habitaciones != null ? habitaciones.transform.childCount : 0)}\n" +
                  $"- Alien invisible: {(alienInv != null ? "OK" : "NOT FOUND")}\n" +
                  $"- Phone: {(telefono != null ? "OK" : "NOT FOUND")}\n" +
                  $"- Advanced Camera: {(advCamRoot != null ? "OK" : "NOT FOUND")}\n" +
                  $"- Mutilation disabled: {(mutilation != null)}");
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
        return go.GetComponent<T>() ?? go.AddComponent<T>();
    }
}
