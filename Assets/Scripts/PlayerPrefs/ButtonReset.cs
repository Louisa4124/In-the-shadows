using UnityEngine;

public class CubeClickHandler : MonoBehaviour
{
    public ResetProgression resetManager;

    void OnMouseDown()
    {
        if (resetManager != null)
            resetManager.OnResetButtonClick();
    }
}
