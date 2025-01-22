using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    void Start()
    {
        // Debug.Log("HUH?");
        if (!PlayerPrefs.HasKey("UnlockedLevel"))
        {
            PlayerPrefs.SetInt("UnlockedLevel", 1);
            PlayerPrefs.Save(); 
        }
    }
}
