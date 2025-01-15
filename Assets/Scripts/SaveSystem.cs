using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string saveDirectory = Application.persistentDataPath + "/Saves";

    public static void SaveGame(SaveData data, string saveFileName)
    {
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Path.Combine(saveDirectory, saveFileName + ".json"), json);
        Debug.Log("Game saved to: " + Path.Combine(saveDirectory, saveFileName + ".json"));
    }

    public static SaveData LoadGame(string saveFileName)
    {
        string path = Path.Combine(saveDirectory, saveFileName + ".json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.LogError("Save file not found at: " + path);
            return null;
        }
    }

    public static string[] GetSaveFiles()
    {
        if (Directory.Exists(saveDirectory))
        {
            return Directory.GetFiles(saveDirectory, "*.json");
        }

        return new string[0];
    }
}
