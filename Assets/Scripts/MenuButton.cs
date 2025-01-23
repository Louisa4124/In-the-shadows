using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject leftImage;  
    public GameObject rightImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (leftImage != null) leftImage.SetActive(true);
        if (rightImage != null) rightImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (leftImage != null) leftImage.SetActive(false);
        if (rightImage != null) rightImage.SetActive(false);
    }
}
