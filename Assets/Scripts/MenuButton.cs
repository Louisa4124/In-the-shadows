using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject leftImage;  
    public GameObject rightImage;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (leftImage != null) leftImage.SetActive(true);
        if (rightImage != null) rightImage.SetActive(true);
        if (hoverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hoverSound, hoverSoundVolume);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (leftImage != null) leftImage.SetActive(false);
        if (rightImage != null) rightImage.SetActive(false);
    }
}
