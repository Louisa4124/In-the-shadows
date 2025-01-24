using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoverDetection : MonoBehaviour
{
	public float scaleFactor = 1.1f;
	public float lerpSpeed = 5f;
	public float rotationSpeed = 300f;
	public bool isLevelUnlock = false;
	public float levelIndex = 0;
	public string sceneName;
	public Animator targetAnimator;
	public GameObject resolvedObject;
	public GameObject lockedObject;
	public AudioClip hoverSound;
	public float hoverSoundVolume = 0.5f;


	private Vector3 originalScale;
	private Vector3 targetScale;
	private bool isMouseOver = false;
	private Quaternion originalRotation;
	private AudioSource audioSource;
	private bool hasPlayedHoverSound = false;

	void Start()
	{
		originalRotation = transform.parent.rotation;

		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}
		audioSource.playOnAwake = false;
		audioSource.volume = hoverSoundVolume;
	}

	void Update()
	{
		ObjectIsUnlock();
		ObjectIsResolved();

		if (isMouseOver == true)
		{
			float horizontalInput = Input.GetAxis("Mouse X");

			float deltaY = horizontalInput * rotationSpeed * Time.deltaTime;

			transform.parent.Rotate(0f, deltaY, 0f, Space.Self);

			if (Input.GetMouseButtonDown(0) && isLevelUnlock == true)
			{
				targetAnimator.SetTrigger("LaunchLevel");
				LoadNextSceneWithDelay();
			}
			// Debug.Log(PlayerPrefs.GetInt("UnlockedLevelCount", 1));

			if (!hasPlayedHoverSound && hoverSound != null && audioSource != null)
			{
				audioSource.PlayOneShot(hoverSound, hoverSoundVolume);
				hasPlayedHoverSound = true;
			}
		}
		else
		{
			transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, originalRotation, Time.deltaTime * lerpSpeed);
			hasPlayedHoverSound = false; 
		}
	}

	void OnMouseOver()
	{
		isMouseOver = true;
		if (originalScale == Vector3.zero)
			originalScale = transform.parent.localScale;

		targetScale = originalScale * scaleFactor;
		transform.parent.localScale = Vector3.Lerp(transform.parent.localScale, targetScale, Time.deltaTime * lerpSpeed);
	}

	void OnMouseExit()
	{
		isMouseOver = false;
		transform.parent.localScale = originalScale;
	}

	public void LoadNextSceneWithDelay()
	{
		StartCoroutine(LoadSceneAfterDelay(2f));
	}

	private IEnumerator LoadSceneAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
		asyncLoad.allowSceneActivation = true;
	}

	public void ObjectIsUnlock()
	{
		if (levelIndex <= PlayerPrefs.GetInt("UnlockedLevelCount", 1))
		{
			isLevelUnlock = true;
			if (lockedObject != null)
				lockedObject.SetActive(false);
		}
		else
		{
			isLevelUnlock = false;
			if (lockedObject != null)
				lockedObject.SetActive(true);
		}
	}

	public void ObjectIsResolved()
	{
		if (levelIndex < PlayerPrefs.GetInt("UnlockedLevelCount", 1))
		{
			if (resolvedObject != null)
				resolvedObject.SetActive(true);
		}
		else
		{
			if (resolvedObject != null)
				resolvedObject.SetActive(false);
		}
	}
}
