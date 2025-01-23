using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;
    public Animator animator;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextSceneWithDelay(float delay)
	{
		StartCoroutine(LoadSceneAfterDelay(delay));
	}

	private IEnumerator LoadSceneAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
		asyncLoad.allowSceneActivation = true;
	}

    public void SetTrigger(string trigger)
    {
        if (animator != null)
            animator.SetTrigger(trigger);
    }
}
