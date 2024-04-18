using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class order : MonoBehaviour
{
    private bool dragging;

    private Vector2 _offset;

    private Vector2 _originalpos;

    SpriteRenderer orderSprite;

    [SerializeField] public RecipeSO cocktail;

    // set where the start point is
    private void Start()
    {
        _originalpos = transform.position;
        orderSprite = transform.GetComponent<SpriteRenderer>();
        orderSprite.sprite = cocktail.RecipeImage;
        if (this.GetComponent<BoxCollider2D>() == null) this.AddComponent<BoxCollider2D>();
    }

    // while clicking
    private void OnMouseDown()
    {
        dragging = true;
        if (this.GetComponent<BoxCollider2D>() != null)
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        _offset = GetMousePos() - (Vector2)transform.position;
    }

    // when release mouse, return to start point
    private void OnMouseUp()
    {
        dragging = false;
        if (this.GetComponent<BoxCollider2D>() != null)
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
        bool overlapping = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Seat")
            {
                overlapping = true;
                collider.gameObject.GetComponent<CustomerSeat>().OnTriggerEnter2D(GetComponent<Collider2D>());
                break;
            }
        }

        if (!overlapping)
        {
            transform.position = _originalpos;
        }
        transform.position = _originalpos;
    }

    private void Update()
    {
        if (!dragging)
        {
            return;
        }

        var mousepos = GetMousePos();

        transform.position = mousepos - _offset;
    }

    Vector2 GetMousePos()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
