using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    public Text nameText;
    public Text classText;
    public Text levelText;
    public Text playtimeText;
    public Text dateText;

    public void Populate(string name, string className, int level, float playtime, string date)
    {
        nameText.text = name;
        classText.text = className;
        levelText.text = "Level: " + level.ToString();
        playtimeText.text = playtime.ToString("F1") + " hrs";
        dateText.text = date;
    }
}
