using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QueueManager : MonoBehaviour
{
    public CustomerSO[] customerlists;
    public List<CustomerSO> availableCustomers = new List<CustomerSO>();
    public Queue<CustomerSO> customerQueue = new Queue<CustomerSO>();
    public List<CustomerSO> seatedCustomers = new List<CustomerSO>();
    public int maxSeats = 3;
    public CustomerSeat[] seats;

    public int TotalScore = 0;
    
    [SerializeField] public int CUSTOMER_PER_DAY = 10;
    private int customerGoal;

    private int dayCounter = 0;

    public TextMeshProUGUI uiScore;
    public TextMeshProUGUI uiDay;

    

    private void Start()
    {
        for (int i = 0; i < maxSeats; i++)
        {
            seats[i].SetSeatStatus(false);
        }
        startNewDay();
    }

    private void Update()
    {
        while (customerQueue.Count > 0 && seatedCustomers.Count < maxSeats)
        {
            CustomerSO customer = customerQueue.Dequeue();
            AssignCustomerToSeat(customer);
        }

        // Check if the seated customers have reached the goal
        if (customerQueue.Count == 0 && customerGoal > 0)
        {
            AddNewCustomerToQueue();
        }

        if (customerQueue.Count == 0 && customerGoal <= 0)
        {
            startNewDay();
        }
        
    }

    public void EnqueueCustomer(CustomerSO customer)
    {
        customerQueue.Enqueue(customer);
    }

    public void DequeueCustomer()
    {
        if (customerQueue.Count > 0)
        {
            CustomerSO customer = customerQueue.Dequeue();
        }
        else
        {
            Debug.Log("No customers in queue.");
        }
    }

    public CustomerSO GetRandomCustomer()
    {
        if (customerlists.Length == 0)
        {
            Debug.Log("No customers available.");
            return null;
        }

        // Create a copy of customerlists
        CustomerSO[] remainingCustomers = new CustomerSO[customerlists.Length];
        customerlists.CopyTo(remainingCustomers, 0);

        int randomIndex = Random.Range(0, remainingCustomers.Length);
        CustomerSO randomCustomer = remainingCustomers[randomIndex];

        // Remove the selected customer from remainingCustomers
        remainingCustomers = remainingCustomers.Where((source, index) => index != randomIndex).ToArray();

        return randomCustomer;
    }

    public void AddRandomCustomersToQueue(int count)
    {
        availableCustomers.AddRange(customerlists);
        for (int i = 0; i < count; i++)
        {
            if (availableCustomers.Count == 0)
            {
                Debug.Log("No more customers available.");
                break;
            }

            int randomIndex = Random.Range(0, availableCustomers.Count);
            CustomerSO randomCustomer = availableCustomers[randomIndex];
            EnqueueCustomer(randomCustomer);
            availableCustomers.RemoveAt(randomIndex);

            RecipeSO randomItem = randomCustomer.GetRandomWishlist();
        }
    }

    private void AssignCustomerToSeat(CustomerSO customer)
    {
        // Debug.Log(seatedCustomers.Count + " customer(s) sitting");
        foreach (CustomerSeat seat in seats)
        {
            if (!seat.IsOccupied())
            {
                Debug.Log(customerQueue.Count + " customer(s) left in the queue");
                seat.SetCustomer(customer);
                seatedCustomers.Add(customer);
                return;
            }
        }
    }

    public void AddNewCustomerToQueue()
    {
        foreach (CustomerSO customer in customerlists)
        {
            bool isCustomerSeated = seats.Any(seat => seat.IsOccupied() && seat.customer == customer);

            if (!isCustomerSeated)
            {
                // Add the customer to the queue
                EnqueueCustomer(customer);
                Debug.Log("Added customer " + customer.customerName + " back to the queue.");
                return;
            }
        }

        // If all customers are in seats, log that no new customers were added
        // Debug.Log("No new customers added to the queue.");
    }


    public void RemoveCustomerFromSeat(CustomerSeat seat)
    {
        if (seat.IsOccupied())
        {
            int score = seat.GetScore();
            Debug.Log("Customer " + seat.customer.customerName + " state " + seat.customer.currentState + ". Score: " + score);
            CalculateScore(score);

            seatedCustomers.Remove(seat.customer);

            seat.RemoveCustomer();

            if (customerQueue.Count > 0)
            {
                AssignCustomerToSeat(customerQueue.Dequeue());
            }
        }
    }

    private void CalculateScore(int score)
    {
        if (score > 0)
        {
            customerGoal--;
            Debug.Log(customerGoal);
        }
        TotalScore += score;
        uiScore.text = TotalScore.ToString();
    }

    public void startNewDay()
    {
        dayCounter += 1;
        // uiDay.text = "Day: " + dayCounter;
        customerGoal = CUSTOMER_PER_DAY;
        AddRandomCustomersToQueue(customerGoal);
    }

    public int getDayCounter()
    {
        return dayCounter;
    }
}
