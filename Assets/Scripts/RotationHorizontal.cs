using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ObjectRotationHorizontal : MonoBehaviour
{
    public float rotationSpeed = 300f;
    public float fixedRotationX = 0f;
    public float fixedRotationZ = 0f;
    public float targetRotationY = 180f;
    public float tolerance = 8f;
    public float winAnimationTolerance = 1f;
    public float autoRotationSpeed = 20f;
    public Color originalColor = Color.white;
    public Color winColor = Color.white;
    public float colorTransitionDuration = 1f;
    public Animator animator;
    public float dragMouse = 0.5f;
    public string sceneName;
    public AudioClip winSound;
    public float winSoundVolume = 0.3f;


    private bool canRotate = true;
    private bool isInWinningRange = false;
    private int triggerAnimation = 0;
    private float transitionTimer = 0f;
    private bool hasPlayed = false;
    [SerializeField] private CameraManager cameraManager;
    private AudioSource audioSource;

    void Start()
    {
        Vector3 initialRotation = transform.eulerAngles;
        initialRotation.y = 120f;
        transform.eulerAngles = initialRotation;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.volume = winSoundVolume;
    }

    void Update()
    {
        if (canRotate)
        {
            if (Input.GetMouseButton(0))
            {
                float horizontalInput = Input.GetAxis("Mouse X");
                if (!hasPlayed && Mathf.Abs(horizontalInput) > dragMouse)
                {
                    animator.SetTrigger("mouse");
                    hasPlayed = true;
                }
                float deltaY = horizontalInput * rotationSpeed * Time.deltaTime;

                transform.Rotate(0f, deltaY, 0f, Space.Self);
            }

            float currentRotationY = NormalizeAngle(transform.eulerAngles.y);
            if (currentRotationY < 0)
            {
                currentRotationY += 360f;
            }

            if (currentRotationY >= targetRotationY - tolerance && currentRotationY <= targetRotationY + tolerance)
            {
                isInWinningRange = true;
                canRotate = false;
            }
        }

        if (isInWinningRange)
        {
            HandleWinningLight();

            if (winSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(winSound);
            }

            float currentRotationY = NormalizeAngle(transform.eulerAngles.y);
            if (Mathf.Abs(currentRotationY - targetRotationY) > 0.1f)
            {
                float step = autoRotationSpeed * Time.deltaTime;
                float newY = Mathf.MoveTowardsAngle(currentRotationY, targetRotationY, step);
                transform.eulerAngles = new Vector3(fixedRotationX, newY, fixedRotationZ);
            }
            else
            {
                if (triggerAnimation == 0)
                    triggerAnimation = 1;
                if (transitionTimer >= colorTransitionDuration * 2f)
                {
                    isInWinningRange = false;
                }
            }

            if (!hasPlayed)
            {
                animator.SetTrigger("mouse");
                hasPlayed = true;
            }

            cameraManager.SwitchToVictoryCamera();
            // a changer !!
            // if (Input.GetMouseButton(0))
            //     LoadNextSceneWithDelay();
        }

        if (triggerAnimation == 1)
        {
            // animation victory
            if (animator != null)
                animator.SetTrigger("win");
            triggerAnimation = 2;
            IncrementUnlockedLevelCount();
        }

        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.x = fixedRotationX;
        currentRotation.z = fixedRotationZ;
        transform.eulerAngles = currentRotation;

    }

    private float NormalizeAngle(float angle)
    {
        return (angle > 180) ? angle - 360 : angle;
    }

    private void HandleWinningLight()
    {
        transitionTimer += Time.deltaTime;
        float t = Mathf.PingPong(transitionTimer / colorTransitionDuration, 1f);
    }

    public void IncrementUnlockedLevelCount()
    {
        int currentCount = PlayerPrefs.GetInt("UnlockedLevelCount", 1);

        if (currentCount < 2)
            currentCount++;
        PlayerPrefs.SetInt("UnlockedLevelCount", currentCount);
        PlayerPrefs.Save();
    }

    public void LoadNextSceneWithDelay()
    {
        StartCoroutine(LoadSceneAfterDelay(5f));
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = true;
    }
}
