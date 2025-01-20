using UnityEngine;

public class AnimationEventController : MonoBehaviour
{
    [SerializeField] private TextAnim textAnim;
    [SerializeField] private TextAnim textAnim2;

    public void TriggerTextAnimation()
    {
        textAnim.EndCheck(); 
    }

	public void TriggerTextAnimation2()
    {
        textAnim2.EndCheck(); 
    }

}