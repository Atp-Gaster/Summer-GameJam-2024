using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CustomerSeat : MonoBehaviour
{
    private bool occupied = false;
    public CustomerSO customer;
    public QueueManager manager;

    public void SetSeatStatus(bool isOccupied)
    {
        occupied = isOccupied;
    }

    public bool IsOccupied()
    {
        return occupied;
    }

    public void SetCustomer(CustomerSO newCustomer)
    {
        customer = newCustomer;
        occupied = true;
        Debug.Log("Customer " + customer.customerName + " seated at " + gameObject.name + " wants " + customer.order);

        SpriteRenderer orderSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        SpriteRenderer customerSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        // Assign the sprite to the SpriteRenderer
        if (orderSprite != null && customerSprite != null)
        {
            orderSprite.sprite = newCustomer.orderSprite;
            customerSprite.sprite = newCustomer.customerSprite;
        }
        else
        {
            Debug.LogError("Item sprite or SpriteRenderer is null!");
        }
    }

    public void ChangeCustomerState(CustomerSO customer, int state)
    {
        customer.currentState = state;
        SpriteRenderer customerSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        switch (state)
        {
            case 0: // Happy
                customerSprite.color = Color.green;
                break;
            case 1: // Normal
                customerSprite.color = Color.yellow;
                break;
            case 2: // Sad
                customerSprite.color = Color.red;
                break;
            default:
                break;
        }
    }

    public int GetScore()
    {
        if (customer.currentState == 0)
        {
            return 20;
        }
        else if (customer.currentState == 1 || customer.currentState == 2)
        {
            return 10;
        }
        else
        {
            return 0;
        }
    }

    public void RemoveCustomer()
    {
        if (occupied)
        {
            Debug.Log("Removing customer " + customer.customerName + " from " + gameObject.name);
            customer = null;
            occupied = false;

            SpriteRenderer orderSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

            SpriteRenderer customerSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
            customerSprite.color = Color.white;
            if (orderSprite != null && customerSprite != null)
            {
                orderSprite.sprite = null;
                customerSprite.sprite = null;
            }
        }
    }

    private void OnMouseDown()
    {
        manager.RemoveCustomerFromSeat(this);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cocktail")
        {
            order cocktail = collision.GetComponent<order>();
            if (cocktail != null && cocktail.cocktail == customer.item)
            {
                manager.RemoveCustomerFromSeat(this);
                Debug.Log("correct cocktail");
            }
            else
            {
                Debug.Log("incorrect cocktail");
            }
            // Debug.Log(cocktail.cocktail.Name);
        }
    }
}
