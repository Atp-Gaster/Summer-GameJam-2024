using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public Vector3 startPoint; // Starting position of the line
    public float attractSpeed = 5f; // Speed at which the end point attracts to the mouse position

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Set up the initial positions of the line renderer
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, startPoint);
    }

    void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure the mouse position stays on the same Z-plane

        // Move the end point towards the mouse position
        Vector3 endPosition = Vector3.MoveTowards(lineRenderer.GetPosition(1), mousePosition, attractSpeed * Time.deltaTime);

        // Update the end position of the line renderer
        lineRenderer.SetPosition(1, endPosition);
    }
}
