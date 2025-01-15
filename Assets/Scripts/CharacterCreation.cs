using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterCreation : MonoBehaviour
{
    // UI Elements
    public InputField nameInput;
    public Text raceText;
    public Text genderText;
    public Text classText;
    public Button confirmButton;
    public Text[] statLabels;         // Labels for attributes like Strength, etc.
    public Dropdown[] statDropdowns;  // Dropdowns to assign stats from the standard array

    // Arrays for Selection Data
    private string[] races = { "Human", "Elf", "Half-Elf", "Dwarf", "Orc", "Tiefling", "Halfling", "Gnome", "Dragonborn", "Half-Orc" };
    private string[] genders = { "Male", "Female" };
    private string[] classes = { "Barbarian", "Bard", "Cleric", "Druid", "Fighter", "Monk", "Paladin", "Ranger", "Rogue", "Sorcerer", "Warlock", "Wizard" };

    // Current Indices
    private int currentRaceIndex = 0;
    private int currentGenderIndex = 0;
    private int currentClassIndex = 0;

    // Character Data
    private string characterName;
    private string characterRace;
    private string characterGender;
    private string characterClass;
    private Dictionary<string, int> attributes;
    private int maxHp;
    private int hitDiceValue;

    // Standard Array
    private int[] standardArray = { 15, 14, 13, 12, 10, 8 };
    private bool[] usedValues = new bool[6]; // Track used values from the standard array

    void Start()
    {
        UpdateUI();
        confirmButton.onClick.AddListener(ConfirmCharacter);
    }

    // --- Functions for Race Selection ---
    public void NextRace()
    {
        currentRaceIndex = (currentRaceIndex + 1) % races.Length;
        UpdateUI();
    }

    public void PreviousRace()
    {
        currentRaceIndex = (currentRaceIndex - 1 + races.Length) % races.Length;
        UpdateUI();
    }

    // --- Functions for Gender Selection ---
    public void NextGender()
    {
        currentGenderIndex = (currentGenderIndex + 1) % genders.Length;
        UpdateUI();
    }

    public void PreviousGender()
    {
        currentGenderIndex = (currentGenderIndex - 1 + genders.Length) % genders.Length;
        UpdateUI();
    }

    // --- Functions for Class Selection ---
    public void NextClass()
    {
        currentClassIndex = (currentClassIndex + 1) % classes.Length;
        UpdateUI();
    }

    public void PreviousClass()
    {
        currentClassIndex = (currentClassIndex - 1 + classes.Length) % classes.Length;
        UpdateUI();
    }

    // Update the UI with current selections
    void UpdateUI()
    {
        raceText.text = $"Race: {races[currentRaceIndex]}";
        genderText.text = $"Gender: {genders[currentGenderIndex]}";
        classText.text = $"Class: {classes[currentClassIndex]}";
    }

    // Confirm Character Creation
    void ConfirmCharacter()
    {
        // Capture basic details
        characterName = nameInput.text;
        characterRace = races[currentRaceIndex];
        characterGender = genders[currentGenderIndex];
        characterClass = classes[currentClassIndex];

        if (string.IsNullOrEmpty(characterName))
        {
            Debug.LogError("Character name cannot be empty!");
            return;
        }

        // Assign stats
        attributes = new Dictionary<string, int>();
        for (int i = 0; i < statLabels.Length; i++)
        {
            string statName = statLabels[i].text;
            int statValue = int.Parse(statDropdowns[i].options[statDropdowns[i].value].text);

            if (usedValues[statValue])
            {
                Debug.LogError($"Value {statValue} already used. Please assign unique values.");
                return;
            }

            usedValues[statValue] = true; // Mark value as used
            attributes[statName] = statValue;
        }

        ApplyRaceModifiers();
        SetupClass();

        Debug.Log($"Character Created: {characterName}, {characterRace}, {characterGender}, {characterClass}");
    }

    void ApplyRaceModifiers()
    {
        if (characterRace == "Human")
        {
            foreach (var stat in attributes.Keys)
            {
                attributes[stat]++;
            }
        }
        else if (characterRace == "Elf" || characterRace == "Drow")
        {
            attributes["Dexterity"] += 2;
            attributes["Intelligence"]++;
        }
    }

    void SetupClass()
    {
        hitDiceValue = characterClass switch
        {
            "Barbarian" => 12,
            "Fighter" or "Paladin" or "Ranger" => 10,
            "Rogue" or "Cleric" or "Warlock" or "Bard" or "Monk" => 8,
            _ => 6 // Default for Wizard, Sorcerer
        };

        maxHp = hitDiceValue + GetModifier(attributes["Constitution"]);
        Debug.Log($"Starting HP: {maxHp}");
    }

    int GetModifier(int statValue)
    {
        return (statValue - 10) / 2;
    }
}
