using UnityEngine;

public class CameraFloat : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 0.2f;
    public Vector3 rotationAmplitude = new Vector3(1f, 1.5f, 0f);
    public float rotationSpeed = 0.5f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float timeOffset;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        timeOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        float verticalOffset = Mathf.Sin((Time.time + timeOffset) * frequency) * amplitude;
        transform.position = initialPosition + new Vector3(0f, verticalOffset, 0f);

        float rotationX = Mathf.Sin((Time.time + timeOffset) * rotationSpeed) * rotationAmplitude.x;
        float rotationY = Mathf.Cos((Time.time + timeOffset) * rotationSpeed) * rotationAmplitude.y;
        transform.rotation = initialRotation * Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
