using UnityEngine;
using System.Text;

public class SceneHierarchyDebug : MonoBehaviour
{
    [Header("Options")]
    public bool logOnStart = true;
    public bool includeInactive = true;
    public bool showComponents = true;

    void Start()
    {
        if (logOnStart)
            LogFullHierarchy();
    }

    [ContextMenu("Log Full Hierarchy")]
    public void LogFullHierarchy()
    {
        var sb = new StringBuilder();
        sb.AppendLine("========== SCENE HIERARCHY DEBUG ==========");
        sb.AppendLine($"Scene: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");
        sb.AppendLine("===========================================");

        var roots = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var root in roots)
        {
            if (!includeInactive && !root.activeInHierarchy) continue;
            LogObject(root, sb, 0);
        }

        sb.AppendLine("===========================================");
        sb.AppendLine("END OF HIERARCHY");
        Debug.Log(sb.ToString());
    }

    void LogObject(GameObject go, StringBuilder sb, int depth)
    {
        string indent = new string(' ', depth * 2);
        string active = go.activeSelf ? "" : " [INACTIVE]";
        sb.AppendLine($"{indent}└─ {go.name}{active}");

        if (showComponents)
        {
            var components = go.GetComponents<Component>();
            foreach (var comp in components)
            {
                if (comp == null) continue;
                string compIndent = new string(' ', (depth + 1) * 2);
                sb.AppendLine($"{compIndent}· {comp.GetType().Name}");
            }
        }

        foreach (Transform child in go.transform)
        {
            if (!includeInactive && !child.gameObject.activeInHierarchy) continue;
            LogObject(child.gameObject, sb, depth + 1);
        }
    }
}
