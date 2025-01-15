using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private Transform saveListContainer; // Where save slots will appear
    [SerializeField] private GameObject saveSlotPrefab;   // Prefab for a save slot UI

    private void Start()
    {
        DisplaySaves();
    }

    public void DisplaySaves()
    {
        // Clear old save slots
        foreach (Transform child in saveListContainer)
        {
            Destroy(child.gameObject);
        }

        // Get save files
        string[] saveFiles = SaveSystem.GetSaveFiles();

        foreach (string saveFile in saveFiles)
        {
            SaveData saveData = SaveSystem.LoadGame(Path.GetFileNameWithoutExtension(saveFile));
            if (saveData == null) continue;

            GameObject saveSlot = Instantiate(saveSlotPrefab, saveListContainer);
            SaveSlotUI slotUI = saveSlot.GetComponent<SaveSlotUI>();

            // Populate the save slot with the correct data types
            slotUI.Populate(
                saveData.characterName,
                saveData.characterClass,
                saveData.characterLevel,          // Ensure this is an int
                saveData.playtime,                // Ensure this is a float
                saveData.saveDate                 // Ensure this is a string
            );

            saveSlot.GetComponent<Button>().onClick.AddListener(() =>
            {
                LoadGameFile(saveFile);
            });
        }
    }

    public void LoadGameFile(string saveFilePath)
    {
        SaveData loadedData = SaveSystem.LoadGame(Path.GetFileNameWithoutExtension(saveFilePath));
        if (loadedData != null)
        {
            Debug.Log("Loaded game for: " + loadedData.characterName);
            // Implement scene loading logic here
        }
    }
}
