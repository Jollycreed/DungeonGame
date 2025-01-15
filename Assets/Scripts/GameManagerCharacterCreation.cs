using UnityEngine;

public class GameManagerCharacterCreation : MonoBehaviour
{
    // Reference to the CharacterCreation script
    public CharacterCreation characterCreation;

    // --- Functions for Class Selection ---
    public void OnNextClass()
    {
        characterCreation.NextClass();
    }

    public void OnPreviousClass()
    {
        characterCreation.PreviousClass();
    }

    // --- Functions for Race Selection ---
    public void OnNextRace()
    {
        characterCreation.NextRace();
    }

    public void OnPreviousRace()
    {
        characterCreation.PreviousRace();
    }

    // --- Functions for Gender Selection ---
    public void OnNextGender()
    {
        characterCreation.NextGender();
    }

    public void OnPreviousGender()
    {
        characterCreation.PreviousGender();
    }
}
