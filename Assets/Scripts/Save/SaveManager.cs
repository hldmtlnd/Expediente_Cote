using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    private string savePath;

    void Awake()
    {
        if (Instance == null) Instance = this;
        savePath = Application.persistentDataPath + "/expediente_save.json";
    }

    public void SaveGame(GameState state)
    {
        string json = JsonUtility.ToJson(state, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved.");
    }

    public GameState LoadGame()
    {
        if (!File.Exists(savePath)) return new GameState();
        string json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<GameState>(json);
    }

    public void AutoSave()
    {
        // Called by EventDirector or Checkpoint
    }
}
