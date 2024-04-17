using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DragAndDropItem : MonoBehaviour
{
    public float attractSpeed = 5f; // Speed at which the object moves towards the attract point
    public bool isDragging = false; // Flag to track if the object is being dragged
    private Vector3 attractPoint; // Point to which the object is attracted
    [SerializeField] Vector3 StartPoint;

    private void Start()
    {
        if (this.GetComponent<BoxCollider>() != null) this.AddComponent<BoxCollider>();
        StartPoint = this.transform.position;
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
        }
    }

    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // When left mouse button is released, stop dragging
            isDragging = false;
            this.transform.position = StartPoint;
        }
    }

    void OnMouseDrag()
    {
        // Update the attract point while dragging
        attractPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        attractPoint.z = 0f; // Ensure the object stays on the same Z-plane
    }
}
