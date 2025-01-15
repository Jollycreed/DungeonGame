using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public string characterName;
    public string characterRace;
    public string characterSex;
    public int characterLevel;
    public string characterClass;
    public int currentHP;
    public int maxHP;
    public int temporaryHP;
    public int hitDiceCount;
    public string hitDiceType;

    public List<string> equipment;
    public List<string> inventory;
    public List<string> abilities;
    public List<string> spells;
    public List<string> attacks;

    public float playtime;       // Added playtime as a float
    public string saveDate;      // Ensure saveDate is a string
}
