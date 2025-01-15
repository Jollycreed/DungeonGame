using UnityEngine;

public static class GameStateManager
{
    private static SaveData currentSaveData;

    public static void SetCurrentSaveData(SaveData data)
    {
        currentSaveData = data;
    }

    public static SaveData GetCurrentSaveData()
    {
        return currentSaveData;
    }
}
