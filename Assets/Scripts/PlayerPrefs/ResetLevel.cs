using UnityEngine;

public class ResetProgression : MonoBehaviour
{
    public void OnResetButtonClick()
    {
		Debug.Log("Reset progression!");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.SetInt("UnlockedLevelCount", 1);
        PlayerPrefs.Save();
    }
}
