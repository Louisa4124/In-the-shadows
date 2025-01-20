using UnityEngine;

public class WinManager : MonoBehaviour
{
    public Light spotlight;
    public Color originalColor = new Color(247 / 255f, 121 / 255f, 41 / 255f);
    public Color winColor = Color.green;
    public float colorTransitionDuration = 1f;

    private float transitionTimer = 0f;

    public void ResetLight()
    {
        if (spotlight != null)
        {
            spotlight.color = originalColor;
            transitionTimer = 0f;
        }
    }

    public void HandleWinningLight()
    {
        if (spotlight != null)
        {
            transitionTimer += Time.deltaTime;
            float t = Mathf.PingPong(transitionTimer / colorTransitionDuration, 1f);
            spotlight.color = Color.Lerp(originalColor, winColor, t);
        }
    }
}
