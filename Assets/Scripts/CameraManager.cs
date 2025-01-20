using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private CinemachineCamera IntroCamera;  
    [SerializeField] private CinemachineCamera mainCamera;  
    [SerializeField] private CinemachineCamera victoryCamera;  
    [SerializeField] private CinemachineCamera ExitCamera;  


    void Start()
    {
        SwitchToMainCamera();
    }

    public void SwitchToMainCamera()
    {
        mainCamera.gameObject.SetActive(true);
        victoryCamera.gameObject.SetActive(false);
        IntroCamera.gameObject.SetActive(false);
        ExitCamera.gameObject.SetActive(false);
    }

    public void SwitchToVictoryCamera()
    {
        mainCamera.gameObject.SetActive(false); 
        victoryCamera.gameObject.SetActive(true);  
        IntroCamera.gameObject.SetActive(false);
        ExitCamera.gameObject.SetActive(false);
    }

	public void SwitchToExitCamera()
    {
        mainCamera.gameObject.SetActive(false); 
        victoryCamera.gameObject.SetActive(false);  
        IntroCamera.gameObject.SetActive(false);
        ExitCamera.gameObject.SetActive(true);
    }
}
