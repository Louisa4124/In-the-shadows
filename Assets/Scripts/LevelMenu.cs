using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    public Transform content;
    public float scrollSpeed = 20f;
    public float edgeThreshold = 100f;
    public float verticalScrollZoneHeight = 300f;	

    private Vector3 initialContentPosition;

    void Start()
    {
        initialContentPosition = content.localPosition;
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
		float positionHorizontalLow = ((screenHeight - verticalScrollZoneHeight) / 2) - 100;
		float positionHorizontalUp = ((screenHeight + verticalScrollZoneHeight) / 2) - 50;

        if (mousePosition.y >= positionHorizontalLow && mousePosition.y <= positionHorizontalUp)
        {
            if (mousePosition.x <= edgeThreshold)
                ScrollContent(Vector3.left);
            else if (mousePosition.x >= screenWidth - edgeThreshold)
                ScrollContent(Vector3.right);
        }
    }

    private void ScrollContent(Vector3 direction)
    {
        Vector3 movement = direction * scrollSpeed * Time.deltaTime;
        content.localPosition += movement;
        ClampContentPosition();
    }

    private void ClampContentPosition()
    {
        float minX = 0;
        float maxX = 60;

        Vector3 clampedPosition = content.localPosition;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        content.localPosition = clampedPosition;
    }

    private float CalculateContentWidth()
    {
        float width = 0f;
        foreach (Transform child in content)
        {
            Renderer childRenderer = child.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                width += childRenderer.bounds.size.x;
            }
        }
        return width;
    }

    private float CalculateParentWidth()
    {
        Renderer parentRenderer = content.parent.GetComponent<Renderer>();
        if (parentRenderer != null)
        {
            return parentRenderer.bounds.size.x;
        }
        return 0f;
    }
}
