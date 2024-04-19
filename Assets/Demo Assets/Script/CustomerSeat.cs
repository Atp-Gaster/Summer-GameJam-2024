using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSeat : MonoBehaviour
{
    private bool occupied = false;
    public CustomerSO customer;
    public QueueManager manager;

    [SerializeField] private Image uifill;
    [SerializeField] private TextMeshProUGUI uiText;

    [SerializeField] public DizzyBar DizzyBar;

    private Dictionary<CustomerSeat, Coroutine> seatCoroutineMap = new Dictionary<CustomerSeat, Coroutine>();

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
        if (newCustomer == null)
        {
            Debug.LogError("Trying to set null customer in seat " + gameObject.name);
            return;
        }

        customer = newCustomer;
        occupied = true;
        Debug.Log("Customer " + customer.customerName + " seated at " + gameObject.name + " wants " + customer.order);

        SpriteRenderer orderSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        SpriteRenderer customerSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        Coroutine coroutine = StartCoroutine(StartTimer(customer.timerDuration));
        seatCoroutineMap[this] = coroutine;

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

        // Set customer state to 0
        ChangeCustomerState(customer, 0);
    }

    public void ChangeCustomerState(CustomerSO customer, int state)
    {
        if (customer == null)
        {
            return;
        }

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
                DizzyBar.CurrentDizzyValue = DizzyBar.CurrentDizzyValue + 10;
                Debug.Log("incorrect cocktail");
            }

            Coroutine coroutine;
            if (seatCoroutineMap.TryGetValue(this, out coroutine))
            {
                StopCoroutine(coroutine); // Stop the coroutine using the reference
                seatCoroutineMap.Remove(this); // Remove the coroutine reference from the map
            }

            // Reset UI and customer state here
            uiText.text = "30";
            uifill.fillAmount = 1;
            uifill.color = Color.white;
            ChangeCustomerState(customer, 3); // Set customer state to 3 (finished)
            customer = null;
            occupied = false;

            SpriteRenderer orderSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
            SpriteRenderer customerSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
            orderSprite.sprite = null;
            customerSprite.sprite = null;
            customerSprite.color = Color.white;

            // Add a new customer to the seat
            SetCustomer(manager.GetRandomCustomer());
        }
    }
    private void OnMouseDown()
    {
        manager.RemoveCustomerFromSeat(this);
    }

    private IEnumerator StartTimer(float duration)
    {
        CustomerSO currentCustomer = customer; // Store the current customer

        yield return new WaitForSeconds(3f);

        Debug.Log("Timer started! " + duration);
        float timer = 0f;

        while (timer <= duration)
        {
            if (currentCustomer != customer)
            {
                // If the customer has changed, exit the coroutine
                yield break;
            }

            uiText.text = Mathf.CeilToInt(duration - timer).ToString();

            uifill.fillAmount = 1 - (timer / duration);

            int remainingTime = Mathf.CeilToInt(duration - timer);

            if (remainingTime >= 16 && customer != null)
            {
                uifill.color = new Color32(0, 255, 11, 168);
                ChangeCustomerState(customer, customer.state[0]);
            }
            else if (remainingTime <= 15 && remainingTime >= 6 && customer != null)
            {
                uifill.color = Color.yellow;
                ChangeCustomerState(customer, customer.state[1]);
            }
            else if (remainingTime <= 5 && customer != null)
            {
                uifill.color = Color.red;
                ChangeCustomerState(customer, customer.state[2]);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Timer has finished
        uiText.text = "0";
        Debug.Log("Timer finished!");

        if (currentCustomer != null)
        {
            ChangeCustomerState(currentCustomer, currentCustomer.state[3]);
            manager.RemoveCustomerFromSeat(this);
        }
        else
        {
            uiText.text = "30"; // Reset timer to 30 if no customer is assigned
            uifill.fillAmount = 1;
            uifill.color = Color.white; // Reset color to white
        }
    }

}
