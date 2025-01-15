using UnityEngine;
using System;

public class NewGame : MonoBehaviour
{
    public void CreateNewGame()
    {
        // Create a new SaveData instance
        SaveData newSave = new SaveData
        {
            characterName = "New Hero",           // Default character name
            characterRace = "Human",              // Default race
            characterSex = "Male",                // Default gender
            characterLevel = 1,                   // Starting level
            characterClass = "Fighter",           // Default class
            currentHP = 10,                       // Starting HP
            maxHP = 10,                           // Max HP
            temporaryHP = 0,                      // No temporary HP
            hitDiceCount = 1,                     // Starting with 1 hit dice
            hitDiceType = "d10",                  // Default hit dice type
            equipment = new System.Collections.Generic.List<string> { "Sword", "Shield" },
            inventory = new System.Collections.Generic.List<string> { "Health Potion" },
            abilities = new System.Collections.Generic.List<string> { "Second Wind" },
            spells = new System.Collections.Generic.List<string>(), // Empty for Fighter
            attacks = new System.Collections.Generic.List<string> { "Slash" },
            playtime = 0f,                        // Starting playtime in hours
            saveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") // Current date and time
        };

        // Save the new game using SaveSystem
        SaveSystem.SaveGame(newSave, "save_slot_1");
        Debug.Log("New Save Created: " + newSave.characterName);
    }
}
