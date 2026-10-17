using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class UISetupAutomator : EditorWindow
{
    [MenuItem("Expediente/Setup Test UI")]
    public static void SetupUI()
    {
        // Canvas principal
        var canvasGO = GameObject.Find("TestCanvas") ?? new GameObject("TestCanvas");
        var canvas = GetOrAdd<Canvas>(canvasGO);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        GetOrAdd<CanvasScaler>(canvasGO);
        GetOrAdd<GraphicRaycaster>(canvasGO);

        // === PHONE UI ===
        var phonePanel = CreatePanel(canvasGO.transform, "PhonePanel", new Color(0, 0, 0, 0.85f));
        phonePanel.SetActive(false);

        var phoneUI = GetOrAdd<PhoneUI>(phonePanel);
        phoneUI.phonePanel = phonePanel;

        // Menú del teléfono
        var menuPanel = CreatePanel(phonePanel.transform, "MenuPanel", new Color(0.1f, 0.1f, 0.1f, 0.9f));
        phoneUI.menuPanel = menuPanel;

        CreateButton(menuPanel.transform, "Btn_Camera", "Cámara", new Vector2(0, 80));
        CreateButton(menuPanel.transform, "Btn_Gallery", "Galería", new Vector2(0, 20));
        CreateButton(menuPanel.transform, "Btn_Messages", "Mensajes", new Vector2(0, -40));
        CreateButton(menuPanel.transform, "Btn_SEGCAMS", "SEGCAMS", new Vector2(0, -100));

        // Apps
        var cameraApp = CreatePanel(phonePanel.transform, "CameraApp", new Color(0, 0, 0, 0.5f));
        var galleryApp = CreatePanel(phonePanel.transform, "GalleryApp", new Color(0.05f, 0.05f, 0.05f, 0.9f));
        var messagesApp = CreatePanel(phonePanel.transform, "MessagesApp", new Color(0.05f, 0.05f, 0.1f, 0.9f));
        var segcamsApp = CreatePanel(phonePanel.transform, "SEGCAMSApp", new Color(0, 0, 0, 0.95f));

        cameraApp.SetActive(false);
        galleryApp.SetActive(false);
        messagesApp.SetActive(false);
        segcamsApp.SetActive(false);

        phoneUI.appPanels = new GameObject[] { cameraApp, galleryApp, messagesApp, segcamsApp };

        // SEGCAMS UI
        var segcamsUI = GetOrAdd<SEGCAMSUI>(segcamsApp);
        var grid = CreatePanel(segcamsApp.transform, "GridPanel", new Color(0.1f, 0.1f, 0.1f, 1f));
        var fullView = CreatePanel(segcamsApp.transform, "FullViewPanel", Color.black);
        fullView.SetActive(false);
        segcamsUI.gridPanel = grid;
        segcamsUI.fullViewPanel = fullView;

        var feed = CreateRawImage(fullView.transform, "CameraFeed");
        segcamsUI.mainFeed = feed;

        // Gallery UI
        GetOrAdd<GalleryUI>(galleryApp);

        // Message UI
        GetOrAdd<MessageUI>(messagesApp);

        // === INVENTORY UI ===
        var invPanel = CreatePanel(canvasGO.transform, "InventoryPanel", new Color(0, 0, 0, 0.6f));
        invPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -200);
        invPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 80);

        var invUI = GetOrAdd<InventoryUI>(invPanel);
        invUI.slotImages = new Image[5];
        for (int i = 0; i < 5; i++)
        {
            var slot = CreateImage(invPanel.transform, $"Slot_{i}", new Color(0.2f, 0.2f, 0.2f, 0.8f));
            var rt = slot.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(60, 60);
            rt.anchoredPosition = new Vector2(-120 + i * 70, 0);
            invUI.slotImages[i] = slot.GetComponent<Image>();
        }

        // === BATTERY UI ===
        var batteryPanel = CreatePanel(canvasGO.transform, "BatteryPanel", new Color(0, 0, 0, 0.5f));
        batteryPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(200, 180);
        batteryPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 30);

        var fill = CreateImage(batteryPanel.transform, "BatteryFill", Color.green);
        var fillRT = fill.GetComponent<RectTransform>();
        fillRT.anchorMin = Vector2.zero;
        fillRT.anchorMax = Vector2.one;
        fillRT.offsetMin = new Vector2(2, 2);
        fillRT.offsetMax = new Vector2(-2, -2);
        fill.GetComponent<Image>().type = Image.Type.Filled;
        fill.GetComponent<Image>().fillMethod = Image.FillMethod.Horizontal;

        var batteryUI = GetOrAdd<BatteryUI>(batteryPanel);
        batteryUI.batteryFill = fill.GetComponent<Image>();

        // === NOTIFICATION ===
        var notifPanel = CreatePanel(canvasGO.transform, "NotificationPanel", new Color(0.1f, 0.1f, 0.1f, 0.9f));
        notifPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 150);
        notifPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 50);
        notifPanel.SetActive(false);
        GetOrAdd<PhoneNotification>(notifPanel);

        // === INTERACTION PROMPT ===
        var prompt = CreateText(canvasGO.transform, "InteractionPrompt", "E para interactuar");
        prompt.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100);
        prompt.SetActive(false);

        // Conectar con PlayerPhone si existe
        var player = GameObject.Find("Player");
        if (player != null)
        {
            var playerPhone = player.GetComponent<PlayerPhone>();
            if (playerPhone != null)
                playerPhone.phoneUI = phonePanel;
        }

        Debug.Log("[Expediente] Test UI created.\n" +
                  "- PhonePanel (with Menu, Camera, Gallery, Messages, SEGCAMS)\n" +
                  "- InventoryPanel (5 slots)\n" +
                  "- BatteryPanel\n" +
                  "- NotificationPanel\n" +
                  "- InteractionPrompt");
    }

    static GameObject CreatePanel(Transform parent, string name, Color color)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        var img = go.AddComponent<Image>();
        img.color = color;
        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        return go;
    }

    static GameObject CreateButton(Transform parent, string name, string label, Vector2 pos)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        var img = go.AddComponent<Image>();
        img.color = new Color(0.2f, 0.2f, 0.2f, 1f);
        var btn = go.AddComponent<Button>();
        var rt = go.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(160, 40);
        rt.anchoredPosition = pos;

        var textGO = new GameObject("Text");
        textGO.transform.SetParent(go.transform, false);
        var tmp = textGO.AddComponent<TextMeshProUGUI>();
        tmp.text = label;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontSize = 18;
        tmp.color = Color.white;
        var textRT = textGO.GetComponent<RectTransform>();
        textRT.anchorMin = Vector2.zero;
        textRT.anchorMax = Vector2.one;
        textRT.offsetMin = Vector2.zero;
        textRT.offsetMax = Vector2.zero;

        return go;
    }

    static GameObject CreateImage(Transform parent, string name, Color color)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        var img = go.AddComponent<Image>();
        img.color = color;
        return go;
    }

    static RawImage CreateRawImage(Transform parent, string name)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        var raw = go.AddComponent<RawImage>();
        raw.color = Color.white;
        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        return raw;
    }

    static GameObject CreateText(Transform parent, string name, string content)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        var tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.text = content;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontSize = 20;
        tmp.color = Color.white;
        return go;
    }

    static T GetOrAdd<T>(GameObject go) where T : Component
    {
        return go.GetComponent<T>() ?? go.AddComponent<T>();
    }
}
