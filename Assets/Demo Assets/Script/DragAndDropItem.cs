using UnityEngine;

public class DragAndDropItem : MonoBehaviour
{
    public float attractSpeed = 5f; // Speed at which the object moves towards the attract point
    public bool isDragging = false; // Flag to track if the object is being dragged
    private Vector3 attractPoint; // Point to which the object is attracted
    [SerializeField] Vector3 StartPoint;

    public Texture2D handOpenCursor; // Texture for the cursor when not dragging
    public Texture2D handCloseCursor; // Texture for the cursor when dragging

    private void Start()
    {
        if (GetComponent<BoxCollider>() == null) // Add a BoxCollider if not already present
        {
            gameObject.AddComponent<BoxCollider>();
        }
        StartPoint = transform.position;
    }

    void Update()
    {
        if (isDragging)
        {
            // Get the mouse position in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Ensure the object stays on the same Z-plane

            // Move the object towards the mouse position
            transform.position = Vector3.MoveTowards(transform.position, mousePosition, attractSpeed * Time.deltaTime);
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // When left mouse button is clicked, start dragging
            isDragging = true;
            Cursor.SetCursor(handCloseCursor, Vector2.zero, CursorMode.Auto); // Change cursor to hand close
        }
    }

    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // When left mouse button is released, stop dragging
            isDragging = false;
            transform.position = StartPoint;
            Cursor.SetCursor(handOpenCursor, Vector2.zero, CursorMode.Auto); // Change cursor to hand open
        }
    }

    void OnMouseDrag()
    {
        // Update the attract point while dragging
        attractPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        attractPoint.z = 0f; // Ensure the object stays on the same Z-plane
    }
}
