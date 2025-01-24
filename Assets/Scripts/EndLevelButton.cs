using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class ExitLevel : MonoBehaviour
{
    public string sceneName;
    public TextMeshPro textToScale;
    public float targetFontSize = 2f;
    public float normalFontSize = 1.5f;
    public float scaleSpeed = 5f;
	public AudioClip hoverSound;
    private AudioSource audioSource;
    [Range(0f, 1f)]
    public float hoverSoundVolume = 0.5f;
	
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.volume = hoverSoundVolume;
    }

    void OnMouseDown()
    {
        LoadNextSceneWithDelay();
    }

    void OnMouseEnter()
    {
        StartCoroutine(ChangeFontSize(targetFontSize));
		if (hoverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hoverSound, hoverSoundVolume);
        }
    }

    void OnMouseExit()
    {
        StartCoroutine(ChangeFontSize(normalFontSize));
    }

    public void LoadNextSceneWithDelay()
    {
        StartCoroutine(LoadSceneAfterDelay(0f));
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = true;
    }

    private IEnumerator ChangeFontSize(float targetSize)
    {
        float initialSize = textToScale.fontSize;
        float timeElapsed = 0f;

        while (timeElapsed < 1f)
        {
            textToScale.fontSize = Mathf.Lerp(initialSize, targetSize, timeElapsed);
            timeElapsed += Time.deltaTime * scaleSpeed;
            yield return null;
        }
        textToScale.fontSize = targetSize;
    }
}
