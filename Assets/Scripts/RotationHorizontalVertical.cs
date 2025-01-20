using UnityEngine;

public class ObjectRotationHorizontalVertical : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public float fixedRotationX = 3f;
    public float targetRotationZ = 90f;
    public float toleranceZ = 5f;
    public float autoRotationSpeed = 5f;
    public Animator animator;
    public float dragMouse = 0.5f;

    private bool hasWon = false;
    private int triggerAnimation = 0;
    private bool hasPlayed = false;
    [SerializeField] private CameraManager cameraManager;

    void Start()
    {

    }

    void Update()
    {
        Vector3 currentRotation = transform.eulerAngles;

        if (!hasWon)
        {
            HandleMouseRotation();
            CheckWinCondition();
        }
        else
        {
            RotateToFinalPosition();
        }

    }

    void HandleMouseRotation()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 currentRotation = transform.eulerAngles;

            if (Mathf.Abs(mouseX) > Mathf.Abs(mouseY))
            {
                currentRotation.y += mouseX * rotationSpeed * Time.deltaTime;
            }
            else
            {
                currentRotation.z += -mouseY * rotationSpeed * Time.deltaTime;
            }

            if (!hasPlayed && Mathf.Abs(mouseY) > dragMouse)
            {
                animator.SetTrigger("mouse");
                hasPlayed = true;
            }

            currentRotation.x = fixedRotationX;
            transform.eulerAngles = currentRotation;
        }
    }

    void CheckWinCondition()
    {
        float currentZ = transform.eulerAngles.z;
        if (currentZ > 180f) currentZ -= 360f;

        if (Mathf.Abs(currentZ - targetRotationZ) <= toleranceZ)
        {
            hasWon = true;
        }
    }

    void RotateToFinalPosition()
    {
        Vector3 currentRotation = transform.eulerAngles;

        currentRotation.x = Mathf.LerpAngle(currentRotation.x, fixedRotationX, autoRotationSpeed * Time.deltaTime);
        currentRotation.z = Mathf.LerpAngle(currentRotation.z, targetRotationZ, autoRotationSpeed * Time.deltaTime);
        currentRotation.y = Mathf.LerpAngle(currentRotation.y, 0f, autoRotationSpeed * Time.deltaTime);

        transform.eulerAngles = currentRotation;

        if (Mathf.Abs(currentRotation.z - targetRotationZ) < 0.1f)
        {
            hasWon = false;
        }

        if (!hasPlayed)
        {
            animator.SetTrigger("mouse");
            hasPlayed = true;
        }

        if (triggerAnimation == 0 && Mathf.Abs(currentRotation.z - targetRotationZ) - 360 <= 0.5f)
        {
            triggerAnimation = 1;
            cameraManager.SwitchToVictoryCamera();
        }

        if (triggerAnimation == 1)
        {
            // animation victory
            if (animator != null)
                animator.SetTrigger("win");
            triggerAnimation = 2;
        }
    }
}
